using Infrastructure.Domain;

namespace eCommerce.Storefront.Model.Products
{
    public class ProductSize : EntityBase<int>, IAggregateRoot, IProductAttribute
    {
        public string Name { get; set; }
        
        protected override void Validate()
        {
        }
    }
}