using System.Linq;
using eCommerce.Storefront.Controllers.ViewModels.CustomerAccount;
using eCommerce.Storefront.Services.Interfaces;
using eCommerce.Storefront.Services.Messaging.CustomerService;
using eCommerce.Storefront.Services.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using eCommerce.Storefront.Controllers.Services.Interfaces;
using eCommerce.Storefront.Model;

namespace eCommerce.Storefront.Controllers.Controllers
{
    [Authorize(Roles = "Customer")]    
    public class CustomerController : BaseController
    {
        public CustomerController(ICustomerService customerService,
                                  ICookieAuthentication cookieAuthentication) : base(cookieAuthentication,
                                                                                     customerService)
        {
        }

        public async Task<IActionResult> Detail()
        {
            GetCustomerRequest customerRequest = new GetCustomerRequest
            {
                CustomerEmail = _cookieAuthentication.GetAuthenticationToken()
            };
            GetCustomerResponse response = _customerService.GetCustomer(customerRequest);

            if (response.CustomerFound)
            {
                CustomerDetailView customerDetailView = new CustomerDetailView
                {
                    Customer = response.Customer,
                    BasketSummary = GetBasketSummaryView()
                };

                return View(customerDetailView);
            }
            else 
            {
                await _cookieAuthentication.SignOut();

                return RedirectToAction("Register", "AccountRegister");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Detail(CustomerView customerView)
        {
            ModifyCustomerRequest request = new ModifyCustomerRequest
            {
                NewEmail = customerView.Email,
                FirstName = customerView.FirstName,
                SecondName = customerView.SecondName,
                CurrentEmail = _cookieAuthentication.GetAuthenticationToken()
            };
            CustomerDetailView customerDetailView = new CustomerDetailView
            {
                BasketSummary = GetBasketSummaryView()
            };

            try
            {       
                ModifyCustomerResponse response = _customerService.ModifyCustomer(request);  
                customerDetailView.Customer = response.Customer;

                await _cookieAuthentication.SetAuthenticationToken(customerDetailView.Customer.Email, new List<string> { "Customer" });
            }
            catch (EntityBaseIsInvalidException ex)
            {
                ViewData["IssueMessage"] = ex.Message;
                customerDetailView.Customer = customerView;
            }
        
            return View(customerDetailView);
        }

        public async Task<IActionResult> DeliveryAddresses()
        {
            GetCustomerRequest customerRequest = new GetCustomerRequest
            {
                CustomerEmail = _cookieAuthentication.GetAuthenticationToken()
            };
            GetCustomerResponse response = _customerService.GetCustomer(customerRequest);

            if (response.CustomerFound)
            {
                CustomerDetailView customerDetailView = new CustomerDetailView
                {
                    Customer = response.Customer,
                    BasketSummary = GetBasketSummaryView()
                };

                return View("DeliveryAddresses", customerDetailView);
            }
            else 
            {
                await _cookieAuthentication.SignOut();

                return RedirectToAction("Register", "AccountRegister");
            }
        }

        public async Task<IActionResult> EditDeliveryAddress(int deliveryAddressId)
        {
            GetCustomerRequest customerRequest = new GetCustomerRequest
            {
                CustomerEmail = _cookieAuthentication.GetAuthenticationToken()
            };
            GetCustomerResponse response = _customerService.GetCustomer(customerRequest);

            if (response.CustomerFound)
            {
                CustomerDeliveryAddressView deliveryAddressView = new CustomerDeliveryAddressView
                {
                    CustomerView = response.Customer,
                    Address = response.Customer.DeliveryAddressBook.FirstOrDefault(d => d.Id == deliveryAddressId),
                    BasketSummary = GetBasketSummaryView()
                };

                return View(deliveryAddressView);
            }
            else 
            {
                await _cookieAuthentication.SignOut();
                
                return RedirectToAction("Register", "AccountRegister");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditDeliveryAddress(DeliveryAddressView deliveryAddressView)
        {
            DeliveryAddressModifyRequest request = new DeliveryAddressModifyRequest
            {
                Address = deliveryAddressView,
                CustomerEmail = _cookieAuthentication.GetAuthenticationToken()
            };

            _customerService.ModifyDeliveryAddress(request);

            return await DeliveryAddresses();
        }

        public IActionResult AddDeliveryAddress()
        {
            CustomerDeliveryAddressView customerDeliveryAddressView = new CustomerDeliveryAddressView
            {
                Address = new DeliveryAddressView(),
                BasketSummary = GetBasketSummaryView()
            };

            return View(customerDeliveryAddressView);
        }

        [HttpPost]
        public async Task<IActionResult> AddDeliveryAddress(DeliveryAddressView deliveryAddressView)
        {
            DeliveryAddressAddRequest request = new DeliveryAddressAddRequest
            {
                Address = deliveryAddressView,
                CustomerEmail = _cookieAuthentication.GetAuthenticationToken()
            };

            _customerService.AddDeliveryAddress(request);

            return await DeliveryAddresses();
        }
    }
}