using Infrastructure.Domain;

namespace eCommerce.Storefront.Model.Customers
{
    public static class DeliveryAddressBusinessRules
    {
        public static readonly BusinessRule AddressLineRequired = new BusinessRule() { Property = "AddressLine", Rule = "The line of a delivery address is required." };
        public static readonly BusinessRule CityRequired = new BusinessRule() { Property = "City", Rule = "A delivery address must have a city." };
        public static readonly BusinessRule CountryRequired = new BusinessRule() { Property = "Country", Rule = "A delivery address must have a country." };
        public static readonly BusinessRule ZipCodeRequired = new BusinessRule() { Property = "ZipCode", Rule = "A delivery address must have a zip code." };
        public static readonly BusinessRule NameRequired = new BusinessRule() { Property = "Name", Rule = "A delivery address must have a name." };
        public static readonly BusinessRule CustomerRequired = new BusinessRule() { Property = "Customer", Rule = "A delivery address must be associated with a customer." };
    }
}