using System.Linq;
using eCommerce.Storefront.Model.Basket;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eCommerce.Storefront.Tests.BasketItemSpecs
{
    [TestClass]
    public class WhenBasketItemIsCreatedWithNullProduct
    {
        private BasketItem _basketItem;

        [TestInitialize]
        public void Given()
        {
            _basketItem = new BasketItem(null, new Basket(), 1);
        }

        [TestMethod]
        public void ThenItShouldHaveBrokenRuleHighlightingTheRequirementForProduct()
        {            
            Assert.AreEqual(BasketItemBusinessRules.ProductRequired.Rule, _basketItem.GetBrokenRules().First(x => true).Rule);
            Assert.AreEqual(BasketItemBusinessRules.ProductRequired.Property, _basketItem.GetBrokenRules().First(x => true).Property);        
        }

        [TestMethod]
        public void ThenItShouldHaveOneBrokenRule()
        {
            Assert.AreEqual(1, _basketItem.GetBrokenRules().Count());
        }
    }
}