using Infrastructure.Domain;

namespace eCommerce.Storefront.Model.Products
{
    public class Brand : EntityBase<long>, IAggregateRoot, IProductAttribute
    {
        public string Name { get; set; }
        
        protected override void Validate()
        {
        }
    }
}