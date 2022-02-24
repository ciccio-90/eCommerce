using System.Linq;
using AutoMapper;
using eCommerce.Storefront.Model.Basket;
using eCommerce.Storefront.Model.Customers;
using eCommerce.Storefront.Model.Orders;
using eCommerce.Storefront.Services.Interfaces;
using eCommerce.Storefront.Services.Messaging.OrderService;
using eCommerce.Storefront.Services.ViewModels;
using System;
using System.Text;
using eCommerce.Storefront.Repository.EntityFrameworkCore.Repositories.Interfaces;
using eCommerce.Storefront.Repository.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace eCommerce.Storefront.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public OrderService(IOrderRepository orderRepository,
                            IBasketRepository basketRepository,
                            ICustomerRepository customerRepository,
                            IUnitOfWork uow,
                            IMapper mapper,
                            ILogger<OrderService> logger,
                            IConfiguration configuration,
                            IEmailService emailService)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
            _basketRepository = basketRepository;
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;
            _emailService = emailService;
        }

        public CreateOrderResponse CreateOrder(CreateOrderRequest request)
        {
            CreateOrderResponse response = new CreateOrderResponse();
            Customer customer = _customerRepository.FindBy(request.CustomerEmail);
            Basket basket = _basketRepository.FindBy(request.BasketId);
            DeliveryAddress deliveryAddress = customer.DeliveryAddressBook.FirstOrDefault(d => d.Id == request.DeliveryId);
            Order order = ConvertToOrder(basket);
            order.Customer = customer;
            order.DeliveryAddress = deliveryAddress;

            _orderRepository.Save(order);
            _basketRepository.Remove(basket);
            _uow.Commit();

            response.Order = _mapper.Map<Order, OrderView>(order);

            return response;
        }

        public SetOrderPaymentResponse SetOrderPayment(SetOrderPaymentRequest paymentRequest)
        {
            SetOrderPaymentResponse paymentResponse = new SetOrderPaymentResponse();
            Order order = _orderRepository.FindBy(paymentRequest.OrderId);

            try
            {
                order.SetPayment(new Payment(DateTime.Now, paymentRequest.PaymentToken, paymentRequest.PaymentMerchant, paymentRequest.Amount));
                Submit(order, paymentRequest.CustomerEmail);
                _orderRepository.Save(order);
                _uow.Commit();
            }
            catch (OrderAlreadyPaidForException ex)
            {
                // Refund the payment using the payment service.
                _logger.LogError(ex.Message);
            }
            catch (PaymentAmountDoesNotEqualOrderTotalException ex)
            {
                // Refund the payment using the payment service.
                _logger.LogError(ex.Message);
            }

            paymentResponse.Order = _mapper.Map<Order, OrderView>(order);

            return paymentResponse;
        }

        public GetOrderResponse GetOrder(GetOrderRequest request)
        {
            GetOrderResponse response = new GetOrderResponse();
            Order order = _orderRepository.FindBy(request.OrderId);
            response.Order = _mapper.Map<Order, OrderView>(order);

            return response;
        }

        private Order ConvertToOrder(Basket basket)
        {
            Order order = new Order();
            order.ShippingCharge = basket.DeliveryCost();
            order.ShippingService = basket.DeliveryOption.ShippingService;

            foreach (BasketItem item in basket.Items)
            {
                order.AddItem(item.Product, item.Qty);
            }

            return order;
        }

        private void Submit(Order order, string customerEmail)
        {
            if (order.Status == OrderStatus.Open)
            {
                if (order.OrderHasBeenPaidFor())
                {
                    order.Status = OrderStatus.Submitted;
                }

                StringBuilder emailBody = new StringBuilder();
                string emailAddress = customerEmail;
                string emailSubject = string.Format("Order #{0}", order.Id);

                emailBody.AppendLine(string.Format("Hello {0},", order.Customer.FirstName));
                emailBody.AppendLine();
                emailBody.AppendLine("The following order will be packed and dispatched as soon as possible.");
                emailBody.AppendLine(ToString());
                emailBody.AppendLine();
                emailBody.AppendLine("Thank you for your custom.");

                if (!string.IsNullOrWhiteSpace(_configuration["MailSettingsSmtpNetworkPassword"]))
                {
                    _emailService.SendMail(_configuration["MailSettingsSmtpNetworkUserName"], emailAddress, emailSubject, emailBody.ToString());                
                }
            }
            else
            {
                throw new InvalidOperationException("You cannot submit this order as it has already been submitted.");
            }
        }
    }
}