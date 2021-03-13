using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eCommerce.Storefront.Tests.CustomerSpecs
{
    [TestClass]
    public class WhenUpdatingCustomersNameWithNullValue : WithValidCustomer
    {
        public override void When()
        {
            sut.FirstName = null;
            sut.SecondName = null;
        }

        [TestMethod]
        public void ThenTheCustomerShouldHaveTwoBrokenRules()
        {
            Assert.AreEqual(2, sut.GetBrokenRules().Count());
        }
    }
}