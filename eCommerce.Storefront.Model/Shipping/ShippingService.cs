using System;
using Infrastructure.Domain;

namespace eCommerce.Storefront.Model.Shipping
{
    public class ShippingService : EntityBase<long>
    {
        private readonly string _code;
        private readonly string _description;
        private readonly Courier _courier;

        public ShippingService()
        {
        }
        
        public ShippingService(string code, string description, Courier courier)
        {
            _code = code;
            _description = description;
            _courier = courier;
        }
        
        public string Code
        {
            get { return _code; }
        }
        
        public string Description
        {
            get { return _description; }
        }
        
        public Courier Courier
        {
            get { return _courier; }
        }
        
        protected override void Validate()
        {
        }
    }
}