using eCommerce.Storefront.Model.Customers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eCommerce.Storefront.Tests.CustomerSpecs
{
    [TestClass]
    public class WhenValidatingValidEmailAddress
    {
        private string _validEmailAddress;
        private EmailValidationSpecification _emailValidationSpecification;

        [TestInitialize]
        public void Given()
        {
            _validEmailAddress = "scott@elbandit.co.uk";

            _emailValidationSpecification = new EmailValidationSpecification();
        }

        [TestMethod]
        public void ValidEmailAddressWillSatisfiyTheEmailValidationSpecification()
        {
            Assert.IsTrue(_emailValidationSpecification.IsSatisfiedBy(_validEmailAddress));
        }
    }
}