using eCommerce.Storefront.Model.Products;

namespace eCommerce.Storefront.Services.Cache.Specifications
{
    public class ProductIsInCategorySpecification : IProductSearchSpecification
    {
        private readonly int _categoryId;

        public ProductIsInCategorySpecification(int categoryId)
        {
            _categoryId = categoryId;
        }

        public bool IsSatisfiedBy(Product product)
        {
            return product.Category.Id == _categoryId;            
        }
    }
}