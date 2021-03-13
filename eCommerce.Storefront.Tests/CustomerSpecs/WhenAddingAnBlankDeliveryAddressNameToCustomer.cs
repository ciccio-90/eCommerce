using eCommerce.Storefront.Model.Customers;
using Infrastructure.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eCommerce.Storefront.Tests.CustomerSpecs
{
    [TestClass]
    public class WhenAddingAnBlankDeliveryAddressNameToCustomer : WithValidCustomer
    {
        private DeliveryAddress _address;

        public override void When()
        {
            _address = new DeliveryAddress()
            {
                AddressLine = "99 Old street", 
                City = "City", 
                State = "State", 
                Country = "Country",
                ZipCode = "PostCode",
                Customer = sut
            };
        }

        [TestMethod]
        [ExpectedException(typeof(EntityBaseIsInvalidException))]
        public void ThenAnInvalidAddressExceptionWillBeThrown()
        {
            sut.AddAddress(_address);
        }
    }
}