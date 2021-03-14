using System.Threading.Tasks;
using eCommerce.Storefront.Controllers.ActionArguments;
using eCommerce.Storefront.Controllers.ViewModels.Account;
using Infrastructure.Authentication;
using eCommerce.Storefront.Services.Interfaces;
using eCommerce.Storefront.Services.Messaging.CustomerService;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Domain;

namespace eCommerce.Storefront.Controllers.Controllers
{
    public class AccountLogOnController : BaseAccountController
    {
        public AccountLogOnController(ILocalAuthenticationService authenticationService,
                                      ICustomerService customerService,
                                      ICookieAuthentication cookieAuthentication,
                                      IActionArguments actionArguments) : base(authenticationService, 
                                                                               customerService,
                                                                               cookieAuthentication, 
                                                                               actionArguments)
        {
        }

        public IActionResult LogOn()
        {
            AccountView accountView = InitializeAccountViewWithIssue(false, "");

            return View(accountView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOn(string email, string password, string returnUrl)
        {
            User user = await _authenticationService.Login(email, password);

            if (user.IsAuthenticated)
            {
                try
                {
                    SetCustomerIdentityTokenRequest setCustomerIdentityTokenRequest = new SetCustomerIdentityTokenRequest();
                    setCustomerIdentityTokenRequest.CustomerIdentityToken = user.AuthenticationToken;
                    setCustomerIdentityTokenRequest.Email = user.Email;

                    _customerService.SetCustomerIdentityToken(setCustomerIdentityTokenRequest);
                    await _cookieAuthentication.SetAuthenticationToken(user.AuthenticationToken);

                    return RedirectToAction("Index", "Home");
                }
                catch (EntityBaseIsInvalidException ex)
                {
                    AccountView accountView = InitializeAccountViewWithIssue(true, ex.Message);
                    ViewData["email"] = email;
                    ViewData["password"] = password;

                    return View(accountView);
                }
            }
            else
            {
                AccountView accountView = InitializeAccountViewWithIssue(true, "Sorry we could not log you in. Please try again.");
                accountView.CallBackSettings.ReturnUrl = GetReturnActionFrom(returnUrl).ToString();
                ViewData["email"] = email;
                ViewData["password"] = password;
                
                return View("LogOn", accountView);
            }
        }

        public IActionResult SignOut()
        {
            _cookieAuthentication.SignOut();
            
            return RedirectToAction("Index", "Home");
        }

        private AccountView InitializeAccountViewWithIssue(bool hasIssue, string message)
        {
            AccountView accountView = new AccountView();
            accountView.CallBackSettings.Action = "ReceiveTokenAndLogon";
            accountView.CallBackSettings.Controller = "AccountLogOn";
            accountView.HasIssue = hasIssue;
            accountView.Message = message;
            string returnUrl = _actionArguments.GetValueForArgument(ActionArgumentKey.ReturnUrl);
            accountView.CallBackSettings.ReturnUrl = GetReturnActionFrom(returnUrl).ToString();

            return accountView;
        }
    }
}