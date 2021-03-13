using System.Linq;
using eCommerce.Storefront.Model.Basket;
using eCommerce.Storefront.Model.Products;
using eCommerce.Storefront.Model.Shipping;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eCommerce.Storefront.Tests.BasketSpecs
{
    [TestClass]
    public class WhenChangingTheQuantityOfProductThatExisitsInBasketToZeroQuantity
    {
        private Basket _basket;
        private Product _product;

        [TestInitialize]
        public void Given()
        {
            _basket = new Basket();

            _basket.SetDeliveryOption(new DeliveryOption());

            _product = new Product()
            {
                Title = new ProductTitle()
                {
                    Name = "Product A",
                    Price = 15m,
                    Brand = new Brand(),
                    Category = new Category(),
                    Color = new ProductColor(),
                    Products = null
                },
                Size = new ProductSize()
            };
            
            _basket.Add(_product);   
        }

        [TestMethod]
        public void ThenTheProductShouldNotBeRemoved()
        {
            int newQty = 0;

            _basket.ChangeQtyOfProduct(newQty, _product);

            Assert.IsNotNull(_basket.Items.FirstOrDefault(i => i.Product == _product));
        }
    }
}