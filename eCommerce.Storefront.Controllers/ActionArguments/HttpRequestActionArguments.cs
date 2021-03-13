using Microsoft.AspNetCore.Http;

namespace eCommerce.Storefront.Controllers.ActionArguments
{
    public class HttpRequestActionArguments : IActionArguments
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpRequestActionArguments(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        
        public string GetValueForArgument(ActionArgumentKey key)
        {
            return _httpContextAccessor?.HttpContext?.Request?.Query?[key.ToString()];
        }
    }
}