using eCommerce.Storefront.Services.ViewModels;

namespace eCommerce.Storefront.Services.Messaging.CustomerService
{
    public class DeliveryAddressModifyRequest
    {
        public string CustomerIdentityToken { get; set; }
        public DeliveryAddressView Address { get; set; }
    }
}