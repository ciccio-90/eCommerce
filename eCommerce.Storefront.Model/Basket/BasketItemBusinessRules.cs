using Infrastructure.Domain;

namespace eCommerce.Storefront.Model.Basket
{
    public static class BasketItemBusinessRules
    {
        public static readonly BusinessRule BasketRequired = new BusinessRule() { Property = "Basket", Rule = "A basket item must be related to a basket." };
        public static readonly BusinessRule ProductRequired = new BusinessRule() { Property = "Product", Rule = "A basket item must be related to a product." };
        public static readonly BusinessRule QtyInvalid = new BusinessRule() { Property = "Qty", Rule = "The quantity of a basket item cannot be negative." };
    }
}