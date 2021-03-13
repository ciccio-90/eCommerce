using System.Collections.Generic;
using eCommerce.Storefront.Services.ViewModels;

namespace eCommerce.Storefront.Controllers.ViewModels.ProductCatalog
{
    public class BasketDetailView : BaseProductCatalogPageView
    {
        public BasketView Basket { get; set; }
        public IEnumerable<DeliveryOptionView> DeliveryOptions { get; set; }
    }
}