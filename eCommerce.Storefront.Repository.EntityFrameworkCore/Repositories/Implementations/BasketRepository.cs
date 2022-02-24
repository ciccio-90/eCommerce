using System;
using System.Linq;
using eCommerce.Storefront.Model.Basket;
using eCommerce.Storefront.Repository.EntityFrameworkCore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Storefront.Repository.EntityFrameworkCore.Repositories.Implementations
{
    public class BasketRepository : Repository<Basket, Guid>, IBasketRepository
    {
        public BasketRepository(IUnitOfWork uow, ShopDataContext dataContext) : base(uow, dataContext)
        {
        }

        public override IQueryable<Basket> AppendCriteria(IQueryable<Basket> criteria)
        {
            return criteria.Include(b => b.Items)
                           .ThenInclude(i => i.Product)
                           .ThenInclude(p => p.Title)
                           .Include(b => b.Items)
                           .ThenInclude(i => i.Product)
                           .ThenInclude(p => p.Size)
                           .Include(b => b.DeliveryOption)
                           .ThenInclude(d => d.ShippingService)
                           .Include(b => b.Customer);
        }
    }
}