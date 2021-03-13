using System.Collections.Generic;
using eCommerce.Storefront.Services.ViewModels;

namespace eCommerce.Storefront.Controllers.ViewModels.Checkout
{
    public class OrderConfirmationView
    {
        public BasketView Basket { get; set; }
        public IEnumerable<DeliveryAddressView> DeliveryAddresses { get; set; }
    }
}