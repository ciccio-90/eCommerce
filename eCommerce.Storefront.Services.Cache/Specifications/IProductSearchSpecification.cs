using eCommerce.Storefront.Model.Products;

namespace eCommerce.Storefront.Services.Cache.Specifications
{
    public interface IProductSearchSpecification
    {
        bool IsSatisfiedBy(Product product);
    }
}