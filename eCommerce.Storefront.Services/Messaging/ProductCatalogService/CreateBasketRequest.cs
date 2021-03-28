using System.Collections.Generic;

namespace eCommerce.Storefront.Services.Messaging.ProductCatalogService
{
    public class CreateBasketRequest
    {
        public CreateBasketRequest()
        {
            ProductsToAdd = new List<long>();
        }
        
        public IList<long> ProductsToAdd { get; set; }
        public string CustomerEmail { get; set; }
    }
}