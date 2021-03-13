namespace eCommerce.Storefront.Services.Messaging.ProductCatalogService
{
    public class ProductQtyUpdateRequest
    {
        public int ProductId { get; set; }
        public int NewQty { get; set; }
    }
}