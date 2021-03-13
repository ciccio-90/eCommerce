using eCommerce.Storefront.Model.Customers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eCommerce.Storefront.Tests.CustomerSpecs
{
    [TestClass]
    public class WhenValidatingAnEmptyEmailAddress
    {
        private string _blankEmailAddress;
        private EmailValidationSpecification _emailValidationSpecification;

        [TestInitialize]
        public void Given()
        {
            _blankEmailAddress = string.Empty;

            _emailValidationSpecification = new EmailValidationSpecification();
        }

        [TestMethod]
        public void ThenTheEmailAddressWillNotSatisfiyTheEmailValidationSpecification()
        {
            Assert.IsFalse(_emailValidationSpecification.IsSatisfiedBy(_blankEmailAddress));
        }
    }
}