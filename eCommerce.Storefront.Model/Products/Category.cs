using Infrastructure.Domain;

namespace eCommerce.Storefront.Model.Products
{
    public class Category : EntityBase<long>, IProductAttribute
    {
        public string Name { get; set; }
        
        protected override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                AddBrokenRule(new BusinessRule() { Property = nameof(Name), Rule = "A category must have a name." });
            }
            else
            {
                if (Name.Length > 50)
                {
                    AddBrokenRule(new BusinessRule() { Property = nameof(Name), Rule = string.Format("A category name cannot exceed {0} characters.", 50) });
                }
            }
        }
    }
}