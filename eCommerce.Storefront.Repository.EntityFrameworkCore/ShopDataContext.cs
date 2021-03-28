using System.Collections.Generic;
using eCommerce.Storefront.Model.Basket;
using eCommerce.Storefront.Model.Customers;
using eCommerce.Storefront.Model.Orders;
using eCommerce.Storefront.Model.Products;
using eCommerce.Storefront.Model.Shipping;
using eCommerce.Storefront.Repository.EntityFrameworkCore.Mapping;
using Infrastructure.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Storefront.Repository.EntityFrameworkCore
{
    public class ShopDataContext : DataContext
    {
        public ShopDataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductColor> Colors { get; set; }
        public DbSet<ProductSize> Sizes { get; set; }
        public DbSet<ProductTitle> ProductTitles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Courier> Couriers { get; set; }
        public DbSet<DeliveryOption> DeliveryOptions { get; set; }
        public DbSet<ShippingService> CourierServices { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<DeliveryAddress> CustomerDeliveryAddresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "f01eb6e7-a59f-4094-a38e-db1acb888a27",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "c25e7311-4d62-4cd1-a42a-43fadecdac0a"
                },
                new IdentityRole
                {
                    Id = "4c93af0c-9921-4abb-a5eb-26f8e01b2249",
                    Name = "Customer",
                    NormalizedName = "CUSTOMER",
                    ConcurrencyStamp = "b497576b-9358-46eb-ab44-4b8aa5cd50f6"
                }
            });
            builder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = "c8dd25ca-5d15-4a39-8023-7199a7f84286",
                UserName = "francescoguagnano@alice.it",
                NormalizedUserName = "FRANCESCOGUAGNANO@ALICE.IT",
                Email = "francescoguagnano@alice.it",
                NormalizedEmail = "FRANCESCOGUAGNANO@ALICE.IT",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEMlv8lrB6gzwN+Icpe4qmD0BQqw72MxJM9VwCUwzrdBcuBJtF8tiIGaim10UtNy51g==",
                SecurityStamp = "SMKW4WM6MF266XGGFASDTP7H4Y2TK3QO",
                ConcurrencyStamp = "b2205356-7b1b-4955-9142-d02c78c94651",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0
            });
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "f01eb6e7-a59f-4094-a38e-db1acb888a27",
                UserId = "c8dd25ca-5d15-4a39-8023-7199a7f84286"
            });
            builder.ApplyConfiguration(new BrandConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new ProductColorConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new ProductSizeConfiguration());
            builder.ApplyConfiguration(new ProductTitleConfiguration());
            builder.ApplyConfiguration(new BasketConfiguration());
            builder.ApplyConfiguration(new BasketItemConfiguration());
            builder.ApplyConfiguration(new CourierConfiguration());
            builder.ApplyConfiguration(new DeliveryOptionConfiguration());
            builder.ApplyConfiguration(new ShippingServiceConfiguration());
            builder.ApplyConfiguration(new CustomerConfiguration());
            builder.ApplyConfiguration(new DeliveryAddressConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new OrderItemConfiguration());
        }
    }
}