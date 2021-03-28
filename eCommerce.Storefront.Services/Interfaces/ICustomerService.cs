using eCommerce.Storefront.Services.Messaging.CustomerService;

namespace eCommerce.Storefront.Services.Interfaces
{
    public interface ICustomerService
    {
        CreateCustomerResponse CreateCustomer(CreateCustomerRequest request);
        GetCustomerResponse GetCustomer(GetCustomerRequest request);
        ModifyCustomerResponse ModifyCustomer(ModifyCustomerRequest request);
        DeliveryAddressModifyResponse ModifyDeliveryAddress(DeliveryAddressModifyRequest request);
        DeliveryAddressAddResponse AddDeliveryAddress(DeliveryAddressAddRequest request);
    }
}