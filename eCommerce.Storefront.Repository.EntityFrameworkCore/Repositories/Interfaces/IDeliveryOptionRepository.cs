using eCommerce.Storefront.Model.Shipping;

namespace eCommerce.Storefront.Repository.EntityFrameworkCore.Repositories.Interfaces
{
    public interface IDeliveryOptionRepository : IReadOnlyRepository<DeliveryOption, long>
    {
    }
}