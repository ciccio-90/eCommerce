using eCommerce.Storefront.Controllers.ActionArguments;
using Infrastructure.Authentication;
using eCommerce.Storefront.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Storefront.Controllers.Controllers
{
    public abstract class BaseAccountController : Controller
    {
        protected readonly ILocalAuthenticationService _authenticationService;
        protected readonly ICustomerService _customerService;
        protected readonly ICookieAuthentication _cookieAuthentication;
        protected readonly IActionArguments _actionArguments;

        protected BaseAccountController(ILocalAuthenticationService authenticationService,
                                        ICustomerService customerService,
                                        ICookieAuthentication cookieAuthentication,
                                        IActionArguments actionArguments)
        {
            _authenticationService = authenticationService;
            _customerService = customerService;
            _cookieAuthentication = cookieAuthentication;
            _actionArguments = actionArguments;
        }

        public IActionResult RedirectBasedOn(string returnUrl)
        {
            if (returnUrl == ActionArgumentKey.GoToCheckout.ToString())
            {
                return RedirectToAction("Checkout", "Checkout");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionArgumentKey GetReturnActionFrom(string returnUrl)
        {
            if (!string.IsNullOrWhiteSpace(returnUrl) && returnUrl.ToLower().Contains("checkout"))
            {
                return ActionArgumentKey.GoToCheckout;
            }
            else
            {
                return ActionArgumentKey.GoToAccount;
            }
        }
    }     
}