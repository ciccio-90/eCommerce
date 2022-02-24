namespace eCommerce.Storefront.Model.Payments
{
    public class OrderItemPaymentRequest
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }
    }
}