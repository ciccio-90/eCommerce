using System.Collections.Generic;

namespace eCommerce.Storefront.Services.ViewModels
{
    public class CustomerView
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public IEnumerable<DeliveryAddressView> DeliveryAddressBook { get; set; }
    }
}