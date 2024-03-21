using eCommerce.Storefront.Controllers.ViewModels.ProductCatalog;
using eCommerce.Storefront.Services.Messaging.ProductCatalogService;
using Microsoft.AspNetCore.Mvc;
using eCommerce.Storefront.Services.Cache;
using eCommerce.Storefront.Controllers.ViewModels;
using System.Diagnostics;
using eCommerce.Storefront.Services.Interfaces;
using eCommerce.Storefront.Controllers.Services.Interfaces;

namespace eCommerce.Storefront.Controllers.Controllers
{
    public class HomeController : ProductCatalogBaseController
    {
        public HomeController(ICookieAuthentication cookieAuthentication,
                              ICustomerService customerService,
                              ICachedProductCatalogService cachedProductCatalogService) : base(cookieAuthentication,
                                                                                               customerService,
                                                                                               cachedProductCatalogService)
        {
        }

        public IActionResult Index()
        {
            HomePageView homePageView = new HomePageView
            {
                Categories = GetCategories(),
                BasketSummary = GetBasketSummaryView()
            };
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