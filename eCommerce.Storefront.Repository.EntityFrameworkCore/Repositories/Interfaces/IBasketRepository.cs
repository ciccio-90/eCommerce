using System;
using eCommerce.Storefront.Model.Basket;

namespace eCommerce.Storefront.Repository.EntityFrameworkCore.Repositories.Interfaces
{
    public interface IBasketRepository : IRepository<Basket, Guid>
    {
    }
}