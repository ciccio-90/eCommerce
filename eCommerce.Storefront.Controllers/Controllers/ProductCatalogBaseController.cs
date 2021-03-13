using System.Collections.Generic;
using Infrastructure.CookieStorage;
using eCommerce.Storefront.Services.Messaging.ProductCatalogService;
using eCommerce.Storefront.Services.ViewModels;
using eCommerce.Storefront.Services.Cache;

namespace eCommerce.Storefront.Controllers.Controllers
{
    public class ProductCatalogBaseController : BaseController
    {
        private readonly ICachedProductCatalogService _cachedProductCatalogService;

        public ProductCatalogBaseController(ICookieStorageService cookieStorageService,
                                            ICachedProductCatalogService cachedProductCatalogService) : base(cookieStorageService)
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