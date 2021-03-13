using System;
using System.Linq;
using Infrastructure.EntityFrameworkCore;
using Infrastructure.UnitOfWork;
using eCommerce.Storefront.Model.Basket;
using Microsoft.EntityFrameworkCore;

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
                           .Include(b => b.DeliveryOption)
                           .ThenInclude(d => d.ShippingService);
        }
    }
}