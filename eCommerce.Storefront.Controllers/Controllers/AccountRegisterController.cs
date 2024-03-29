using System;
using System.Threading.Tasks;
using eCommerce.Storefront.Controllers.ActionArguments;
using eCommerce.Storefront.Controllers.ViewModels.Account;
using eCommerce.Storefront.Services.Interfaces;
using eCommerce.Storefront.Services.Messaging.CustomerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Transactions;
using eCommerce.Storefront.Controllers.Services.Interfaces;
using eCommerce.Storefront.Model;
using eCommerce.Storefront.Controllers.Models;

namespace eCommerce.Storefront.Controllers.Controllers
{
    public class AccountRegisterController : BaseAccountController
    {
        public AccountRegisterController(ILocalAuthenticationService authenticationService,
                                         ICustomerService customerService,
                                         ICookieAuthentication cookieAuthentication,
                                         IActionArguments actionArguments) : base(authenticationService, 
                                                                                  customerService,
                                                                                  cookieAuthentication, 
                                                                                  actionArguments)
        {
        }

        public IActionResult Register()
        {
            AccountView accountView = InitializeAccountViewWithIssue(false, string.Empty);

            return View(accountView);
        }

        [HttpPost]
        public async Task<IActionResult> Register(IFormCollection collection)
        {
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
            {
                User user = null;
                string password = collection[FormDataKeys.Password.ToString()];
                string email = collection[FormDataKeys.Email.ToString()];
                string firstName = collection[FormDataKeys.FirstName.ToString()];
                string secondName = collection[FormDataKeys.SecondName.ToString()];

                try
                {
                    user = await _authenticationService.RegisterUser(email, password, true, new List<string> { "Customer" });
                }
                catch (InvalidOperationException ex)
                {
                    AccountView accountView = InitializeAccountViewWithIssue(true, ex.Message);
                    ViewData[FormDataKeys.Password.ToString()] = password;
                    ViewData[FormDataKeys.Email.ToString()] = email;
                    ViewData[FormDataKeys.FirstName.ToString()] = firstName;
                    ViewData[FormDataKeys.SecondName.ToString()] = secondName;

                    return View(accountView);
                }

                if (user.IsAuthenticated)
                {
                    try
                    {
                        CreateCustomerRequest createCustomerRequest = new CreateCustomerRequest
                        {
                            UserId = user.Id,
                            Email = email,
                            FirstName = firstName,
                            SecondName = secondName
                        };

                        _customerService.CreateCustomer(createCustomerRequest);
                        await _cookieAuthentication.SetAuthenticationToken(user.Email, new List<string> { "Customer" });
                        transactionScope.Complete();

                        return RedirectToAction("Detail", "Customer");
                    }
                    catch (EntityBaseIsInvalidException ex)
                    {
                        AccountView accountView = InitializeAccountViewWithIssue(true, ex.Message);
                        ViewData[FormDataKeys.Password.ToString()] = password;
                        ViewData[FormDataKeys.Email.ToString()] = email;
                        ViewData[FormDataKeys.FirstName.ToString()] = firstName;
                        ViewData[FormDataKeys.SecondName.ToString()] = secondName;

                        return View(accountView);
                    }
                }
                else
                {
                    AccountView accountView = InitializeAccountViewWithIssue(true, "Sorry we could not authenticate you. Please try again.");
                    ViewData[FormDataKeys.Password.ToString()] = password;
                    ViewData[FormDataKeys.Email.ToString()] = email;
                    ViewData[FormDataKeys.FirstName.ToString()] = firstName;
                    ViewData[FormDataKeys.SecondName.ToString()] = secondName;

                    return View(accountView);
                }
            }
        }

        private AccountView InitializeAccountViewWithIssue(bool hasIssue, string message)
        {
            AccountView accountView = new AccountView();
            accountView.CallBackSettings.Action = "ReceiveTokenAndRegister";
            accountView.CallBackSettings.Controller = "AccountRegister";
            accountView.HasIssue = hasIssue;
            accountView.Message = message;
            string returnUrl = _actionArguments.GetValueForArgument(ActionArgumentKey.ReturnUrl);
            accountView.CallBackSettings.ReturnUrl = GetReturnActionFrom(returnUrl).ToString();

            return accountView;
        }
    }
}