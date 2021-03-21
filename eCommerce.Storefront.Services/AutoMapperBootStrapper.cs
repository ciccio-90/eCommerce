using AutoMapper;
using Infrastructure.Helpers;
using Infrastructure.Payments;
using eCommerce.Storefront.Model.Basket;
using eCommerce.Storefront.Model.Customers;
using eCommerce.Storefront.Model.Orders;
using eCommerce.Storefront.Model.Products;
using eCommerce.Storefront.Model.Shipping;
using eCommerce.Storefront.Services.ViewModels;

namespace eCommerce.Storefront.Services
{
    public class AutoMapperBootStrapper : Profile
    {
        public AutoMapperBootStrapper()
        {            
            // Product Title and Product
            CreateMap<ProductTitle, ProductSummaryView>().ForMember(p => p.Price, 
                                                                    p => p.MapFrom(p => p.Price.FormatMoney()));
            CreateMap<ProductTitle, ProductView>().ForMember(p => p.Price, 
                                                             p => p.MapFrom(p => p.Price.FormatMoney()));
            CreateMap<Product, ProductSummaryView>().ForMember(p => p.Price, 
                                                               p => p.MapFrom(p => p.Price.FormatMoney()));
            CreateMap<Product, ProductSizeOption>();
            // Category
            CreateMap<Category, CategoryView>();
            // IProductAttribute
            CreateMap<IProductAttribute, Refinement>();
            // Basket
            CreateMap<DeliveryOption, DeliveryOptionView>();
            CreateMap<BasketItem, BasketItemView>().ForMember(b => b.ProductPrice, 
                                                              b => b.MapFrom(b => b.Product.Price.FormatMoney()))
                                                   .ForMember(b => b.LineTotal, 
                                                              b => b.MapFrom(b => b.LineTotal().FormatMoney()));
            CreateMap<Basket, BasketView>().ForMember(b => b.BasketTotal, 
                                                      b => b.MapFrom(b => b.BasketTotal.FormatMoney()))
                                           .ForMember(b => b.ItemsTotal, 
                                                      b => b.MapFrom(b => b.ItemsTotal.FormatMoney()))
                                           .ForMember(b => b.DeliveryCost, 
                                                      b => b.MapFrom(b => b.DeliveryCost().FormatMoney()))
                                           .ForMember(b => b.ShippingServiceDescription, 
                                                      b => b.MapFrom(b => b.DeliveryOption.ShippingService.Description));
            // Customer
            CreateMap<Customer, CustomerView>();
            CreateMap<DeliveryAddress, DeliveryAddressView>();
            // Orders
            CreateMap<Order, OrderView>().ForMember(o => o.ShippingCharge, 
                                                    o => o.MapFrom(o => o.ShippingCharge.FormatMoney()))
                                         .ForMember(o => o.Total, 
                                                    o => o.MapFrom(o => o.Total().FormatMoney()));
            CreateMap<OrderItem, OrderItemView>().ForMember(o => o.Price, 
                                                            o => o.MapFrom(o => o.Price.FormatMoney()));
            CreateMap<DeliveryAddress, DeliveryAddressView>();
            CreateMap<Order, OrderSummaryView>().ForMember(o => o.IsSubmitted,
                                                           o => o.MapFrom(o => o.Status == OrderStatus.Submitted));
            CreateMap<OrderView, OrderPaymentRequest>().ForMember(o => o.Total,
                                                                  o => o.MapFrom(o => decimal.Parse(o.Total.Substring(1, o.Total.Length -1))))
                                                       .ForMember(o => o.ShippingCharge,
                                                                  o => o.MapFrom(o => decimal.Parse(o.ShippingCharge.Substring(1, o.ShippingCharge.Length - 1))));
            CreateMap<OrderItemView, OrderItemPaymentRequest>().ForMember(o => o.Price, 
                                                                          o => o.MapFrom(o => decimal.Parse(o.Price.Substring(1, o.Price.Length - 1))));
            CreateMap<DeliveryAddress, DeliveryAddress>();
        }
    }
}
