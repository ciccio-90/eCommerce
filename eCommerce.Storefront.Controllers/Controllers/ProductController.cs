using eCommerce.Storefront.Controllers.DTOs;
using eCommerce.Storefront.Controllers.ViewModels.ProductCatalog;
using Infrastructure.CookieStorage;
using eCommerce.Storefront.Services.Messaging.ProductCatalogService;
using eCommerce.Storefront.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;
using eCommerce.Storefront.Services.Cache;
using Infrastructure.Configuration;

namespace eCommerce.Storefront.Controllers.Controllers
{
    public class ProductController : ProductCatalogBaseController
    {
        private readonly ICachedProductCatalogService _cachedProductCatalogService;
        private readonly IApplicationSettings _applicationSettings;

        public ProductController(ICachedProductCatalogService cachedProductCatalogService,
                                 ICookieStorageService cookieStorageService,
                                 IApplicationSettings applicationSettings) : base(cookieStorageService, cachedProductCatalogService)
        {
            _cachedProductCatalogService = cachedProductCatalogService;
            _applicationSettings = applicationSettings;
        }

        public IActionResult GetProductsByCategory(int categoryId)
        {
            GetProductsByCategoryRequest productSearchRequest = GenerateInitialProductSearchRequestFrom(categoryId);
            GetProductsByCategoryResponse response = _cachedProductCatalogService.GetProductsByCategory(productSearchRequest);
            ProductSearchResultView productSearchResultView = GetProductSearchResultViewFrom(response);

            return View("ProductSearchResults", productSearchResultView);
        }

        private ProductSearchResultView GetProductSearchResultViewFrom(GetProductsByCategoryResponse response)
        {
            ProductSearchResultView productSearchResultView = new ProductSearchResultView();
            productSearchResultView.BasketSummary = base.GetBasketSummaryView();
            productSearchResultView.Categories = base.GetCategories();
            productSearchResultView.CurrentPage = response.CurrentPage;
            productSearchResultView.NumberOfTitlesFound = response.NumberOfTitlesFound;
            productSearchResultView.Products = response.Products;
            productSearchResultView.RefinementGroups = response.RefinementGroups;
            productSearchResultView.SelectedCategory = response.SelectedCategory;
            productSearchResultView.SelectedCategoryName = response.SelectedCategoryName;
            productSearchResultView.TotalNumberOfPages = response.TotalNumberOfPages;
            
            return productSearchResultView;
        }

        private GetProductsByCategoryRequest GenerateInitialProductSearchRequestFrom(int categoryId)
        {
            GetProductsByCategoryRequest productSearchRequest = new GetProductsByCategoryRequest();
            productSearchRequest.NumberOfResultsPerPage = _applicationSettings.NumberOfResultsPerPage;
            productSearchRequest.CategoryId = categoryId;
            productSearchRequest.Index = 1;
            productSearchRequest.SortBy = ProductsSortBy.PriceHighToLow;

            return productSearchRequest;
        }

        [HttpPost]
        public IActionResult GetProducts([FromBody] ProductSearchRequest jsonProductSearchRequest)
        {
            GetProductsByCategoryRequest productSearchRequest = GenerateProductSearchRequestFrom(jsonProductSearchRequest);
            GetProductsByCategoryResponse response = _cachedProductCatalogService.GetProductsByCategory(productSearchRequest);
            ProductSearchResultView productSearchResultView = GetProductSearchResultViewFrom(response);

            return Ok(productSearchResultView);
        }

        private GetProductsByCategoryRequest GenerateProductSearchRequestFrom(ProductSearchRequest jsonProductSearchRequest)
        {
            GetProductsByCategoryRequest productSearchRequest = new GetProductsByCategoryRequest();
            productSearchRequest.NumberOfResultsPerPage = _applicationSettings.NumberOfResultsPerPage;

            if (jsonProductSearchRequest != null)
            {
                productSearchRequest.Index = jsonProductSearchRequest.Index;
                productSearchRequest.CategoryId = jsonProductSearchRequest.CategoryId;
                productSearchRequest.SortBy = jsonProductSearchRequest.SortBy;

                foreach (var jsonRefinementGroup in jsonProductSearchRequest.RefinementGroups)
                {
                    switch ((RefinementGroupings)jsonRefinementGroup.GroupId)
                    {
                        case RefinementGroupings.Brand:
                            productSearchRequest.BrandIds = jsonRefinementGroup.SelectedRefinements;

                            break;
                        case RefinementGroupings.Color:
                            productSearchRequest.ColorIds = jsonRefinementGroup.SelectedRefinements;

                            break;
                        case RefinementGroupings.Size:
                            productSearchRequest.SizeIds = jsonRefinementGroup.SelectedRefinements;

                            break;
                        default:
                            break;
                    }
                }
            }

            return productSearchRequest;
        }

        public IActionResult Detail(int id)
        {
            ProductDetailView productDetailView = new ProductDetailView();
            GetProductRequest request = new GetProductRequest 
            { 
                ProductId = id 
            };
            GetProductResponse response = _cachedProductCatalogService.GetProduct(request);
            ProductView productView = response.Product;
            productDetailView.Product = productView;
            productDetailView.BasketSummary = base.GetBasketSummaryView();
            productDetailView.Categories = base.GetCategories();

            return View(productDetailView);
        }
    }
}