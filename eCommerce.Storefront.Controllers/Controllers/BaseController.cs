using System;
using eCommerce.Storefront.Controllers.ViewModels;
using eCommerce.Storefront.Services.Interfaces;
using eCommerce.Storefront.Services.Messaging.CustomerService;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Storefront.Controllers.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly ICookieAuthentication _cookieAuthentication;
        protected readonly ICustomerService _customerService;
        
        protected BaseController(ICookieAuthentication cookieAuthentication,
                                 ICustomerService customerService)
        {
            _cookieAuthentication = cookieAuthentication;
            _customerService = customerService;
        }
        
        protected BasketSummaryView GetBasketSummaryView()
        {
            string basketTotal = string.Empty;
            int numberOfItems = 0;
            var email = _cookieAuthentication.GetAuthenticationToken();

            if (!string.IsNullOrWhiteSpace(email))
            {
                var response = _customerService.GetCustomer(new GetCustomerRequest
                {
                    CustomerEmail = email,
                    LoadBasketSummary = true
                });

                if (response.CustomerFound && response.Basket != null)
                {
                    basketTotal = response.Basket.BasketTotal;
                    numberOfItems = response.Basket.NumberOfItems;
                }
            }

            return new BasketSummaryView
            {
                BasketTotal = basketTotal,
                NumberOfItems = numberOfItems
            };
        }
        
        protected Guid GetBasketId()
        {
            Guid basketId = Guid.Empty;            
            var email = _cookieAuthentication.GetAuthenticationToken();

            if (!string.IsNullOrWhiteSpace(email))
            {
                var response = _customerService.GetCustomer(new GetCustomerRequest
                {
                    CustomerEmail = email,
                    LoadBasketSummary = true
                });

                if (response.CustomerFound && response.Basket != null)
                {
                    basketId = response.Basket.Id;
                }
            }
            
            return basketId;
        }
    }
}
