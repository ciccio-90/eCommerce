namespace eCommerce.Storefront.Services.Messaging.CustomerService
{
    public class ModifyCustomerRequest
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string CurrentEmail { get; set; }
        public string NewEmail { get; set; }
    }
}