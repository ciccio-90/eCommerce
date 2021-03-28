namespace eCommerce.Storefront.Services.Messaging.CustomerService
{
    public class GetCustomerRequest
    {
        public string CustomerEmail { get; set; }
        public bool LoadOrderSummary { get; set; }
        public bool LoadBasketSummary { get; set; }
    }
}