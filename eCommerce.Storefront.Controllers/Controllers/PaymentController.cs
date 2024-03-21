using System.Threading.Tasks;
using AutoMapper;
using eCommerce.Storefront.Services.Interfaces;
using eCommerce.Storefront.Services.Messaging.OrderService;
using eCommerce.Storefront.Services.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using eCommerce.Storefront.Controllers.Services.Interfaces;
using Microsoft.Extensions.Logging;
using eCommerce.Storefront.Model.Orders;
using eCommerce.Storefront.Controllers.Models;

namespace eCommerce.Storefront.Controllers.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly ILogger<PaymentController> _logger;
        private readonly ICookieAuthentication _cookieAuthentication;

        public PaymentController(IPaymentService paymentService,
                                 IOrderService orderService,
                                 IMapper mapper,
                                 ILogger<PaymentController> logger,
                                 ICookieAuthentication cookieAuthentication)
        {
            _paymentService = paymentService;
            _orderService = orderService;
            _mapper = mapper;
            _logger = logger;
            _cookieAuthentication = cookieAuthentication;
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task PaymentCallBack(IFormCollection collection)
        {
            int orderId = _paymentService.GetOrderIdFor(collection);
            GetOrderRequest request = new GetOrderRequest 
            { 
                OrderId = orderId 
            };
            GetOrderResponse response = _orderService.GetOrder(request);
            OrderPaymentRequest orderPaymentRequest = _mapper.Map<OrderView, OrderPaymentRequest>(response.Order);
            TransactionResult transactionResult = await _paymentService.HandleCallBack(orderPaymentRequest, collection);

            if (transactionResult.PaymentOk)
            {
                SetOrderPaymentRequest paymentRequest = new SetOrderPaymentRequest
                {
                    Amount = transactionResult.Amount,
                    PaymentToken = transactionResult.PaymentToken,
                    PaymentMerchant = transactionResult.PaymentMerchant,
                    OrderId = orderId,
                    CustomerEmail = _cookieAuthentication.GetAuthenticationToken()
                };

                _orderService.SetOrderPayment(paymentRequest);
            }
            else
            {
                _logger.LogWarning(string.Format("Payment not ok for order id {0}, payment token {1}", orderId, transactionResult.PaymentToken));
            }
        }

        public IActionResult CreatePaymentFor(int orderId)
        {
            GetOrderRequest request = new GetOrderRequest
            { 
                OrderId = orderId 
            };
            GetOrderResponse response = _orderService.GetOrder(request);
            OrderPaymentRequest orderPaymentRequest = _mapper.Map<OrderView, OrderPaymentRequest>(response.Order);
            PaymentPostData paymentPostData = _paymentService.GeneratePostDataFor(orderPaymentRequest);

            return View("PaymentPost", paymentPostData);
        }

        public IActionResult PaymentComplete()
        {
            return View();
        }

        public IActionResult PaymentCancel()
        {
            return View();
        }
    }
}