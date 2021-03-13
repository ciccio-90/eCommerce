using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Domain;
using eCommerce.Storefront.Model.Orders;

namespace eCommerce.Storefront.Model.Customers
{
    public class Customer : EntityBase<int>, IAggregateRoot
    {
        private readonly IList<DeliveryAddress> _deliveryAddressBook = new List<DeliveryAddress>();
      
        public string IdentityToken { get; set; }
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

        protected override void Validate()
        {
            if (string.IsNullOrEmpty(FirstName))
            {
                base.AddBrokenRule(CustomerBusinessRules.FirstNameRequired);
            }

            if (string.IsNullOrEmpty(SecondName))
            {
                base.AddBrokenRule(CustomerBusinessRules.SecondNameRequired);
            }

            if (!new EmailValidationSpecification().IsSatisfiedBy(Email))
            {
                base.AddBrokenRule(CustomerBusinessRules.EmailRequired);
            }

            if (string.IsNullOrEmpty(IdentityToken))
            {
                base.AddBrokenRule(CustomerBusinessRules.IdentityTokenRequired);
            }
        }
    }
}