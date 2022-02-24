namespace eCommerce.Storefront.Model
{
    public static class PriceHelper
    {
        public static string FormatMoney(this decimal price, string currencySymbol)
        {
            return $"{currencySymbol}{price}";
        }
    }
}