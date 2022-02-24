using Microsoft.VisualStudio.TestTools.UnitTesting;
using eCommerce.Storefront.Model.Customers;
using eCommerce.Storefront.Model;

namespace eCommerce.Storefront.Tests.AddressSpecs
{
    [TestClass]
    public class WhenCreatingNewAddressWithBlankZipCode
    {
        [TestMethod]
        [ExpectedException(typeof(EntityBaseIsInvalidException))]
        public void ThenAnInvalidAddressExceptionWillBeThrown()
        {
            DeliveryAddress invalidAddress = new DeliveryAddress()
            {
                AddressLine = "99 Old street", 
                City = "City", 
                State = "State", 
                Country = "Country",
                ZipCode = string.Empty,
                Name = "Home",
                Customer = new Customer()
            };

            invalidAddress.ThrowExceptionIfInvalid();
        }
    }
}