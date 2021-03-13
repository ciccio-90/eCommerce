namespace eCommerce.Storefront.Services.Messaging.CustomerService
{
    public class ModifyCustomerRequest
    {
        public string CustomerIdentityToken { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
    }
}