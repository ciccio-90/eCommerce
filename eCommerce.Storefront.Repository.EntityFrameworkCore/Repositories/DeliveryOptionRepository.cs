using System.Linq;
using eCommerce.Storefront.Model.Shipping;
using Infrastructure.EntityFrameworkCore;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Storefront.Repository.EntityFrameworkCore.Repositories
{
    public class DeliveryOptionRepository : Repository<DeliveryOption, long>, IDeliveryOptionRepository
    {
        public DeliveryOptionRepository(IUnitOfWork uow, DataContext dataContext) : base(uow, dataContext)
        {
        }

        public override IQueryable<DeliveryOption> AppendCriteria(IQueryable<DeliveryOption> criteria)
        {
            return criteria.Include(d => d.ShippingService);
        }
    }
}