using eCommerce.Storefront.Controllers.ViewModels;
using eCommerce.Storefront.Services.ViewModels;

namespace eCommerce.Storefront.Controllers
{
    public static class BasketMapper
    {
        public static BasketSummaryView ConvertToSummary(this BasketView basket)
        {
            return new BasketSummaryView
            {
                BasketTotal = basket.BasketTotal,
                NumberOfItems = basket.NumberOfItems
            };
        }
    }
}