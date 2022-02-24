using Microsoft.AspNetCore.Http;

namespace eCommerce.Storefront.Controllers
{
    public static class UrlHelper
    {
        public static string Resolve(this HttpContext httpContext, string resource)
        {
            return string.Format("{0}://{1}{2}", httpContext.Request.Scheme, httpContext.Request.Host.Value, resource);
        }
    }
}