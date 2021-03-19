using System;
using System.Linq;
using eCommerce.Storefront.Controllers.ViewModels.Checkout;
using Infrastructure.Authentication;
using Infrastructure.CookieStorage;
using eCommerce.Storefront.Services.Interfaces;
using eCommerce.Storefront.Services.Messaging.CustomerService;
using eCommerce.Storefront.Services.Messaging.OrderService;
using eCommerce.Storefront.Services.Messaging.ProductCatalogService;
using eCommerce.Storefront.Services.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Storefront.Controllers.Controllers
{
    [Authorize]
    public class CheckoutController : BaseController
    {
        private readonly ICookieStorageService _cookieStorageService;
        private readonly IBasketService _basketService;
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;
        private readonly ICookieAuthentication _cookieAuthentication;

        public CheckoutController(ICookieStorageService cookieStorageService,
                                  IBasketService basketService,
                                  ICustomerService customerService,
                                  IOrderService orderService,
                                  ICookieAuthentication cookieAuthentication) : base(cookieStorageService)
        {
            _cookieStorageService = cookieStorageService;
            _basketService = basketService;
            _customerService = customerService;
            _orderService = orderService;
            _cookieAuthentication = cookieAuthentication;
        }

        public IActionResult Checkout()
        {
            GetCustomerRequest customerRequest = new GetCustomerRequest
            {
                CustomerIdentityToken = _cookieAuthentication.GetAuthenticationToken()
            };
            GetCustomerResponse customerResponse = _customerService.GetCustomer(customerRequest);

            if (customerResponse.CustomerFound)
            {
                CustomerView customerView = customerResponse.Customer;

                if (customerView.DeliveryAddressBook.Any())
                {
                    OrderConfirmationView orderConfirmationView = new OrderConfirmationView();
                    GetBasketRequest getBasketRequest = new GetBasketRequest
                    {
                        BasketId = base.GetBasketId()
                    };
                    GetBasketResponse basketResponse = _basketService.GetBasket(getBasketRequest);
                    orderConfirmationView.Basket = basketResponse.Basket;
                    orderConfirmationView.DeliveryAddresses = customerView.DeliveryAddressBook;

                    return View("ConfirmOrder", orderConfirmationView);
                }

                return AddDeliveryAddress();
            }
            else 
            {
                return RedirectToAction("LogOn", "AccountLogOn");
            }
        }

        public IActionResult AddDeliveryAddress()
        {
            DeliveryAddressView deliveryAddressView = new DeliveryAddressView();

            return View("AddDeliveryAddress", deliveryAddressView);
        }

        [HttpPost]
        public IActionResult AddDeliveryAddress(DeliveryAddressView deliveryAddressView)
        {
            DeliveryAddressAddRequest request = new DeliveryAddressAddRequest();
            request.Address = deliveryAddressView;
            request.CustomerIdentityToken = _cookieAuthentication.GetAuthenticationToken();

            _customerService.AddDeliveryAddress(request);

            return Checkout();
        }

        public IActionResult PlaceOrder(IFormCollection collection)
        {
            CreateOrderRequest request = new CreateOrderRequest();
            request.BasketId = base.GetBasketId();
            request.CustomerIdentityToken = _cookieAuthentication.GetAuthenticationToken();
            request.DeliveryId = int.Parse(collection[FormDataKeys.DeliveryAddress.ToString()]);
            CreateOrderResponse response = _orderService.CreateOrder(request);

            _cookieStorageService.Save(CookieDataKeys.BasketItems.ToString(), "0", DateTime.Now.AddDays(1));
            _cookieStorageService.Save(CookieDataKeys.BasketTotal.ToString(), "0", DateTime.Now.AddDays(1));

            return RedirectToAction("CreatePaymentFor", "Payment", new { orderId = response.Order.Id });
        }
    }
}