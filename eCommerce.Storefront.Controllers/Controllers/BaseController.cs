using System;
using eCommerce.Storefront.Controllers.ViewModels;
using Infrastructure.CookieStorage;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Storefront.Controllers.Controllers
{
    public class BaseController : Controller
    {
        private readonly ICookieStorageService _cookieStorageService;
        
        public BaseController(ICookieStorageService cookieStorageService)
        {
            _cookieStorageService = cookieStorageService;
        }
        
        public BasketSummaryView GetBasketSummaryView()
        {
            string basketTotal = string.Empty;
            int numberOfItems = 0;

            if (!string.IsNullOrEmpty(_cookieStorageService.Retrieve(CookieDataKeys.BasketTotal.ToString())))
            {
                basketTotal = _cookieStorageService.Retrieve(CookieDataKeys.BasketTotal.ToString());
            }

            if (!string.IsNullOrEmpty(_cookieStorageService.Retrieve(CookieDataKeys.BasketItems.ToString())))
            {
                numberOfItems = int.Parse(_cookieStorageService.Retrieve(CookieDataKeys.BasketItems.ToString()));
            }

            return new BasketSummaryView
            {
                BasketTotal = basketTotal,
                NumberOfItems = numberOfItems
            };
        }
        
        public Guid GetBasketId()
        {
            string sBasketId = _cookieStorageService.Retrieve(CookieDataKeys.BasketId.ToString());
            Guid basketId = Guid.Empty;
            
            if (!string.IsNullOrEmpty(sBasketId))
            {
                basketId = new Guid(sBasketId);
            }
            
            return basketId;
        }
    }
}