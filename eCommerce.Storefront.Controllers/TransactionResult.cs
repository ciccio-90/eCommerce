namespace eCommerce.Storefront.Controllers
{
    public class TransactionResult
    {
        public string PaymentMerchant { get; set; }
        public bool PaymentOk { get; set; }
        public string PaymentToken { get; set; }
        public decimal Amount { get; set; }
    }
}