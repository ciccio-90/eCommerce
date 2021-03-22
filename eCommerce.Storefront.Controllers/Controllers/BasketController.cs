using System;
using eCommerce.Storefront.Controllers.DTOs;
using eCommerce.Storefront.Controllers.ViewModels;
using eCommerce.Storefront.Controllers.ViewModels.ProductCatalog;
using Infrastructure.CookieStorage;
using eCommerce.Storefront.Services.Implementations;
using eCommerce.Storefront.Services.Interfaces;
using eCommerce.Storefront.Services.Messaging.ProductCatalogService;
using Microsoft.AspNetCore.Mvc;
using eCommerce.Storefront.Services.Cache;

namespace eCommerce.Storefront.Controllers.Controllers
{
    public class BasketController : ProductCatalogBaseController
    {
        private readonly IBasketService _basketService;
        private readonly ICookieStorageService _cookieStorageService;

        public BasketController(ICachedProductCatalogService cachedProductCatalogService,
                                IBasketService basketService,
                                ICookieStorageService cookieStorageService) : base(cookieStorageService, cachedProductCatalogService)
        {
            _basketService = basketService;
            _cookieStorageService = cookieStorageService;
        }

        public IActionResult Detail()
        {
            BasketDetailView basketView = new BasketDetailView();
            Guid basketId = GetBasketId();
            GetBasketRequest basketRequest = new GetBasketRequest() { BasketId = basketId };
            GetBasketResponse basketResponse = _basketService.GetBasket(basketRequest);
            GetAllDispatchOptionsResponse dispatchOptionsResponse = _basketService.GetAllDispatchOptions();
            basketView.Basket = basketResponse.Basket;
            basketView.Categories = GetCategories();
            basketView.BasketSummary = GetBasketSummaryView();
            basketView.DeliveryOptions = dispatchOptionsResponse.DeliveryOptions;
            
            return View("View", basketView);
        }

        [HttpPost]
        public IActionResult RemoveItem(int productId)
        {
            ModifyBasketRequest request = new ModifyBasketRequest();

            request.ItemsToRemove.Add(productId);

            request.BasketId = GetBasketId();
            ModifyBasketResponse response = _basketService.ModifyBasket(request);
            
            SaveBasketSummaryToCookie(response.Basket.NumberOfItems, response.Basket.BasketTotal);
            
            BasketDetailView basketDetailView = new BasketDetailView();
            basketDetailView.BasketSummary = new BasketSummaryView
            {
                BasketTotal = response.Basket.BasketTotal,
                NumberOfItems = response.Basket.NumberOfItems
            };
            basketDetailView.Basket = response.Basket;
            basketDetailView.DeliveryOptions = _basketService.GetAllDispatchOptions().DeliveryOptions;
            
            return Ok(basketDetailView);
        }

        [HttpPost]
        public IActionResult UpdateShipping(int shippingServiceId)
        {
            ModifyBasketRequest request = new ModifyBasketRequest();
            request.SetShippingServiceIdTo = shippingServiceId;
            request.BasketId = GetBasketId();
            BasketDetailView basketDetailView = new BasketDetailView();
            ModifyBasketResponse response = _basketService.ModifyBasket(request);
            
            SaveBasketSummaryToCookie(response.Basket.NumberOfItems, response.Basket.BasketTotal);
            
            basketDetailView.BasketSummary = new BasketSummaryView
            {
                BasketTotal = response.Basket.BasketTotal,
                NumberOfItems = response.Basket.NumberOfItems
            };

            basketDetailView.Basket = response.Basket;
            basketDetailView.DeliveryOptions = _basketService.GetAllDispatchOptions().DeliveryOptions;
            
            return Ok(basketDetailView);
        }

        [HttpPost]
        public IActionResult UpdateItems([FromBody] BasketQtyUpdateRequest jsonBasketQtyUpdateRequest)
        {
            ModifyBasketRequest request = new ModifyBasketRequest();
            request.BasketId = GetBasketId();
            request.ItemsToUpdate = jsonBasketQtyUpdateRequest.ConvertToBasketItemUpdateRequests();
            BasketDetailView basketDetailView = new BasketDetailView();
            ModifyBasketResponse response = _basketService.ModifyBasket(request);
            
            SaveBasketSummaryToCookie(response.Basket.NumberOfItems, response.Basket.BasketTotal);

            basketDetailView.BasketSummary = new BasketSummaryView
            {
                BasketTotal = response.Basket.BasketTotal,
                NumberOfItems = response.Basket.NumberOfItems
            };

            basketDetailView.Basket = response.Basket;
            basketDetailView.DeliveryOptions = _basketService.GetAllDispatchOptions().DeliveryOptions;
            
            return Ok(basketDetailView);
        }

        [HttpPost]
        public IActionResult AddToBasket(int productId)
        {
            BasketSummaryView basketSummaryView = new BasketSummaryView();
            Guid basketId = GetBasketId();
            bool createNewBasket = basketId == Guid.Empty;

            if (!createNewBasket)
            {
                ModifyBasketRequest modifyBasketRequest = new ModifyBasketRequest();

                modifyBasketRequest.ProductsToAdd.Add(productId);

                modifyBasketRequest.BasketId = basketId;
                
                try
                {
                    ModifyBasketResponse response = _basketService.ModifyBasket(modifyBasketRequest);
                    basketSummaryView = response.Basket.ConvertToSummary();
                    
                    SaveBasketSummaryToCookie(basketSummaryView.NumberOfItems, basketSummaryView.BasketTotal);
                }
                catch (BasketDoesNotExistException)
                {
                    createNewBasket = true;
                }
            }

            if (createNewBasket)
            {
                CreateBasketRequest createBasketRequest = new CreateBasketRequest();

                createBasketRequest.ProductsToAdd.Add(productId);

                CreateBasketResponse response = _basketService.CreateBasket(createBasketRequest);

                SaveBasketIdToCookie(response.Basket.Id);

                basketSummaryView = response.Basket.ConvertToSummary();

                SaveBasketSummaryToCookie(basketSummaryView.NumberOfItems,basketSummaryView.BasketTotal);
            }

            return Ok(basketSummaryView);
        }

        private void SaveBasketIdToCookie(Guid basketId)
        {
            _cookieStorageService.Save(CookieDataKeys.BasketId.ToString(), basketId.ToString(), DateTime.Now.AddDays(1));
        }

        private void SaveBasketSummaryToCookie(int numberOfItems, string basketTotal)
        {
            _cookieStorageService.Save(CookieDataKeys.BasketItems.ToString(), numberOfItems.ToString(), DateTime.Now.AddDays(1));
            _cookieStorageService.Save(CookieDataKeys.BasketTotal.ToString(), basketTotal.ToString(), DateTime.Now.AddDays(1));
        }
    }
}