using System.Linq;
using eCommerce.Storefront.Model.Products;

namespace eCommerce.Storefront.Services.Cache.Specifications
{
    public class ProductIsInBrandSpecification : IProductSearchSpecification 
    {
        private readonly int[] _brandIds;

        public ProductIsInBrandSpecification(int[] brandIds)
        {
            _brandIds = brandIds;            
        }

        public bool IsSatisfiedBy(Product product)
        {
            if (_brandIds.Count() > 0)
            {
                return _brandIds.Any(b => b == product.Title.Brand.Id);
            }
            
            return true;
        }
    }
}