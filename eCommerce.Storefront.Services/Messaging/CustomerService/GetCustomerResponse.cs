using System.Collections.Generic;
using eCommerce.Storefront.Services.ViewModels;

namespace eCommerce.Storefront.Services.Messaging.CustomerService
{
    public class GetCustomerResponse
    {
        public bool CustomerFound { get; set; }
        public CustomerView Customer { get; set; }
        public IEnumerable<OrderSummaryView> Orders { get; set; }
        public BasketView Basket { get; set; }
    }
}