namespace eCommerce.Storefront.Controllers.DTOs
{
    public class BasketItemUpdateRequest
    {
        public int ProductId { get; set; }
        public int Qty { get; set; }
    }
}