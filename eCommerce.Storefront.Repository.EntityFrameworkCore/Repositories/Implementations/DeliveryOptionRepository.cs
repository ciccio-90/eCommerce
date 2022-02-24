using System.Linq;
using eCommerce.Storefront.Model.Shipping;
using eCommerce.Storefront.Repository.EntityFrameworkCore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Storefront.Repository.EntityFrameworkCore.Repositories.Implementations
{
    public class DeliveryOptionRepository : Repository<DeliveryOption, long>, IDeliveryOptionRepository
    {
        public DeliveryOptionRepository(IUnitOfWork uow, ShopDataContext dataContext) : base(uow, dataContext)
        {
        }

        public override IQueryable<DeliveryOption> AppendCriteria(IQueryable<DeliveryOption> criteria)
        {
            return criteria.Include(d => d.ShippingService);
        }
    }
}