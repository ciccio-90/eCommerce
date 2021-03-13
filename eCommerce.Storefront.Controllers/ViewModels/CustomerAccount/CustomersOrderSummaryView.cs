using System.Collections.Generic;
using eCommerce.Storefront.Services.ViewModels;

namespace eCommerce.Storefront.Controllers.ViewModels.CustomerAccount
{
    public class CustomersOrderSummaryView : BaseCustomerAccountView
    {
        public IEnumerable<OrderSummaryView> Orders { get; set; }
    }
}