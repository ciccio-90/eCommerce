using eCommerce.Storefront.Services.ViewModels;

namespace eCommerce.Storefront.Services.Messaging.CustomerService
{
    public class DeliveryAddressAddRequest
    {
        public string CustomerEmail { get; set; }
        public DeliveryAddressView Address { get; set; }
    }
}