namespace eCommerce.Storefront.Model.Products
{
    public class Brand : EntityBase<long>, IProductAttribute
    {
        public string Name { get; set; }
        
        protected override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                AddBrokenRule(new BusinessRule() { Property = nameof(Name), Rule = "A brand must have a name." });
            }
            else
            {
                if (Name.Length > 50)
                {
                    AddBrokenRule(new BusinessRule() { Property = nameof(Name), Rule = string.Format("A brand name cannot exceed {0} characters.", 50) });
                }
            }
        }
    }
}