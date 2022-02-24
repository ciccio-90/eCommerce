using System.Collections.Generic;

namespace eCommerce.Storefront.Model.Shipping
{
    public class Courier : EntityBase<long>
    {
        private readonly string _name;
        private readonly IEnumerable<ShippingService> _services;

        public Courier()
        {
        }
        
        public Courier(string name, IEnumerable<ShippingService> services)
        {
            _name = name;
            _services = services;
        }
        
        public string Name
        {
            get { return _name; }
        }
        
        public IEnumerable<ShippingService> Services
        {
            get { return _services; }
        }
        
        protected override void Validate()
        {
        }
    }
}