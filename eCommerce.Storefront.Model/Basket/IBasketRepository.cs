using System;
using System.Collections.Generic;
using Infrastructure.Domain;

namespace eCommerce.Storefront.Model.Basket
{
    public interface IBasketRepository : IRepository<Basket, Guid>
    {
        void RemoveBasketItems(IEnumerable<BasketItem> basketItems);
    }
}