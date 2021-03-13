using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using eCommerce.Storefront.Model.Products;
using eCommerce.Storefront.Services.Cache.Specifications;
using eCommerce.Storefront.Services.Interfaces;
using eCommerce.Storefront.Services.Messaging.ProductCatalogService;
using eCommerce.Storefront.Services.ViewModels;
using Infrastructure.CacheStorage;

namespace eCommerce.Storefront.Services.Cache
{
    public class CachedProductCatalogService : ICachedProductCatalogService
    {
        private readonly ICacheStorage _cacheStorage;
        private readonly IProductCatalogService _productCatalogService;
        private readonly IProductTitleRepository _productTitleRepository;
        private readonly IProductRepository _productRepository;
        private readonly object _getTopSellingProductsLock;
        private readonly object _getAllProductTitlesLock;
        private readonly object _getAllProductsLock;
        private readonly object _getAllCategoriesLock;
        private readonly IMapper _mapper;

        public CachedProductCatalogService(ICacheStorage cacheStorage,
                                           IProductCatalogService productCatalogService,
                                           IProductTitleRepository productTitleRepository,
                                           IProductRepository productRepository,
                                           IMapper mapper)
        {
            _cacheStorage = cacheStorage;
            _productCatalogService = productCatalogService;
            _productTitleRepository = productTitleRepository;
            _productRepository = productRepository;
            _getTopSellingProductsLock = new object();
            _getAllProductTitlesLock = new object();
            _getAllProductsLock = new object();
            _getAllCategoriesLock = new object();
            _mapper = mapper;
        }

        private IEnumerable<ProductTitle> FindAllProductTitles()
        {
            lock (_getAllProductTitlesLock)
            {
                IEnumerable<ProductTitle> allProductTitles = _cacheStorage.Retrieve<IEnumerable<ProductTitle>>(CacheKeys.AllProductTitles.ToString());

                if (allProductTitles == null)
                {
                    allProductTitles = _productTitleRepository.FindAll().ToList();

                    _cacheStorage.Store(CacheKeys.AllProductTitles.ToString(), allProductTitles);
                }

                return allProductTitles;
            }
        }

        private IEnumerable<Product> FindAllProducts()
        {
            lock (_getAllProductsLock)
            {
                IEnumerable<Product> allProducts = _cacheStorage.Retrieve<IEnumerable<Product>>(CacheKeys.AllProducts.ToString());

                if (allProducts == null)
                {
                    allProducts = _productRepository.FindAll().ToList();

                    _cacheStorage.Store(CacheKeys.AllProducts.ToString(), allProducts);
                }

                return allProducts;
            }
        }

        public GetFeaturedProductsResponse GetFeaturedProducts()
        {
            lock (_getTopSellingProductsLock)
            {
                GetFeaturedProductsResponse response = new GetFeaturedProductsResponse();
                IEnumerable<ProductSummaryView> productViews = _cacheStorage.Retrieve<IEnumerable<ProductSummaryView>>(CacheKeys.TopSellingProducts.ToString());

                if (productViews == null)
                {
                    response = _productCatalogService.GetFeaturedProducts();

                    _cacheStorage.Store(CacheKeys.TopSellingProducts.ToString(), response.Products.ToList());
                }
                else
                {
                    response.Products = productViews;
                }

                return response;
            }
        }

        public GetProductsByCategoryResponse GetProductsByCategory(GetProductsByCategoryRequest request)
        {
            IProductSearchSpecification colourSpecification = new ProductIsInColorSpecification(request.ColorIds);
            IProductSearchSpecification brandSpecification = new ProductIsInBrandSpecification(request.BrandIds);
            IProductSearchSpecification sizeSpecification = new ProductIsInSizeSpecification(request.SizeIds);
            IProductSearchSpecification categorySpecification = new ProductIsInCategorySpecification(request.CategoryId);
            IEnumerable<Product> matchingProducts = FindAllProducts().Where(colourSpecification.IsSatisfiedBy)
                                                                     .Where(brandSpecification.IsSatisfiedBy)
                                                                     .Where(sizeSpecification.IsSatisfiedBy)
                                                                     .Where(categorySpecification.IsSatisfiedBy);

            switch (request.SortBy)
            {
                case ProductsSortBy.PriceLowToHigh:
                    matchingProducts = matchingProducts.OrderBy(p => p.Price);

                    break;
                case ProductsSortBy.PriceHighToLow:
                    matchingProducts = matchingProducts.OrderByDescending(p => p.Price);

                    break;
            }

            GetProductsByCategoryResponse response = CreateProductSearchResultFrom(matchingProducts, request);
            response.SelectedCategoryName = GetAllCategories().Categories.FirstOrDefault(c => c.Id == request.CategoryId)?.Name;                                

            return response;
        }
        
        public GetProductResponse GetProduct(GetProductRequest request)
        {
            GetProductResponse response = new GetProductResponse();
            response.Product = _mapper.Map<ProductTitle, ProductView>(FindAllProductTitles().FirstOrDefault(p => p.Id == request.ProductId));

            return response;
        }

        public GetAllCategoriesResponse GetAllCategories()
        {
            lock (_getAllCategoriesLock)
            {
                GetAllCategoriesResponse response = _cacheStorage.Retrieve<GetAllCategoriesResponse>(CacheKeys.AllCategories.ToString());

                if (response == null)
                {
                    response = _productCatalogService.GetAllCategories();
                    response.Categories = response.Categories.ToList();
                    
                    _cacheStorage.Store(CacheKeys.AllCategories.ToString(), response);
                }

                return response;
            }
        }

        public GetProductsByCategoryResponse CreateProductSearchResultFrom(IEnumerable<Product> productsMatchingRefinement, GetProductsByCategoryRequest request)
        {
            return _productCatalogService.CreateProductSearchResultFrom(productsMatchingRefinement, request);
        }
    }
}