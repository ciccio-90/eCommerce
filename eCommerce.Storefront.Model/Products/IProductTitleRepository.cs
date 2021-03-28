using Infrastructure.Domain;

namespace eCommerce.Storefront.Model.Products
{
    public interface IProductTitleRepository : IReadOnlyRepository<ProductTitle, long>
    {
    }
}