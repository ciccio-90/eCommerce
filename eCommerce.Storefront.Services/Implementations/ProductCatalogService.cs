using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using eCommerce.Storefront.Model.Products;
using eCommerce.Storefront.Services.Interfaces;
using eCommerce.Storefront.Services.Messaging.ProductCatalogService;
using eCommerce.Storefront.Services.ViewModels;
using Infrastructure.Domain;

namespace eCommerce.Storefront.Services.Implementations
{
    public class ProductCatalogService : IProductCatalogService
    {
        private readonly IProductTitleRepository _productTitleRepository;
        private readonly IProductRepository _productRepository;
        private readonly IReadOnlyRepository<Category, int> _categoryRepository;
        private readonly IMapper _mapper;

        public ProductCatalogService(IProductTitleRepository productTitleRepository,
                                     IProductRepository productRepository,
                                     IReadOnlyRepository<Category, int> categoryRepository,
                                     IMapper mapper)
        {
            _productTitleRepository = productTitleRepository;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public GetAllCategoriesResponse GetAllCategories()
        {
            GetAllCategoriesResponse response = new GetAllCategoriesResponse();            
            response.Categories = _categoryRepository.FindAll().Select(c => _mapper.Map<Category, CategoryView>(c));
            
            return response;
        }

        public GetFeaturedProductsResponse GetFeaturedProducts()
        {
            GetFeaturedProductsResponse response = new GetFeaturedProductsResponse();
            response.Products = _productTitleRepository.FindBy(p => true, 0, 6).OrderByDescending(p => p.Price).Select(p => _mapper.Map<ProductTitle, ProductSummaryView>(p));

            return response;
        }

        public GetProductResponse GetProduct(GetProductRequest request)
        {
            GetProductResponse response = new GetProductResponse();
            ProductTitle productTitle = _productTitleRepository.FindBy(request.ProductId);            
            response.Product = _mapper.Map<ProductTitle, ProductView>(productTitle);

            return response;
        }

        public GetProductsByCategoryResponse GetProductsByCategory(GetProductsByCategoryRequest request)
        {
            Expression<Func<Product, bool>> productQuery = ProductSearchRequestQueryGenerator.CreateQueryFor(request);
            IEnumerable<Product> productsMatchingRefinement = GetAllProductsMatchingQueryAndSort(request, productQuery);
            GetProductsByCategoryResponse response = CreateProductSearchResultFrom(productsMatchingRefinement, request);
            response.SelectedCategoryName = _categoryRepository.FindBy(request.CategoryId).Name;

            return response;
        }

        private IEnumerable<Product> GetAllProductsMatchingQueryAndSort(GetProductsByCategoryRequest request, Expression<Func<Product, bool>> productQuery)
        {
            IEnumerable<Product> productsMatchingRefinement = _productRepository.FindBy(productQuery);

            switch (request.SortBy)
            {
                case ProductsSortBy.PriceLowToHigh:
                    productsMatchingRefinement = productsMatchingRefinement.OrderBy(p => p.Price);
                    
                    break;
                case ProductsSortBy.PriceHighToLow:
                    productsMatchingRefinement = productsMatchingRefinement.OrderByDescending(p => p.Price);
                    
                    break;
            }
            
            return productsMatchingRefinement;
        }

        public GetProductsByCategoryResponse CreateProductSearchResultFrom(IEnumerable<Product> productsMatchingRefinement, GetProductsByCategoryRequest request)
        {
            GetProductsByCategoryResponse productSearchResultView = new GetProductsByCategoryResponse();
            IEnumerable<ProductTitle> productsFound = productsMatchingRefinement.Select(p => p.Title).Distinct();
            productSearchResultView.SelectedCategory = request.CategoryId;
            productSearchResultView.NumberOfTitlesFound = productsFound.Count();
            productSearchResultView.TotalNumberOfPages = NoOfResultPagesGiven(request.NumberOfResultsPerPage, productSearchResultView.NumberOfTitlesFound);
            productSearchResultView.RefinementGroups = GenerateAvailableProductRefinementsFrom(productsFound);
            productSearchResultView.Products = CropProductListToSatisfyGivenIndex(request.Index, request.NumberOfResultsPerPage, productsFound);

            return productSearchResultView;
        }

        private IEnumerable<ProductSummaryView> CropProductListToSatisfyGivenIndex(int pageIndex, int numberOfResultsPerPage, IEnumerable<ProductTitle> productsFound)
        {
            if (pageIndex > 1)
            {
                int numToSkip = (pageIndex - 1) * numberOfResultsPerPage;

                return _mapper.Map<IEnumerable<ProductTitle>, IEnumerable<ProductSummaryView>>(productsFound.OrderBy(p => p.Name).Skip(numToSkip).Take(numberOfResultsPerPage));
            }
            else
            {
                return _mapper.Map<IEnumerable<ProductTitle>, IEnumerable<ProductSummaryView>>(productsFound.OrderBy(p => p.Name).Take(numberOfResultsPerPage));
            }
        }

        private int NoOfResultPagesGiven(int numberOfResultsPerPage, int numberOfTitlesFound)
        {
            if (numberOfTitlesFound < numberOfResultsPerPage)
            {
                return 1;
            }
            else
            {
                return (numberOfTitlesFound / numberOfResultsPerPage) + (numberOfTitlesFound % numberOfResultsPerPage);
            }
        }

        private IList<RefinementGroup> GenerateAvailableProductRefinementsFrom(IEnumerable<ProductTitle> productsFound)
        {
            var brandsRefinementGroup = ConvertToRefinementGroup(productsFound.Select(p => p.Brand).Distinct().ToList().ConvertAll<IProductAttribute>(b => (IProductAttribute)b), RefinementGroupings.Brand);
            var colorsRefinementGroup = ConvertToRefinementGroup(productsFound.Select(p => p.Color).Distinct().ToList().ConvertAll<IProductAttribute>(c => (IProductAttribute)c), RefinementGroupings.Color);
            var sizesRefinementGroup = ConvertToRefinementGroup((from p in productsFound
                                                                 from si in p.Products
                                                                 select si.Size).Distinct().ToList().ConvertAll<IProductAttribute>(s => (IProductAttribute)s), RefinementGroupings.Size);
            IList<RefinementGroup> refinementGroups = new List<RefinementGroup>();
            
            refinementGroups.Add(brandsRefinementGroup);
            refinementGroups.Add(colorsRefinementGroup);
            refinementGroups.Add(sizesRefinementGroup);
            
            return refinementGroups;
        }

        private RefinementGroup ConvertToRefinementGroup(IEnumerable<IProductAttribute> productAttributes, RefinementGroupings refinementGroupType)
        {
            RefinementGroup refinementGroup = new RefinementGroup
            {
                Name = refinementGroupType.ToString(),
                GroupId = (int)refinementGroupType
            };
            
            refinementGroup.Refinements = _mapper.Map<IEnumerable<IProductAttribute>, IEnumerable<Refinement>>(productAttributes);

            return refinementGroup;
        }
    }
}