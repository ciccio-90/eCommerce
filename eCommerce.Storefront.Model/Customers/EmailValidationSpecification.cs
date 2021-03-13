using System.Text.RegularExpressions;

namespace eCommerce.Storefront.Model.Customers
{
    public class EmailValidationSpecification
    {
        private static Regex _emailregex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");

        public bool IsSatisfiedBy(string email)
        {            
            return _emailregex.IsMatch(email ?? string.Empty);
        }
    }
}