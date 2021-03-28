using Infrastructure.Domain;

namespace eCommerce.Storefront.Model.Products
{
    public class ProductColor : EntityBase<long>, IAggregateRoot, IProductAttribute
    {
        public string Name { get; set; }
        
        protected override void Validate()
        {
        }
    }
}