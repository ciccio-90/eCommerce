using System.Linq;
using Infrastructure.EntityFrameworkCore;
using Infrastructure.UnitOfWork;
using eCommerce.Storefront.Model.Products;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Storefront.Repository.EntityFrameworkCore.Repositories
{
    public class ProductRepository : Repository<Product, int>, IProductRepository
    {
        public ProductRepository(IUnitOfWork uow, DataContext dataContext) : base(uow, dataContext)
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