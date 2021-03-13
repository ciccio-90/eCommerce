using System.Collections.Generic;
using eCommerce.Storefront.Services.ViewModels;

namespace eCommerce.Storefront.Controllers.ViewModels.ProductCatalog
{
    public class BaseProductCatalogPageView : BasePageView
    {
        public IEnumerable<CategoryView> Categories { get; set;}
    }
}