using eCommerce.Storefront.Controllers.ViewModels.ProductCatalog;
using Infrastructure.CookieStorage;
using eCommerce.Storefront.Services.Messaging.ProductCatalogService;
using Microsoft.AspNetCore.Mvc;
using eCommerce.Storefront.Services.Cache;
using eCommerce.Storefront.Controllers.ViewModels;
using System.Diagnostics;

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
            homePageView.Categories = GetCategories();
            homePageView.BasketSummary = GetBasketSummaryView();            
            GetFeaturedProductsResponse response = _cachedProductCatalogService.GetFeaturedProducts();
            homePageView.Products = response.Products;
            
            return View(homePageView);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}