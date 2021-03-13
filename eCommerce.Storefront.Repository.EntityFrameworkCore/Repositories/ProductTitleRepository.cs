using System.Linq;
using Infrastructure.EntityFrameworkCore;
using Infrastructure.UnitOfWork;
using eCommerce.Storefront.Model.Products;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Storefront.Repository.EntityFrameworkCore.Repositories
{
    public class ProductTitleRepository : Repository<ProductTitle, int>, IProductTitleRepository
    {
        public ProductTitleRepository(IUnitOfWork uow, DataContext dataContext) : base(uow, dataContext)
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