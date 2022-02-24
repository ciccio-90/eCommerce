using Microsoft.VisualStudio.TestTools.UnitTesting;
using eCommerce.Storefront.Model.Customers;
using eCommerce.Storefront.Model;

namespace eCommerce.Storefront.Tests.AddressSpecs
{
    [TestClass]
    public class WhenCreatingNewAddressWithBlankCity
    {
        [TestMethod]
        [ExpectedException(typeof(EntityBaseIsInvalidException))]
        public void ThenAnInvalidAddressExceptionWillBeThrown()
        {
            DeliveryAddress invalidAddress = new DeliveryAddress()
            {
                AddressLine = "99 Old street",
                City = string.Empty, 
                State = "State", 
                Country = "Country",
                ZipCode = "PostCode",
                Name = "Home",
                Customer = new Customer()
            };

            invalidAddress.ThrowExceptionIfInvalid();
        }
    }
}