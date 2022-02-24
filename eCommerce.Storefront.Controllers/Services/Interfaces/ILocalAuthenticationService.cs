using System.Collections.Generic;
using System.Threading.Tasks;

namespace eCommerce.Storefront.Controllers.Services.Interfaces
{
    public interface ILocalAuthenticationService
    {
        Task<User> Login(string email, string password);
        Task<User> RegisterUser(string email, string password, bool confirmEmail, IEnumerable<string> roles);
    }
}