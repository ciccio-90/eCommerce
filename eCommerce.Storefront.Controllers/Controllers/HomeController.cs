using eCommerce.Storefront.Controllers.ViewModels.ProductCatalog;
using Infrastructure.CookieStorage;
using eCommerce.Storefront.Services.Messaging.ProductCatalogService;
using Microsoft.AspNetCore.Mvc;
using eCommerce.Storefront.Services.Cache;

namespace eCommerce.Storefront.Controllers.Controllers
{
    public class HomeController : ProductCatalogBaseController
    {
        private readonly ICachedProductCatalogService _cachedProductCatalogService;

        public HomeController(ICachedProductCatalogService cachedProductCatalogService,
                              ICookieStorageService cookieStorageService) : base(cookieStorageService, cachedProductCatalogService)
        {
            _cachedProductCatalogService = cachedProductCatalogService;
        }

        public IActionResult Index()
        {
            HomePageView homePageView = new HomePageView();
            homePageView.Categories = base.GetCategories();
            homePageView.BasketSummary = base.GetBasketSummaryView();            
            GetFeaturedProductsResponse response = _cachedProductCatalogService.GetFeaturedProducts();
            homePageView.Products = response.Products;
            
            return View(homePageView);
        }
    }
}