using System.Collections.Generic;
using Infrastructure.Domain;
using eCommerce.Storefront.Model.Orders;
using System.Text.RegularExpressions;

namespace eCommerce.Storefront.Model.Customers
{
    public class Customer : EntityBase<long>
    {
        private readonly IList<DeliveryAddress> _deliveryAddressBook = new List<DeliveryAddress>();
        private Basket.Basket _basket;
      
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public IList<Order> Orders { get; set; }

        public void AddAddress(DeliveryAddress deliveryAddress)
        {
            deliveryAddress.ThrowExceptionIfInvalid();
            _deliveryAddressBook.Add(deliveryAddress);
        }

        public IEnumerable<DeliveryAddress> DeliveryAddressBook
        {
            get { return _deliveryAddressBook; }
        }

        public void AddBasket(Basket.Basket basket)
        {
            basket.ThrowExceptionIfInvalid();
            
            _basket = basket;
        }

        public Basket.Basket Basket
        {
            get { return _basket; }
        }

        protected override void Validate()
        {    
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                AddBrokenRule(new BusinessRule() { Property = nameof(FirstName), Rule = "A customer must have a first name." });
            }

            if (string.IsNullOrWhiteSpace(SecondName))
            {
                AddBrokenRule(new BusinessRule() { Property = nameof(SecondName), Rule = "A customer must have a second name." });
            }

            if (!Regex.IsMatch(Email, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
            {
                AddBrokenRule(new BusinessRule() { Property = nameof(Email), Rule = "A customer must have a valid email address." });
            }

            if (string.IsNullOrWhiteSpace(UserId))
            {
                AddBrokenRule(new BusinessRule() { Property = nameof(UserId), Rule = "A customer must have an identity user." });
            }
        }
    }
}
