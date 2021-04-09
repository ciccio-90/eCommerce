using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Infrastructure.Domain;
using eCommerce.Storefront.Model.Customers;
using eCommerce.Storefront.Model.Orders;
using eCommerce.Storefront.Services.Interfaces;
using eCommerce.Storefront.Services.Messaging.CustomerService;
using eCommerce.Storefront.Services.ViewModels;
using eCommerce.Storefront.Model.Basket;

namespace eCommerce.Storefront.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository,
                               IUnitOfWork uow,
                               IMapper mapper)
        {
            _customerRepository = customerRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public CreateCustomerResponse CreateCustomer(CreateCustomerRequest request)
        {
            CreateCustomerResponse response = new CreateCustomerResponse();
            Customer customer = new Customer();
            customer.UserId = request.UserId;
            customer.Email = request.Email;
            customer.FirstName = request.FirstName;
            customer.SecondName = request.SecondName;

            customer.ThrowExceptionIfInvalid();
            _customerRepository.Add(customer);
            _uow.Commit();

            response.Customer = _mapper.Map<Customer, CustomerView>(customer);

            return response;
        }

        public GetCustomerResponse GetCustomer(GetCustomerRequest request)
        {
            GetCustomerResponse response = new GetCustomerResponse();
            Customer customer = _customerRepository.FindBy(request.CustomerEmail);

            if (customer != null)
            {
                response.CustomerFound = true;
                response.Customer = _mapper.Map<Customer, CustomerView>(customer);
                response.Customer.Email = request.CustomerEmail;
                
                if (request.LoadOrderSummary)
                {
                    response.Orders = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderSummaryView>>(customer.Orders.OrderByDescending(o => o.Created));
                }

                if (request.LoadBasketSummary)
                {
                    response.Basket = _mapper.Map<Basket, BasketView>(customer.Basket);
                }
            }
            else
            {
                response.CustomerFound = false;
            }

            return response;
        }

        public ModifyCustomerResponse ModifyCustomer(ModifyCustomerRequest request)
        {
            ModifyCustomerResponse response = new ModifyCustomerResponse();
            Customer customer = _customerRepository.FindBy(request.CurrentEmail);
            customer.FirstName = request.FirstName;
            customer.SecondName = request.SecondName;
            customer.Email = request.NewEmail;

            customer.ThrowExceptionIfInvalid();
            _customerRepository.Save(customer);
            _customerRepository.SaveEmail(customer.UserId, customer.Email);
            _uow.Commit();

            response.Customer = _mapper.Map<Customer, CustomerView>(customer);

            return response;
        }

        public DeliveryAddressModifyResponse ModifyDeliveryAddress(DeliveryAddressModifyRequest request)
        {
            DeliveryAddressModifyResponse response = new DeliveryAddressModifyResponse();
            Customer customer = _customerRepository.FindBy(request.CustomerEmail);
            DeliveryAddress deliveryAddress = customer.DeliveryAddressBook.FirstOrDefault(d => d.Id == request.Address.Id);

            if (deliveryAddress != null)
            {
                UpdateDeliveryAddressFrom(request.Address, deliveryAddress);
                _customerRepository.Save(customer);
                _uow.Commit();
            }

            response.DeliveryAddress = _mapper.Map<DeliveryAddress, DeliveryAddressView>(deliveryAddress);

            return response;
        }

        public DeliveryAddressAddResponse AddDeliveryAddress(DeliveryAddressAddRequest request)
        {
            DeliveryAddressAddResponse response = new DeliveryAddressAddResponse();
            Customer customer = _customerRepository.FindBy(request.CustomerEmail);
            DeliveryAddress deliveryAddress = new DeliveryAddress();
            deliveryAddress.Customer = customer;

            UpdateDeliveryAddressFrom(request.Address, deliveryAddress);
            customer.AddAddress(deliveryAddress);
            _customerRepository.Save(customer);
            _uow.Commit();

            response.DeliveryAddress = _mapper.Map<DeliveryAddress, DeliveryAddressView>(deliveryAddress);

            return response;
        }

        private void UpdateDeliveryAddressFrom(DeliveryAddressView deliveryAddressSource, DeliveryAddress deliveryAddressToUpdate)
        {
            deliveryAddressToUpdate.Name = deliveryAddressSource.Name;
            deliveryAddressToUpdate.AddressLine = deliveryAddressSource.AddressLine;
            deliveryAddressToUpdate.City = deliveryAddressSource.City;
            deliveryAddressToUpdate.State = deliveryAddressSource.State;
            deliveryAddressToUpdate.Country = deliveryAddressSource.Country;
            deliveryAddressToUpdate.ZipCode = deliveryAddressSource.ZipCode;

            deliveryAddressToUpdate.ThrowExceptionIfInvalid();
        }
    }
}