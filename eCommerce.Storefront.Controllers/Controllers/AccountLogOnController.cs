using System.Threading.Tasks;
using eCommerce.Storefront.Controllers.ActionArguments;
using eCommerce.Storefront.Controllers.ViewModels.Account;
using eCommerce.Storefront.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using eCommerce.Storefront.Controllers.Services.Interfaces;
using eCommerce.Storefront.Controllers.Models;

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
            AccountView accountView = InitializeAccountViewWithIssue(false, string.Empty);

            return View(accountView);
        }

        [HttpPost]
        public async Task<IActionResult> LogOn(string email, string password, string returnUrl)
        {
            try
            {
                User user = await _authenticationService.Login(email, password);

                if (user.IsAuthenticated && user.Roles.Any(r => r.Equals("Customer")))
                {
                    await _cookieAuthentication.SetAuthenticationToken(user.Email, new List<string> { "Customer" });

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AccountView accountView = InitializeAccountViewWithIssue(true, "Sorry we could not log you in. Please try again.");
                    accountView.CallBackSettings.ReturnUrl = GetReturnActionFrom(returnUrl).ToString();
                    ViewData["email"] = email;
                    ViewData["password"] = password;
                    
                    return View(accountView);
                }
            }
            catch (InvalidOperationException ex)
            {
                AccountView accountView = InitializeAccountViewWithIssue(true, ex.Message);
                accountView.CallBackSettings.ReturnUrl = GetReturnActionFrom(returnUrl).ToString();
                ViewData["email"] = email;
                ViewData["password"] = password;

                return View(accountView);
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