using System.Collections.Generic;

namespace eCommerce.Storefront.Controllers.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool IsAuthenticated { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}