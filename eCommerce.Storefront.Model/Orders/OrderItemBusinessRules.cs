using Infrastructure.Domain;

namespace eCommerce.Storefront.Model.Orders
{
    public static class OrderItemBusinessRules
    {
        public static readonly BusinessRule OrderRequired = new BusinessRule() { Property = "OrderRequired", Rule = "An order item must be associated with an order." };
        public static readonly BusinessRule PriceNonNegative = new BusinessRule() { Property = "Price", Rule = "An order item must have a non negative price value." };
        public static readonly BusinessRule QtyNonNegative = new BusinessRule() { Property = "Qty", Rule = "An order item must have a positive qty value." };
        public static readonly BusinessRule ProductRequired = new BusinessRule() { Property = "Product", Rule = "An order item must be associated with a valid product." };
    }
}