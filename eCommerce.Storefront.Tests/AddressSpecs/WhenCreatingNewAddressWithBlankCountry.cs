using eCommerce.Storefront.Model.Customers;
using Infrastructure.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eCommerce.Storefront.Tests.AddressSpecs
{
    [TestClass]
    public class WhenCreatingNewAddressWithBlankContry
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
                Country = string.Empty,
                ZipCode = "PostCode",
                Name = "Home",
                Customer = new Customer()
            };

            invalidAddress.ThrowExceptionIfInvalid();
        }
    }
}