using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eCommerce.Storefront.Tests.CustomerSpecs
{
    [TestClass]
    public class WhenCreatingAnEmailAddressWithInvalidAddress : WithValidCustomer
    {
        private string _email;

        public override void When()
        {
            _email = "scott@";
            sut.Email = _email;
        }

        [TestMethod]
        public void ThenTheCustomerShouldHaveOneBrokenRule()
        {
            Assert.AreEqual(1, sut.GetBrokenRules().Count());
        }
    }
}