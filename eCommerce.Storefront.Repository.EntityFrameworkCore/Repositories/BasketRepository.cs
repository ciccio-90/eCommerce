using System;
using System.Linq;
using Infrastructure.EntityFrameworkCore;
using Infrastructure.UnitOfWork;
using eCommerce.Storefront.Model.Basket;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace eCommerce.Storefront.Repository.EntityFrameworkCore.Repositories
{
    public class BasketRepository : Repository<Basket, Guid>, IBasketRepository
    {
        public BasketRepository(IUnitOfWork uow, DataContext dataContext) : base(uow, dataContext)
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
                           .ThenInclude(d => d.ShippingService);
        }

        public void RemoveBasketItems(IEnumerable<BasketItem> basketItems)
        {
            _dataContext.Set<BasketItem>().RemoveRange(basketItems);
        }
    }
}