using eCommerce.Storefront.Services.ViewModels;

namespace eCommerce.Storefront.Services.Messaging.CustomerService
{
    public class DeliveryAddressAddRequest
    {
        public string CustomerIdentityToken { get; set; }
        public DeliveryAddressView Address { get; set; }
    }
}