using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Infrastructure.UnitOfWork;
using eCommerce.Storefront.Model.Basket;
using eCommerce.Storefront.Model.Products;
using eCommerce.Storefront.Model.Shipping;
using eCommerce.Storefront.Services.Interfaces;
using eCommerce.Storefront.Services.Messaging.ProductCatalogService;
using eCommerce.Storefront.Services.ViewModels;

namespace eCommerce.Storefront.Services.Implementations
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IProductRepository _productRepository;
        private readonly IDeliveryOptionRepository _deliveryOptionRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository,
                             IProductRepository productRepository,
                             IDeliveryOptionRepository deliveryOptionRepository,
                             IUnitOfWork uow,
                             IMapper mapper)
        {
            _basketRepository = basketRepository;
            _productRepository = productRepository;
            _deliveryOptionRepository = deliveryOptionRepository;
            _uow = uow;
            _mapper = mapper;
        }
        
        public GetBasketResponse GetBasket(GetBasketRequest basketRequest)
        {
            GetBasketResponse response = new GetBasketResponse();
            Basket basket = _basketRepository.FindBy(basketRequest.BasketId);
            BasketView basketView = null;
            
            if (basket != null)
            {
                basketView = _mapper.Map<Basket, BasketView>(basket);
            }
            else
            {
                basketView = new BasketView();
            }

            response.Basket = basketView;
            
            return response;
        }

        public CreateBasketResponse CreateBasket(CreateBasketRequest basketRequest)
        {
            CreateBasketResponse response = new CreateBasketResponse();
            Basket basket = new Basket();

            basket.SetDeliveryOption(GetCheapestDeliveryOption());
            AddProductsToBasket(basketRequest.ProductsToAdd, basket);
            basket.ThrowExceptionIfInvalid();
            _basketRepository.Save(basket);
            _uow.Commit();
            
            response.Basket = _mapper.Map<Basket, BasketView>(basket);
            
            return response;
        }
        
        private DeliveryOption GetCheapestDeliveryOption()
        {
            return _deliveryOptionRepository.FindAll().OrderBy(d => d.Cost).FirstOrDefault();
        }

        public ModifyBasketResponse ModifyBasket(ModifyBasketRequest request)
        {
            ModifyBasketResponse response = new ModifyBasketResponse();
            Basket basket = _basketRepository.FindBy(request.BasketId);

            if (basket == null)
            {
                throw new BasketDoesNotExistException();
            }
            
            AddProductsToBasket(request.ProductsToAdd, basket);
            UpdateLineQtys(request.ItemsToUpdate, basket);
            RemoveItemsFromBasket(request.ItemsToRemove, basket);
            
            if (request.SetShippingServiceIdTo != 0)
            {
                DeliveryOption deliveryOption =_deliveryOptionRepository.FindBy(request.SetShippingServiceIdTo);
                
                basket.SetDeliveryOption(deliveryOption);
            }

            basket.ThrowExceptionIfInvalid();
            _basketRepository.Save(basket);
            _uow.Commit();

            response.Basket = _mapper.Map<Basket, BasketView>(basket);
            
            return response;
        }
        
        private void RemoveItemsFromBasket(IList<int> productsToRemove, Basket basket)
        {
            foreach (int productId in productsToRemove)
            {
                Product product = _productRepository.FindBy(productId);

                if (product != null)
                {
                    _basketRepository.RemoveBasketItems(basket.Items.Where(i => i.Contains(product)));
                    basket.Remove(product);
                }
            }
        }

        private void UpdateLineQtys(IList<ProductQtyUpdateRequest> productQtyUpdateRequests, Basket basket)
        {
            foreach (ProductQtyUpdateRequest productQtyUpdateRequest in productQtyUpdateRequests)
            {
                Product product = _productRepository.FindBy(productQtyUpdateRequest.ProductId);
                
                if (product != null)
                {
                    basket.ChangeQtyOfProduct(productQtyUpdateRequest.NewQty, product);
                }
            }
        }

        private void AddProductsToBasket(IList<int> productsToAdd, Basket basket)
        {
            Product product = null;

            if (productsToAdd.Any())
            {
                foreach (int productId in productsToAdd)
                {
                    product = _productRepository.FindBy(productId);

                    basket.Add(product);
                }
            }
        }

        public GetAllDispatchOptionsResponse GetAllDispatchOptions()
        {
            GetAllDispatchOptionsResponse response = new GetAllDispatchOptionsResponse();
            response.DeliveryOptions = _deliveryOptionRepository.FindAll().OrderBy(d => d.Cost).Select(d => _mapper.Map<DeliveryOption, DeliveryOptionView>(d));
            
            return response;
        }
    }
}