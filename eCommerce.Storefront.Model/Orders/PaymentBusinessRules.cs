using Infrastructure.Domain;

namespace eCommerce.Storefront.Model.Orders
{
    public static class PaymentBusinessRules
    {
        public static readonly BusinessRule TransactionIdRequired = new BusinessRule() { Property = "TransactionId", Rule = "A payment must have a transaction id." };
        public static readonly BusinessRule MerchantRequired = new BusinessRule() { Property = "Merchant", Rule = "A payment must have a Merchant." };
        public static readonly BusinessRule AmountValid = new BusinessRule() { Property = "Amount", Rule = "A payment must be for a non negative amount." };
    }
}