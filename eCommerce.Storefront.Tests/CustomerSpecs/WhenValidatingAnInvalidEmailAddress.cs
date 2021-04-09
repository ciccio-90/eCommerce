using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace eCommerce.Storefront.Tests.CustomerSpecs
{
    [TestClass]
    public class WhenValidatingAnInvalidEmailAddress
    {
        private string _invalidEmailAddress;

        [TestInitialize]
        public void Given()
        {
            _invalidEmailAddress = "gg@kkkkk";
        }

        [TestMethod]
        public void ThenTheEmailAddressWillNotSatisfiyTheEmailValidationSpecification()
        {
            Assert.IsFalse(Regex.IsMatch(_invalidEmailAddress, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"));
        }
    }
}