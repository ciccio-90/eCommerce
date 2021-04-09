using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace eCommerce.Storefront.Tests.CustomerSpecs
{
    [TestClass]
    public class WhenValidatingAnEmptyEmailAddress
    {
        private string _blankEmailAddress;

        [TestInitialize]
        public void Given()
        {
            _blankEmailAddress = string.Empty;
        }

        [TestMethod]
        public void ThenTheEmailAddressWillNotSatisfiyTheEmailValidationSpecification()
        {
            Assert.IsFalse(Regex.IsMatch(_blankEmailAddress, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"));
        }
    }
}