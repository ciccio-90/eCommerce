using Infrastructure.Domain;

namespace eCommerce.Storefront.Model.Basket
{
    public static class BasketBusinessRules
    {
        public static readonly BusinessRule DeliveryOptionRequired = new BusinessRule() { Property = "DeliveryOption", Rule = "A basket must have a valid delivery option." };
        public static readonly BusinessRule ItemInvalid = new BusinessRule() { Property = "Item", Rule = "A basket cannot have any invalid items." };
    }
}