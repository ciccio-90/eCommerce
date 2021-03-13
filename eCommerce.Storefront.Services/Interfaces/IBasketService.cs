using eCommerce.Storefront.Services.Messaging.ProductCatalogService;

namespace eCommerce.Storefront.Services.Interfaces
{
    public interface IBasketService
    {
        GetBasketResponse GetBasket(GetBasketRequest basketRequest);
        CreateBasketResponse CreateBasket(CreateBasketRequest basketRequest);
        ModifyBasketResponse ModifyBasket(ModifyBasketRequest request);
        GetAllDispatchOptionsResponse GetAllDispatchOptions();
    }
}