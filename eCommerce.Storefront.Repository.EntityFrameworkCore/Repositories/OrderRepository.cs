using System.Linq;
using Infrastructure.EntityFrameworkCore;
using Infrastructure.UnitOfWork;
using eCommerce.Storefront.Model.Orders;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Storefront.Repository.EntityFrameworkCore.Repositories
{
    public class OrderRepository : Repository<Order, long>, IOrderRepository
    {
        public OrderRepository(IUnitOfWork uow, DataContext dataContext) : base(uow, dataContext)
        {
        }

        public override IQueryable<Order> AppendCriteria(IQueryable<Order> criteria)
        {
            return criteria.Include(o => o.Items)
                           .ThenInclude(i => i.Product)
                           .ThenInclude(p => p.Size)
                           .Include(o => o.Items)
                           .ThenInclude(i => i.Product)
                           .ThenInclude(p => p.Title)
                           .Include(o => o.ShippingService)
                           .ThenInclude(s => s.Courier)
                           .Include(o => o.DeliveryAddress)
                           .Include(o => o.Customer)                           
                           .ThenInclude(c => c.DeliveryAddressBook);
        }
    }
}