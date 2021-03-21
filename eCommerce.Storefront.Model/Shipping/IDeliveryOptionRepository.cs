using Infrastructure.Domain;

namespace eCommerce.Storefront.Model.Shipping
{
    public interface IDeliveryOptionRepository : IReadOnlyRepository<DeliveryOption, int>
    {
    }
}