using System;
using System.Collections.Generic;
using Infrastructure.Domain;

namespace eCommerce.Storefront.Model.Products
{
    public class ProductTitle : EntityBase<long>, IAggregateRoot
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Brand Brand { get; set; }
        public Category Category { get; set; }
        public ProductColor Color { get; set; }
        public IEnumerable<Product> Products { get; set; }
        
        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}