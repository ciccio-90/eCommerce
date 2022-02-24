using System.Collections.Generic;
using System.Threading.Tasks;

namespace eCommerce.Storefront.Controllers.Services.Interfaces
{
    public interface ICookieAuthentication
    {
        Task SetAuthenticationToken(string email, IEnumerable<string> roles);
        string GetAuthenticationToken();
        Task SignOut();
    }
}