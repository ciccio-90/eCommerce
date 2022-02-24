using System.Linq;
using eCommerce.Storefront.Model.Products;
using eCommerce.Storefront.Repository.EntityFrameworkCore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Storefront.Repository.EntityFrameworkCore.Repositories.Implementations
{
    public class ProductTitleRepository : Repository<ProductTitle, long>, IProductTitleRepository
    {
        public ProductTitleRepository(IUnitOfWork uow, ShopDataContext dataContext) : base(uow, dataContext)
        {
        }

        public override IQueryable<ProductTitle> AppendCriteria(IQueryable<ProductTitle> criteria)
        {
            return criteria.Include(p => p.Brand)
                           .Include(p => p.Color)
                           .Include(p => p.Category)
                           .Include(p => p.Products)
                           .ThenInclude(p => p.Size);
        }
    }
}