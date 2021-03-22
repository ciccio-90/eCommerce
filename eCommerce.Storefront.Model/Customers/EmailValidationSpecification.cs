using System.Text.RegularExpressions;

namespace eCommerce.Storefront.Model.Customers
{
    public class EmailValidationSpecification
    {
        private Regex _emailRegex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");

        public bool IsSatisfiedBy(string email)
        {            
            return _emailRegex.IsMatch(email ?? string.Empty);
        }
    }
}
