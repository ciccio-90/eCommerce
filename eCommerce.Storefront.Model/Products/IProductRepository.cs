using Infrastructure.Domain;

namespace eCommerce.Storefront.Model.Products
{
    public interface IProductRepository : IReadOnlyRepository<Product, int>
    {
    }
}