using eCommerce.Storefront.Services.ViewModels;

namespace eCommerce.Storefront.Controllers.ViewModels.CustomerAccount
{
    public class CustomerOrderView : BaseCustomerAccountView
    {
        public OrderView Order { get; set; }
    }
}