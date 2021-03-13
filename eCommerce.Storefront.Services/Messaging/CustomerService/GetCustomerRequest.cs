namespace eCommerce.Storefront.Services.Messaging.CustomerService
{
    public class GetCustomerRequest
    {
        public string CustomerIdentityToken { get; set; }
        public bool LoadOrderSummary { get; set; }
    }
}