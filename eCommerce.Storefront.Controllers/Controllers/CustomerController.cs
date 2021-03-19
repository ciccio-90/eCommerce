using System.Linq;
using eCommerce.Storefront.Controllers.ViewModels.CustomerAccount;
using Infrastructure.Authentication;
using Infrastructure.CookieStorage;
using eCommerce.Storefront.Services.Interfaces;
using eCommerce.Storefront.Services.Messaging.CustomerService;
using eCommerce.Storefront.Services.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Domain;

namespace eCommerce.Storefront.Controllers.Controllers
{
    [Authorize]    
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;
        private readonly ICookieAuthentication _cookieAuthentication;

        public CustomerController(ICookieStorageService cookieStorageService,
                                  ICustomerService customerService,
                                  ICookieAuthentication cookieAuthentication) : base(cookieStorageService)
        {
            _customerService = customerService;
            _cookieAuthentication = cookieAuthentication;
        }

        public IActionResult Detail()
        {
            GetCustomerRequest customerRequest = new GetCustomerRequest();
            customerRequest.CustomerIdentityToken = _cookieAuthentication.GetAuthenticationToken();
            GetCustomerResponse response = _customerService.GetCustomer(customerRequest);

            if (response.CustomerFound)
            {
                CustomerDetailView customerDetailView = new CustomerDetailView();
                customerDetailView.Customer = response.Customer;
                customerDetailView.BasketSummary = base.GetBasketSummaryView();

                return View(customerDetailView);
            }
            else 
            {
                return RedirectToAction("LogOn", "AccountLogOn");
            }
        }

        [HttpPost]
        public IActionResult Detail(CustomerView customerView)
        {          
            ModifyCustomerRequest request = new ModifyCustomerRequest();
            request.CustomerIdentityToken = _cookieAuthentication.GetAuthenticationToken();
            request.Email = customerView.Email;
            request.FirstName = customerView.FirstName;
            request.SecondName = customerView.SecondName;
            CustomerDetailView customerDetailView = new CustomerDetailView();
            customerDetailView.BasketSummary = base.GetBasketSummaryView();

            try
            {       
                ModifyCustomerResponse response = _customerService.ModifyCustomer(request);  
                customerDetailView.Customer = response.Customer;
            }
            catch (EntityBaseIsInvalidException ex)
            {
                ViewData["IssueMessage"] = ex.Message;
                customerDetailView.Customer = customerView;
            }
        
            return View(customerDetailView);
        }

        public IActionResult DeliveryAddresses()
        {
            GetCustomerRequest customerRequest = new GetCustomerRequest();
            customerRequest.CustomerIdentityToken = _cookieAuthentication.GetAuthenticationToken();
            GetCustomerResponse response = _customerService.GetCustomer(customerRequest);

            if (response.CustomerFound)
            {                
                CustomerDetailView customerDetailView = new CustomerDetailView();
                customerDetailView.Customer = response.Customer;
                customerDetailView.BasketSummary = base.GetBasketSummaryView();

                return View("DeliveryAddresses", customerDetailView);
            }
            else 
            {
                return RedirectToAction("LogOn", "AccountLogOn");
            }
        }

        public IActionResult EditDeliveryAddress(int deliveryAddressId)
        {
            GetCustomerRequest customerRequest = new GetCustomerRequest();
            customerRequest.CustomerIdentityToken = _cookieAuthentication.GetAuthenticationToken();
            GetCustomerResponse response = _customerService.GetCustomer(customerRequest);

            if (response.CustomerFound)
            {                
                CustomerDeliveryAddressView deliveryAddressView = new CustomerDeliveryAddressView();
                deliveryAddressView.CustomerView = response.Customer;
                deliveryAddressView.Address = response.Customer.DeliveryAddressBook.FirstOrDefault(d => d.Id == deliveryAddressId);
                deliveryAddressView.BasketSummary = base.GetBasketSummaryView();

                return View(deliveryAddressView);
            }
            else 
            {
                return RedirectToAction("LogOn", "AccountLogOn");
            }
        }

        [HttpPost]
        public IActionResult EditDeliveryAddress(DeliveryAddressView deliveryAddressView)
        {
            DeliveryAddressModifyRequest request = new DeliveryAddressModifyRequest();
            request.Address = deliveryAddressView;
            request.CustomerIdentityToken = _cookieAuthentication.GetAuthenticationToken();

            _customerService.ModifyDeliveryAddress(request);

            return DeliveryAddresses();
        }

        public IActionResult AddDeliveryAddress()
        {
            CustomerDeliveryAddressView customerDeliveryAddressView = new CustomerDeliveryAddressView();
            customerDeliveryAddressView.Address = new DeliveryAddressView();
            customerDeliveryAddressView.BasketSummary = base.GetBasketSummaryView();

            return View(customerDeliveryAddressView);
        }

        [HttpPost]
        public IActionResult AddDeliveryAddress(DeliveryAddressView deliveryAddressView)
        {
            DeliveryAddressAddRequest request = new DeliveryAddressAddRequest();
            request.Address = deliveryAddressView;
            request.CustomerIdentityToken = _cookieAuthentication.GetAuthenticationToken();
            
            _customerService.AddDeliveryAddress(request);

            return DeliveryAddresses();
        }
    }
}