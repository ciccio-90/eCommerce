namespace eCommerce.Storefront.Services.Messaging.CustomerService
{
    public class CreateCustomerRequest
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
    }
}