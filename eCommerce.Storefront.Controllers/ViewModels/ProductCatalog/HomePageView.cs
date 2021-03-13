using System.Collections.Generic;
using eCommerce.Storefront.Services.ViewModels;

namespace eCommerce.Storefront.Controllers.ViewModels.ProductCatalog
{
    public class HomePageView : BaseProductCatalogPageView
    {
        public IEnumerable<ProductSummaryView> Products { get; set;}
    }
}