namespace eCommerce.Storefront.Model.Products
{
    public class ProductSize : EntityBase<long>, IProductAttribute
    {
        public string Name { get; set; }
        
        protected override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                AddBrokenRule(new BusinessRule() { Property = nameof(Name), Rule = "A product size must have a name." });
            }
            else
            {
                if (Name.Length > 50)
                {
                    AddBrokenRule(new BusinessRule() { Property = nameof(Name), Rule = string.Format("A product size name cannot exceed {0} characters.", 50) });
                }
            }
        }
    }
}