using System.Collections.Generic;
using Infrastructure.Domain;
using eCommerce.Storefront.Model.Orders;

namespace eCommerce.Storefront.Model.Customers
{
    public class Customer : EntityBase<long>, IAggregateRoot
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
                AddBrokenRule(CustomerBusinessRules.FirstNameRequired);
            }

            if (string.IsNullOrWhiteSpace(SecondName))
            {
                AddBrokenRule(CustomerBusinessRules.SecondNameRequired);
            }

            if (!new EmailValidationSpecification().IsSatisfiedBy(Email))
            {
                AddBrokenRule(CustomerBusinessRules.EmailRequired);
            }

            if (string.IsNullOrWhiteSpace(UserId))
            {
                AddBrokenRule(CustomerBusinessRules.UserIdRequired);
            }
        }
    }
}
