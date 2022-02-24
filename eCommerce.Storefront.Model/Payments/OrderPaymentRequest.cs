using System.Collections.Generic;

namespace eCommerce.Storefront.Model.Payments
{
    public class OrderPaymentRequest
    {
        public decimal Total { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerSecondName { get; set; }
        public decimal ShippingCharge { get; set; }
        public string DeliveryAddressAddressLine { get; set; }
        public string DeliveryAddressCity { get; set; }
        public string DeliveryAddressState { get; set; }
        public string DeliveryAddressCountry { get; set; }
        public string DeliveryAddressZipCode { get; set; }
        public int Id { get; set; }
        public IEnumerable<OrderItemPaymentRequest> Items { get; set; }
        public string CurrencyCode { get; set; }
    }
}