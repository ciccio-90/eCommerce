using eCommerce.Storefront.Controllers.ActionArguments;
using Infrastructure.Authentication;
using Infrastructure.CookieStorage;
using Infrastructure.EntityFrameworkCore;
using Infrastructure.Payments;
using eCommerce.Storefront.Model.Basket;
using eCommerce.Storefront.Model.Customers;
using eCommerce.Storefront.Model.Orders;
using eCommerce.Storefront.Model.Products;
using eCommerce.Storefront.Repository.EntityFrameworkCore.Repositories;
using eCommerce.Storefront.Services.Implementations;
using eCommerce.Storefront.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.CacheStorage;
using eCommerce.Storefront.Services.Cache;
using Infrastructure.Domain;
using Infrastructure.Logging;
using Infrastructure.Email;
using eCommerce.Storefront.Model.Shipping;

namespace eCommerce.Storefront.UI.Web.MVC
{
    public static class BootStrapper
    {
        public static IServiceCollection ConfigureDependencies(this IServiceCollection serviceCollection)
        {
            // Repositories
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped(typeof(IReadOnlyRepository<,>), typeof(Repository<,>));
            serviceCollection.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            serviceCollection.AddScoped<IBasketRepository, BasketRepository>();
            serviceCollection.AddScoped<IProductTitleRepository, ProductTitleRepository>();
            serviceCollection.AddScoped<IProductRepository, ProductRepository>();
            serviceCollection.AddScoped<ICustomerRepository, CustomerRepository>();
            serviceCollection.AddScoped<IOrderRepository, OrderRepository>();
            serviceCollection.AddScoped<IDeliveryOptionRepository, DeliveryOptionRepository>();
            // Product Catalogue
            serviceCollection.AddScoped(typeof(IEntityService<,>), typeof(EntityService<,>));
            serviceCollection.AddScoped<IProductCatalogService, ProductCatalogService>();
            serviceCollection.AddScoped<IBasketService, BasketService>();
            serviceCollection.AddScoped<ICookieStorageService, CookieStorageService>();
            serviceCollection.AddScoped<ICustomerService, CustomerService>();
            // Order Service
            serviceCollection.AddScoped<IOrderService, OrderService>();
            // Authentication
            serviceCollection.AddScoped<ICookieAuthentication, AspNetCoreCookieAuthentication>();
            serviceCollection.AddScoped<ILocalAuthenticationService, AspNetCoreIdentityAuthentication>();
            // Controller Helpers
            serviceCollection.AddScoped<IActionArguments, HttpRequestActionArguments>();
            // Payment
            serviceCollection.AddScoped<IPaymentService, PayPalPaymentService>();
            // Caching Strategies
            serviceCollection.AddScoped<ICacheStorage, MemoryCacheAdapter>();
            serviceCollection.AddScoped<ICachedProductCatalogService, CachedProductCatalogService>();
            // Logging
            serviceCollection.AddScoped<ILogger, Log4NetAdapter>();
            // Email
            serviceCollection.AddScoped<IEmailService, SmtpService>();

            return serviceCollection;
        }
    }
}