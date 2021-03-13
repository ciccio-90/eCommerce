using System.Linq;
using eCommerce.Storefront.Model.Basket;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eCommerce.Storefront.Tests.BasketSpecs
{
    [TestClass]
    public class WhenBasketIsGivenNullDeliveryOption
    {
        private Basket _basket;        

        [TestInitialize]
        public void Given()
        {
            _basket = new Basket();

            _basket.SetDeliveryOption(null);            
        }

        [TestMethod]
        public void ThenTheBasketShouldHaveOneBrokenRule()
        {
            Assert.AreEqual(1, _basket.GetBrokenRules().Count());
        }

        [TestMethod]
        public void ThenTheBasketShouldHaveBrokenRuleHighlightingTheInvalidDeliveryOption()
        {
            Assert.AreEqual(BasketBusinessRules.DeliveryOptionRequired.Rule, _basket.GetBrokenRules().First(x => true).Rule);
            Assert.AreEqual(BasketBusinessRules.DeliveryOptionRequired.Property, _basket.GetBrokenRules().First(x => true).Property);
        }
    }
}