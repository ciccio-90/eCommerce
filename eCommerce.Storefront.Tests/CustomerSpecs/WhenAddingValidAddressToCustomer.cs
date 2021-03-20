using System.Linq;
using eCommerce.Storefront.Model.Customers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eCommerce.Storefront.Tests.CustomerSpecs
{
    [TestClass]
    public class WhenAddingValidAddressToCustomer : WithValidCustomer
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
                Name = "My Work Pad",
                Customer = sut
            };

            sut.AddAddress(_address);
        }

        [TestMethod]
        public void ThenTheAddressShouldAppearInTheCustomersList()
        {
            Assert.AreEqual(1, sut.DeliveryAddressBook.Count());

            Assert.IsTrue(sut.DeliveryAddressBook.Any(a => a == _address));            
        }
    }
}