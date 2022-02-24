using eCommerce.Storefront.Model.Products;

namespace eCommerce.Storefront.Repository.EntityFrameworkCore.Repositories.Interfaces
{
    public interface IProductRepository : IReadOnlyRepository<Product, long>
    {
    }
}