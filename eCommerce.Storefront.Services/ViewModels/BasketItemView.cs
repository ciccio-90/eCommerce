namespace eCommerce.Storefront.Services.ViewModels
{
    public class BasketItemView
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductSizeName { get; set; }
        public int ProductTitleId { get; set; }
        public int Qty { get; set; }
        public string ProductPrice { get; set; }
        public string LineTotal { get; set; }
    }
}