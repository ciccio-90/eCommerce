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
                base.AddBrokenRule(DeliveryAddressBusinessRules.AddressLineRequired);
            }

            if (string.IsNullOrEmpty(City))
            {
                base.AddBrokenRule(DeliveryAddressBusinessRules.CityRequired);
            }

            if (string.IsNullOrEmpty(Country))
            {
                base.AddBrokenRule(DeliveryAddressBusinessRules.CountryRequired);
            }

            if (string.IsNullOrEmpty(ZipCode))
            {
                base.AddBrokenRule(DeliveryAddressBusinessRules.ZipCodeRequired);
            }

            if (string.IsNullOrEmpty(Name))
            {
                base.AddBrokenRule(DeliveryAddressBusinessRules.NameRequired);
            }

            if (Customer == null)
            {
                base.AddBrokenRule(DeliveryAddressBusinessRules.CustomerRequired);
            }
        }
    }
}