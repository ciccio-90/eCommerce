using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Payments;
using eCommerce.Storefront.Services.Interfaces;
using eCommerce.Storefront.Services.Messaging.OrderService;
using eCommerce.Storefront.Services.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Logging;
using Infrastructure.Authentication;

namespace eCommerce.Storefront.Controllers.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly ICookieAuthentication _cookieAuthentication;

        public PaymentController(IPaymentService paymentService,
                                 IOrderService orderService,
                                 IMapper mapper,
                                 ILogger logger,
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
                SetOrderPaymentRequest paymentRequest = new SetOrderPaymentRequest();
                paymentRequest.Amount = transactionResult.Amount;
                paymentRequest.PaymentToken = transactionResult.PaymentToken;
                paymentRequest.PaymentMerchant = transactionResult.PaymentMerchant;
                paymentRequest.OrderId = orderId;
                paymentRequest.CustomerEmail = _cookieAuthentication.GetAuthenticationToken();

                _orderService.SetOrderPayment(paymentRequest);
            }
            else
            {
                _logger.Log(string.Format("Payment not ok for order id {0}, payment token {1}", orderId, transactionResult.PaymentToken));
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