using System;

namespace eCommerce.Storefront.Services.ViewModels
{
    public class OrderSummaryView
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public bool IsSubmitted { get; set; }
    }
}