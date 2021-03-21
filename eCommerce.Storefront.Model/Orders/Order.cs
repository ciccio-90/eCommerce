using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Domain;
using eCommerce.Storefront.Model.Customers;
using eCommerce.Storefront.Model.Products;
using eCommerce.Storefront.Model.Shipping;
using Infrastructure.Configuration;
using Infrastructure.Email;

namespace eCommerce.Storefront.Model.Orders
{
    public class Order : EntityBase<int>, IAggregateRoot
    {
        private readonly IApplicationSettings _applicationSettings;
        private readonly IEmailService _emailService;
        private readonly IList<OrderItem> _items;
        private readonly DateTime _created;
        private Payment _payment;

        public Order()
        {
            _created = DateTime.Now;
            _items = new List<OrderItem>();
            Status = OrderStatus.Open;
        }

        public Order(IApplicationSettings applicationSettings,
                     IEmailService emailService)
        {
            _applicationSettings = applicationSettings;
            _emailService = emailService;
            _created = DateTime.Now;
            _items = new List<OrderItem>();
            Status = OrderStatus.Open;
        }

        public DateTime Created
        {
            get { return _created; }
        }

        public decimal ShippingCharge { get; set; }

        public ShippingService ShippingService { get; set; }

        public decimal ItemTotal()
        {
            return Items.Sum(i => i.LineTotal());
        }

        public decimal Total()
        {
            return Items.Sum(i => i.LineTotal()) + ShippingCharge;
        }

        public Payment Payment
        {
            get { return _payment; }
        }

        public void SetPayment(Payment payment)
        {
            if (OrderHasBeenPaidFor())
            {
                throw new OrderAlreadyPaidForException(GetDetailsOnExisitingPayment());
            }

            if (OrderTotalMatches(payment))
            {
                _payment = payment;
            }
            else
            {
                throw new PaymentAmountDoesNotEqualOrderTotalException(GetDetailsOnIssueWith(payment));
            }

            Submit();
        }

        private void Submit()
        {
            if (Status == OrderStatus.Open)
            {
                if (OrderHasBeenPaidFor())
                {
                    Status = OrderStatus.Submitted;
                }

                StringBuilder emailBody = new StringBuilder();
                string emailAddress = Customer.Email;
                string emailSubject = string.Format("Order #{0}", Id);

                emailBody.AppendLine(string.Format("Hello {0},", Customer.FirstName));
                emailBody.AppendLine();
                emailBody.AppendLine("The following order will be packed and dispatched as soon as possible.");
                emailBody.AppendLine(ToString());
                emailBody.AppendLine();
                emailBody.AppendLine("Thank you for your custom.");
                _emailService.SendMail(_applicationSettings.MailSettingsSmtpNetworkUserName, emailAddress, emailSubject, emailBody.ToString());                
            }
            else
            {
                throw new InvalidOperationException("You cannot submit this order as it has already been submitted.");
            }
        }

        private string GetDetailsOnExisitingPayment()
        {
            return string.Format("Order has already been paid for. {0} was paid on {1}. Payment token '{2}'", Payment.Amount, Payment.DatePaid, Payment.TransactionId);
        }

        private string GetDetailsOnIssueWith(Payment payment)
        {
            return string.Format("Payment amount is invalid. Order total is {0} but payment for {1}. Payment token '{2}'", this.Total(), payment.Amount, payment.TransactionId);
        }

        public bool OrderHasBeenPaidFor()
        {
            return Payment != null && OrderTotalMatches(Payment);
        }

        private bool OrderTotalMatches(Payment payment)
        {
            return Total() == payment.Amount;
        }

        public Customer Customer { get; set; }

        public DeliveryAddress DeliveryAddress { get; set; }

        public IEnumerable<OrderItem> Items
        {
            get { return _items; }
        }

        public OrderStatus Status { get; set; }

        public void AddItem(Product product, int qty)
        {
            if (CanAddProduct())
            {
                if (!OrderContains(product))
                {
                    _items.Add(new OrderItem(product, this, qty));
                }
            }   
            else
            {
                throw new CannotAmendOrderException(string.Format("You cannot add an item to an order with the status of '{0}'.", Status.ToString()));
            }
        }

        private bool CanAddProduct()
        {
            if (Status == OrderStatus.Open)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool OrderContains(Product product)
        {
            return _items.Any(i => i.Product.Id == product.Id);
        }

        protected override void Validate()
        {
            if (Created == DateTime.MinValue)
            {
                base.AddBrokenRule(OrderBusinessRules.CreatedDateRequired);
            }

            if (Customer == null)
            {
                base.AddBrokenRule(OrderBusinessRules.CustomerRequired);
            }

            if (DeliveryAddress == null)
            {
                base.AddBrokenRule(OrderBusinessRules.DeliveryAddressRequired);
            }

            if (Items == null || !Items.Any())
            {
                base.AddBrokenRule(OrderBusinessRules.ItemsRequired); 
            }
            else
            {
                if (Items.Any(i => i.GetBrokenRules().Any()))
                {
                    foreach (OrderItem item in Items.Where(i => i.GetBrokenRules().Any()))
                    {
                        foreach (BusinessRule businessRule in item.GetBrokenRules())
                        {
                            base.AddBrokenRule(businessRule);
                        }
                    }
                }
            } 

            if (ShippingService == null)
            {
                base.AddBrokenRule(OrderBusinessRules.ShippingServiceRequired);
            }
        }

        public override string ToString()
        {
            StringBuilder orderInfo = new StringBuilder();

            foreach (OrderItem item in _items)
            {
                orderInfo.AppendLine(string.Format("{0} of {1} ", item.Qty, item.Product.Name));
            }

            orderInfo.AppendLine(string.Format("Shipping: {0}", this.ShippingCharge));
            orderInfo.AppendLine(string.Format("Total: {0}", this.Total()));

            return orderInfo.ToString();
        }
    }
}