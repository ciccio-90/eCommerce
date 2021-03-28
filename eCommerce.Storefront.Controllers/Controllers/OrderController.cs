using eCommerce.Storefront.Controllers.ViewModels.CustomerAccount;
using Infrastructure.Authentication;
using eCommerce.Storefront.Services.Interfaces;
using eCommerce.Storefront.Services.Messaging.CustomerService;
using eCommerce.Storefront.Services.Messaging.OrderService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eCommerce.Storefront.Controllers.Controllers
{
    [Authorize(Roles = "Customer")]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(ICustomerService customerService,
                               IOrderService orderService,
                               ICookieAuthentication cookieAuthentication) : base(cookieAuthentication,
                                                                                  customerService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> List()
        {
            GetCustomerRequest request = new GetCustomerRequest
            {
                CustomerEmail = _cookieAuthentication.GetAuthenticationToken(),
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
                await _cookieAuthentication.SignOut();
                
                return RedirectToAction("Register", "AccountRegister");
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