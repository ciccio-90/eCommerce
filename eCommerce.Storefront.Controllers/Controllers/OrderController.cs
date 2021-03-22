using eCommerce.Storefront.Controllers.ViewModels.CustomerAccount;
using Infrastructure.Authentication;
using Infrastructure.CookieStorage;
using eCommerce.Storefront.Services.Interfaces;
using eCommerce.Storefront.Services.Messaging.CustomerService;
using eCommerce.Storefront.Services.Messaging.OrderService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Storefront.Controllers.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;
        private readonly ICookieAuthentication _cookieAuthentication;

        public OrderController(ICustomerService customerService,
                               IOrderService orderService,
                               ICookieAuthentication cookieAuthentication,
                               ICookieStorageService cookieStorageService) : base(cookieStorageService)
        {
            _customerService = customerService;
            _orderService = orderService;
            _cookieAuthentication = cookieAuthentication;
        }

        public IActionResult List()
        {
            GetCustomerRequest request = new GetCustomerRequest
            {
                CustomerIdentityToken = _cookieAuthentication.GetAuthenticationToken(),
                LoadOrderSummary = true
            };
            GetCustomerResponse response = _customerService.GetCustomer(request);

            if (response.CustomerFound)
            {    
                CustomersOrderSummaryView customersOrderSummaryView = new CustomersOrderSummaryView();
                customersOrderSummaryView.Orders = response.Orders;
                customersOrderSummaryView.BasketSummary = GetBasketSummaryView();

                return View(customersOrderSummaryView);
            }
            else 
            {
                return RedirectToAction("LogOn", "AccountLogOn");
            }
        }
        
        public IActionResult Detail(int orderId)
        {
            GetOrderRequest request = new GetOrderRequest() { OrderId = orderId };
            GetOrderResponse response = _orderService.GetOrder(request);
            CustomerOrderView orderView = new CustomerOrderView();
            orderView.BasketSummary = GetBasketSummaryView();
            orderView.Order = response.Order;

            return View(orderView);
        }
    }
}