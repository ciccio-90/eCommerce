using System;
using Infrastructure.Domain;

namespace eCommerce.Storefront.Model.Basket
{
    public interface IBasketRepository : IRepository<Basket, Guid>
    {
    }
}