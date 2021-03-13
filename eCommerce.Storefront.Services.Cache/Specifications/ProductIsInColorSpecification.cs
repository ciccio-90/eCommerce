using System.Linq;
using eCommerce.Storefront.Model.Products;

namespace eCommerce.Storefront.Services.Cache.Specifications
{
    public class ProductIsInColorSpecification : IProductSearchSpecification
    {
        private readonly int[] _colourIds;

        public ProductIsInColorSpecification(int[] colourIds)
        {
            _colourIds = colourIds;
        }

        public bool IsSatisfiedBy(Product product)
        {
            if (_colourIds.Count() > 0)
            {
                return _colourIds.Any(c => c == product.Title.Color.Id);
            }
            
            return true;
        }
    }
}