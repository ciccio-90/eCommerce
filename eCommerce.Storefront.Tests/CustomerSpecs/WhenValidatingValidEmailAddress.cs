using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace eCommerce.Storefront.Tests.CustomerSpecs
{
    [TestClass]
    public class WhenValidatingValidEmailAddress
    {
        private string _validEmailAddress;

        [TestInitialize]
        public void Given()
        {
            _validEmailAddress = "scott@elbandit.co.uk";
        }

        [TestMethod]
        public void ValidEmailAddressWillSatisfiyTheEmailValidationSpecification()
        {
            Assert.IsTrue(Regex.IsMatch(_validEmailAddress, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"));
        }
    }
}