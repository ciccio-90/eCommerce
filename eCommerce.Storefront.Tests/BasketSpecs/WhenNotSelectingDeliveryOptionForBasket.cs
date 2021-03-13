using eCommerce.Storefront.Model.Basket;
using eCommerce.Storefront.Model.Shipping;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eCommerce.Storefront.Tests.BasketSpecs
{
    [TestClass]
    public class WhenNotSelectingDeliveryOptionForBasket
    {
        private Basket _basket;        

        [TestInitialize]
        public void Given()
        {
            _basket = new Basket();

            _basket.SetDeliveryOption(new DeliveryOption());            
        }

        [TestMethod]
        public void ThenTheDeliveryCostShouldBeZero()
        {
           Assert.AreEqual(0, _basket.DeliveryCost());
        }
    }
}