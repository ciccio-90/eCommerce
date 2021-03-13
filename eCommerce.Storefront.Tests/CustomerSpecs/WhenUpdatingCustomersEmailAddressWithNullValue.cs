using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eCommerce.Storefront.Tests.CustomerSpecs
{
    [TestClass]
    public class WhenUpdatingCustomersEmailAddressWithNullValue : WithValidCustomer
    {
        public override void When()
        {
            sut.Email = null;
        }

        [TestMethod]
        public void ThenTheCustomerShouldHaveOneBrokenRule()
        {
            Assert.AreEqual(1, sut.GetBrokenRules().Count());
        }
    }
}