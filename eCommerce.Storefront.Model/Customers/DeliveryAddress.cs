namespace eCommerce.Storefront.Model.Customers
{
    public class DeliveryAddress : EntityBase<long>
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
            if (string.IsNullOrWhiteSpace(AddressLine))
            {
                AddBrokenRule(new BusinessRule() { Property = nameof(AddressLine), Rule = "The line of a delivery address is required." });
            }

            if (string.IsNullOrWhiteSpace(City))
            {
                AddBrokenRule(new BusinessRule() { Property = nameof(City), Rule = "A delivery address must have a city." });
            }

            if (string.IsNullOrWhiteSpace(Country))
            {
                AddBrokenRule(new BusinessRule() { Property = nameof(Country), Rule = "A delivery address must have a country." });
            }

            if (string.IsNullOrWhiteSpace(ZipCode))
            {
                AddBrokenRule(new BusinessRule() { Property = nameof(ZipCode), Rule = "A delivery address must have a zip code." });
            }

            if (string.IsNullOrWhiteSpace(Name))
            {
                AddBrokenRule(new BusinessRule() { Property = nameof(Name), Rule = "A delivery address must have a name." });
            }

            if (Customer == null)
            {
                AddBrokenRule(new BusinessRule() { Property = nameof(Customer), Rule = "A delivery address must be associated with a customer." });
            }
        }
    }
}
