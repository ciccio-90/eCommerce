using System.Collections.Generic;
using eCommerce.Storefront.Services.ViewModels;

namespace eCommerce.Storefront.Services.Messaging.ProductCatalogService
{
    public class GetAllDispatchOptionsResponse
    {
        public IEnumerable<DeliveryOptionView> DeliveryOptions { get; set; }
    }
}