using System.Collections.Generic;
using eCommerce.Storefront.Services.ViewModels;

namespace eCommerce.Storefront.Services.Messaging.ProductCatalogService
{
    public class GetProductsByCategoryResponse
    {
        public string SelectedCategoryName { get; set; }
        public int SelectedCategory { get; set; }

        public IEnumerable<RefinementGroup> RefinementGroups { get; set; }

        public int NumberOfTitlesFound { get; set; }
        public int TotalNumberOfPages { get; set; }
        public int CurrentPage { get; set; }
        
        public IEnumerable<ProductSummaryView> Products { get; set; }
    }
}