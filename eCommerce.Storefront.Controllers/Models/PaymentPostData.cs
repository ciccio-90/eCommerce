using System.Collections.Specialized;

namespace eCommerce.Storefront.Controllers.Models
{
    public class PaymentPostData
    {
        public string PaymentPostToUrl { get; set; }
        public NameValueCollection PostDataAndValue { get; set; }
    }
}