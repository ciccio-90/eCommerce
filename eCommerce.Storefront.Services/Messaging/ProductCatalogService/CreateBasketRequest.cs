using System.Collections.Generic;

namespace eCommerce.Storefront.Services.Messaging.ProductCatalogService
{
    public class CreateBasketRequest
    {
        public CreateBasketRequest()
        {
            ProductsToAdd = new List<int>();
        }
        
        public IList<int> ProductsToAdd { get; set; }
    }
}