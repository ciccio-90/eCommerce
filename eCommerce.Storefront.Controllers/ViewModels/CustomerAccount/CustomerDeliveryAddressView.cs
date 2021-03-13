using eCommerce.Storefront.Services.ViewModels;

namespace eCommerce.Storefront.Controllers.ViewModels.CustomerAccount
{
    public class CustomerDeliveryAddressView : BaseCustomerAccountView
    {
        public CustomerView CustomerView { get; set; }
        public DeliveryAddressView Address { get; set; }
    }
}