namespace eCommerce.Storefront.Services.Messaging.ProductCatalogService
{
    public class GetProductsByCategoryRequest
    {
        public GetProductsByCategoryRequest()
        {
            ColorIds = new int[0];
            BrandIds = new int[0];
            SizeIds = new int[0];
        }

        public int CategoryId { get; set; }

        public int[] ColorIds { get; set; }
        public int[] BrandIds { get; set; }
        public int[] SizeIds { get; set; }
        
        public ProductsSortBy SortBy { get; set; }
        public int Index { get; set; }
        public int NumberOfResultsPerPage { get; set; }
    }
}