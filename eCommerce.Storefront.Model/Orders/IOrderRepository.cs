using Infrastructure.Domain;

namespace eCommerce.Storefront.Model.Orders
{
    public interface IOrderRepository : IRepository<Order, int>
    {         
    }
}