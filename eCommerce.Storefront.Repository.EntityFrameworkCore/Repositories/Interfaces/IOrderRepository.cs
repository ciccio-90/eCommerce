using eCommerce.Storefront.Model.Orders;

namespace eCommerce.Storefront.Repository.EntityFrameworkCore.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<Order, long>
    {         
    }
}