using System.Linq;
using AutoMapper;
using Infrastructure.UnitOfWork;
using eCommerce.Storefront.Model.Basket;
using eCommerce.Storefront.Model.Customers;
using eCommerce.Storefront.Model.Orders;
using eCommerce.Storefront.Services.Interfaces;
using eCommerce.Storefront.Services.Messaging.OrderService;
using eCommerce.Storefront.Services.ViewModels;
using Infrastructure.Logging;
using Infrastructure.Configuration;
using Infrastructure.Email;
using System;

namespace eCommerce.Storefront.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IApplicationSettings _applicationSettings;
        private readonly IEmailService _emailService;

        public OrderService(IOrderRepository orderRepository,
                            IBasketRepository basketRepository,
                            ICustomerRepository customerRepository,
                            IUnitOfWork uow,
                            IMapper mapper,
                            ILogger logger,
                            IApplicationSettings applicationSettings,
                            IEmailService emailService)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
            _basketRepository = basketRepository;
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
            _applicationSettings = applicationSettings;
            _emailService = emailService;
        }

        public CreateOrderResponse CreateOrder(CreateOrderRequest request)
        {
            CreateOrderResponse response = new CreateOrderResponse();
            Customer customer = _customerRepository.FindBy(request.CustomerIdentityToken);
            Basket basket = _basketRepository.FindBy(request.BasketId);
            DeliveryAddress deliveryAddress = customer.DeliveryAddressBook.FirstOrDefault(d => d.Id == request.DeliveryId);
            Order order = ConvertToOrder(basket);
            order.Customer = customer;
            order.DeliveryAddress = deliveryAddress;

            _orderRepository.Save(order);
            _basketRepository.Remove(basket);
            _uow.Commit();

            response.Order = _mapper.Map<Order, OrderView>(order);

            return response;
        }

        public SetOrderPaymentResponse SetOrderPayment(SetOrderPaymentRequest paymentRequest)
        {
            SetOrderPaymentResponse paymentResponse = new SetOrderPaymentResponse();
            Order order = _orderRepository.FindBy(paymentRequest.OrderId);

            try
            {
                order.SetPayment(new Payment(DateTime.Now, paymentRequest.PaymentToken, paymentRequest.PaymentMerchant, paymentRequest.Amount));
                _orderRepository.Save(order);
                _uow.Commit();
            }
            catch (OrderAlreadyPaidForException ex)
            {
                // Refund the payment using the payment service.
                _logger.Log(ex.Message);
            }
            catch (PaymentAmountDoesNotEqualOrderTotalException ex)
            {
                // Refund the payment using the payment service.
                _logger.Log(ex.Message);
            }

            paymentResponse.Order = _mapper.Map<Order, OrderView>(order);

            return paymentResponse;
        }

        public GetOrderResponse GetOrder(GetOrderRequest request)
        {
            GetOrderResponse response = new GetOrderResponse();
            Order order = _orderRepository.FindBy(request.OrderId);
            response.Order = _mapper.Map<Order, OrderView>(order);

            return response;
        }

        private Order ConvertToOrder(Basket basket)
        {
            Order order = new Order(_applicationSettings, _emailService);
            order.ShippingCharge = basket.DeliveryCost();
            order.ShippingService = basket.DeliveryOption.ShippingService;

            foreach (BasketItem item in basket.Items)
            {
                order.AddItem(item.Product, item.Qty);
            }

            return order;
        }
    }
}