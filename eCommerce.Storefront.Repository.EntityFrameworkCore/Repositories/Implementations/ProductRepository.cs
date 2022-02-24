using System.Linq;
using eCommerce.Storefront.Model.Products;
using eCommerce.Storefront.Repository.EntityFrameworkCore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Storefront.Repository.EntityFrameworkCore.Repositories.Implementations
{
    public class ProductRepository : Repository<Product, long>, IProductRepository
    {
        public ProductRepository(IUnitOfWork uow, ShopDataContext dataContext) : base(uow, dataContext)
        {
        }

        public override IQueryable<Product> AppendCriteria(IQueryable<Product> criteria)
        {
            return criteria.Include(p => p.Size)
                           .Include(p => p.Title)
                           .Include(p => p.Title.Brand)
                           .Include(p => p.Title.Color)
                           .Include(p => p.Title.Category);
        }
    }
}