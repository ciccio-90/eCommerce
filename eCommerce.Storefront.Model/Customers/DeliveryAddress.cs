using Infrastructure.Domain;

namespace eCommerce.Storefront.Model.Customers
{
    public class DeliveryAddress : EntityBase<int>
    {        
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string Name { get; set; }
        public Customer Customer { get; set; }

        protected override void Validate()
        {
            if (string.IsNullOrEmpty(AddressLine))
            {
                AddBrokenRule(DeliveryAddressBusinessRules.AddressLineRequired);
            }

            if (string.IsNullOrEmpty(City))
            {
                AddBrokenRule(DeliveryAddressBusinessRules.CityRequired);
            }

            if (string.IsNullOrEmpty(Country))
            {
                AddBrokenRule(DeliveryAddressBusinessRules.CountryRequired);
            }

            if (string.IsNullOrEmpty(ZipCode))
            {
                AddBrokenRule(DeliveryAddressBusinessRules.ZipCodeRequired);
            }

            if (string.IsNullOrEmpty(Name))
            {
                AddBrokenRule(DeliveryAddressBusinessRules.NameRequired);
            }

            if (Customer == null)
            {
                AddBrokenRule(DeliveryAddressBusinessRules.CustomerRequired);
            }
        }
    }
}
