using System.Collections.Generic;
using eCommerce.Storefront.Services.Messaging.ProductCatalogService;
using eCommerce.Storefront.Services.ViewModels;
using eCommerce.Storefront.Services.Cache;
using Infrastructure.Authentication;
using eCommerce.Storefront.Services.Interfaces;

namespace eCommerce.Storefront.Controllers.Controllers
{
    public class ProductCatalogBaseController : BaseController
    {
        protected readonly ICachedProductCatalogService _cachedProductCatalogService;

        public ProductCatalogBaseController(ICookieAuthentication cookieAuthentication,
                                            ICustomerService customerService,
                                            ICachedProductCatalogService cachedProductCatalogService) : base(cookieAuthentication,
                                                                                                             customerService)
        {
            _cachedProductCatalogService = cachedProductCatalogService;
        }

        public IEnumerable<CategoryView> GetCategories()
        {
            GetAllCategoriesResponse response = _cachedProductCatalogService.GetAllCategories();

            return response.Categories;
        }
    }
}