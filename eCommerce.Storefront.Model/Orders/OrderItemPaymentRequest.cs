namespace eCommerce.Storefront.Model.Orders
{
    public class OrderItemPaymentRequest
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }
    }
}