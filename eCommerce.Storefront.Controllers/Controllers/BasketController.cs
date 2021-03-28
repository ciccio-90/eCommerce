using System;
using eCommerce.Storefront.Controllers.DTOs;
using eCommerce.Storefront.Controllers.ViewModels;
using eCommerce.Storefront.Controllers.ViewModels.ProductCatalog;
using eCommerce.Storefront.Services.Implementations;
using eCommerce.Storefront.Services.Interfaces;
using eCommerce.Storefront.Services.Messaging.ProductCatalogService;
using Microsoft.AspNetCore.Mvc;
using eCommerce.Storefront.Services.Cache;
using Microsoft.AspNetCore.Authorization;
using Infrastructure.Authentication;

namespace eCommerce.Storefront.Controllers.Controllers
{
    [Authorize(Roles = "Customer")]
    public class BasketController : ProductCatalogBaseController
    {
        private readonly IBasketService _basketService;

        public BasketController(ICachedProductCatalogService cachedProductCatalogService,
                                IBasketService basketService,
                                ICookieAuthentication cookieAuthentication,
                                ICustomerService customerService) : base(cookieAuthentication, 
                                                                         customerService,
                                                                         cachedProductCatalogService)
        {
            _basketService = basketService;
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
                }
                catch (BasketDoesNotExistException)
                {
                    createNewBasket = true;
                }
            }

            if (createNewBasket)
            {
                CreateBasketRequest createBasketRequest = new CreateBasketRequest();
                createBasketRequest.CustomerEmail = _cookieAuthentication.GetAuthenticationToken();

                createBasketRequest.ProductsToAdd.Add(productId);

                CreateBasketResponse response = _basketService.CreateBasket(createBasketRequest);
                basketSummaryView = response.Basket.ConvertToSummary();
            }

            return Ok(basketSummaryView);
        }
    }
}