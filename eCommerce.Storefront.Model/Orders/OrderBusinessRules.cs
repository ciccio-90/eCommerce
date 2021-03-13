using Infrastructure.Domain;

namespace eCommerce.Storefront.Model.Orders
{
    public static class OrderBusinessRules
    {
        public static readonly BusinessRule CreatedDateRequired = new BusinessRule() { Property = "CreatedDate", Rule = "An order must have a created date." };
        public static readonly BusinessRule PaymentTransactionIdRequired = new BusinessRule() { Property = "PaymentTransactionId", Rule = "If an order is set as paid it must have a corresponding payment transaction id." };
        public static readonly BusinessRule CustomerRequired = new BusinessRule() { Property = "Customer", Rule = "An order must be associated with a customer." };
        public static readonly BusinessRule DeliveryAddressRequired = new BusinessRule() { Property = "DeliveryAddress", Rule = "An order must have a valid delilvery address." };
        public static readonly BusinessRule ItemsRequired = new BusinessRule() { Property = "Items", Rule = "An order must contain at least one order item." };
        public static readonly BusinessRule ShippingServiceRequired = new BusinessRule() { Property = "ShippingService", Rule = "An order must have a shipping service set." };
    }
}