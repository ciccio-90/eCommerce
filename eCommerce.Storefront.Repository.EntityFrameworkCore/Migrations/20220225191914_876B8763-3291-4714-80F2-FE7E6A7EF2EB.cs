using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Storefront.Repository.EntityFrameworkCore.Migrations
{
    public partial class _876B87633291471480F2FE7E6A7EF2EB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    BrandId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.BrandId);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    ColorId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.ColorId);
                });

            migrationBuilder.CreateTable(
                name: "Couriers",
                columns: table => new
                {
                    CourierId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Couriers", x => x.CourierId);
                });

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    SizeId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.SizeId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTitles",
                columns: table => new
                {
                    ProductTitleId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BrandId = table.Column<long>(type: "INTEGER", nullable: true),
                    CategoryId = table.Column<long>(type: "INTEGER", nullable: true),
                    ColorId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTitles", x => x.ProductTitleId);
                    table.ForeignKey(
                        name: "FK_ProductTitles_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "BrandId");
                    table.ForeignKey(
                        name: "FK_ProductTitles_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId");
                    table.ForeignKey(
                        name: "FK_ProductTitles_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "ColorId");
                });

            migrationBuilder.CreateTable(
                name: "CourierServices",
                columns: table => new
                {
                    CourierServiceId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ServiceCode = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ServiceDescription = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CourierId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourierServices", x => x.CourierServiceId);
                    table.ForeignKey(
                        name: "FK_CourierServices_Couriers_CourierId",
                        column: x => x.CourierId,
                        principalTable: "Couriers",
                        principalColumn: "CourierId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SizeId = table.Column<long>(type: "INTEGER", nullable: true),
                    TitleId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_ProductTitles_TitleId",
                        column: x => x.TitleId,
                        principalTable: "ProductTitles",
                        principalColumn: "ProductTitleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "SizeId");
                });

            migrationBuilder.CreateTable(
                name: "DeliveryOptions",
                columns: table => new
                {
                    DeliveryOptionId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FreeDeliveryThreshold = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShippingServiceId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryOptions", x => x.DeliveryOptionId);
                    table.ForeignKey(
                        name: "FK_DeliveryOptions_CourierServices_ShippingServiceId",
                        column: x => x.ShippingServiceId,
                        principalTable: "CourierServices",
                        principalColumn: "CourierServiceId");
                });

            migrationBuilder.CreateTable(
                name: "BasketItems",
                columns: table => new
                {
                    BasketItemId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Qty = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<long>(type: "INTEGER", nullable: true),
                    BasketId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketItems", x => x.BasketItemId);
                    table.ForeignKey(
                        name: "FK_BasketItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId");
                });

            migrationBuilder.CreateTable(
                name: "Baskets",
                columns: table => new
                {
                    BasketId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DeliveryOptionId = table.Column<long>(type: "INTEGER", nullable: true),
                    CustomerId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baskets", x => x.BasketId);
                    table.ForeignKey(
                        name: "FK_Baskets_DeliveryOptions_DeliveryOptionId",
                        column: x => x.DeliveryOptionId,
                        principalTable: "DeliveryOptions",
                        principalColumn: "DeliveryOptionId");
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", maxLength: 450, nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    SecondName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    BasketId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Customers_Baskets_BasketId",
                        column: x => x.BasketId,
                        principalTable: "Baskets",
                        principalColumn: "BasketId");
                });

            migrationBuilder.CreateTable(
                name: "CustomerDeliveryAddresses",
                columns: table => new
                {
                    DeliveryId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AddressLine = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    State = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Country = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ZipCode = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CustomerId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerDeliveryAddresses", x => x.DeliveryId);
                    table.ForeignKey(
                        name: "FK_CustomerDeliveryAddresses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ShippingCharge = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShippingServiceId = table.Column<long>(type: "INTEGER", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    PaymentTransactionId = table.Column<string>(type: "TEXT", nullable: true),
                    PaymentMerchant = table.Column<string>(type: "TEXT", nullable: true),
                    PaymentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CustomerId = table.Column<long>(type: "INTEGER", nullable: false),
                    DeliveryAddressId = table.Column<long>(type: "INTEGER", nullable: true),
                    State = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_CourierServices_ShippingServiceId",
                        column: x => x.ShippingServiceId,
                        principalTable: "CourierServices",
                        principalColumn: "CourierServiceId");
                    table.ForeignKey(
                        name: "FK_Orders_CustomerDeliveryAddresses_DeliveryAddressId",
                        column: x => x.DeliveryAddressId,
                        principalTable: "CustomerDeliveryAddresses",
                        principalColumn: "DeliveryId");
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItemId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<long>(type: "INTEGER", nullable: true),
                    Qty = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4c93af0c-9921-4abb-a5eb-26f8e01b2249", "b497576b-9358-46eb-ab44-4b8aa5cd50f6", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f01eb6e7-a59f-4094-a38e-db1acb888a27", "c25e7311-4d62-4cd1-a42a-43fadecdac0a", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3d5bc7f9-e445-46fe-b284-894c3f0a1873", 0, "d092d71e-b8fe-44fb-85b6-06c27fab1fc1", "admin@ecommerce.com", true, true, null, "ADMIN@ECOMMERCE.COM", "ADMIN@ECOMMERCE.COM", "AQAAAAIAAYagAAAAEIV3gNJplD9fTYjDhH/lOnFcVVF16+jxwjD6hHx3OPFjOsbJOZsZE9smVDPFxDbZbw==", null, false, "4LMSVL7MBG3LGFPYFJ3QJTEMSPVVIWRD", false, "admin@ecommerce.com" });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 1L, "Levi" });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 2L, "Bench" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[] { 1L, "Trousers" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[] { 2L, "Shirts" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[] { 3L, "Socks" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[] { 4L, "Jackets" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[] { 5L, "T-Shirts" });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "ColorId", "Name" },
                values: new object[] { 1L, "Black" });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "ColorId", "Name" },
                values: new object[] { 2L, "Blue" });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "ColorId", "Name" },
                values: new object[] { 3L, "Red" });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "ColorId", "Name" },
                values: new object[] { 4L, "Green" });

            migrationBuilder.InsertData(
                table: "Couriers",
                columns: new[] { "CourierId", "Name" },
                values: new object[] { 1L, "UPS" });

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "SizeId", "Name" },
                values: new object[] { 1L, "L" });

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "SizeId", "Name" },
                values: new object[] { 2L, "XL" });

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "SizeId", "Name" },
                values: new object[] { 3L, "M" });

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "SizeId", "Name" },
                values: new object[] { 4L, "S" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "f01eb6e7-a59f-4094-a38e-db1acb888a27", "c8dd25ca-5d15-4a39-8023-7199a7f84286" });

            migrationBuilder.InsertData(
                table: "CourierServices",
                columns: new[] { "CourierServiceId", "ServiceCode", "CourierId", "ServiceDescription" },
                values: new object[] { 1L, "UPS-STD", 1L, "UPS Standard" });

            migrationBuilder.InsertData(
                table: "ProductTitles",
                columns: new[] { "ProductTitleId", "BrandId", "CategoryId", "ColorId", "ProductName", "Price" },
                values: new object[] { 1L, 1L, 1L, 2L, "506 Stretch Diamond", 27.99m });

            migrationBuilder.InsertData(
                table: "ProductTitles",
                columns: new[] { "ProductTitleId", "BrandId", "CategoryId", "ColorId", "ProductName", "Price" },
                values: new object[] { 2L, 1L, 1L, 1L, "506 Dark Stuff Straight", 25.99m });

            migrationBuilder.InsertData(
                table: "ProductTitles",
                columns: new[] { "ProductTitleId", "BrandId", "CategoryId", "ColorId", "ProductName", "Price" },
                values: new object[] { 3L, 1L, 1L, 2L, "512 Bootcut Jeans", 22.99m });

            migrationBuilder.InsertData(
                table: "ProductTitles",
                columns: new[] { "ProductTitleId", "BrandId", "CategoryId", "ColorId", "ProductName", "Price" },
                values: new object[] { 4L, 2L, 1L, 2L, "Lucian Straight Jeans", 22.99m });

            migrationBuilder.InsertData(
                table: "ProductTitles",
                columns: new[] { "ProductTitleId", "BrandId", "CategoryId", "ColorId", "ProductName", "Price" },
                values: new object[] { 5L, 2L, 1L, 3L, "Lucian Straight Jeans", 22.99m });

            migrationBuilder.InsertData(
                table: "ProductTitles",
                columns: new[] { "ProductTitleId", "BrandId", "CategoryId", "ColorId", "ProductName", "Price" },
                values: new object[] { 6L, 2L, 1L, 4L, "Lucian Straight Jeans", 22.99m });

            migrationBuilder.InsertData(
                table: "DeliveryOptions",
                columns: new[] { "DeliveryOptionId", "Cost", "FreeDeliveryThreshold", "ShippingServiceId" },
                values: new object[] { 1L, 3.99m, 29.99m, 1L });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "SizeId", "TitleId" },
                values: new object[] { 1L, 1L, 1L });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "SizeId", "TitleId" },
                values: new object[] { 2L, 2L, 1L });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "SizeId", "TitleId" },
                values: new object[] { 3L, 3L, 1L });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "SizeId", "TitleId" },
                values: new object[] { 4L, 4L, 1L });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "SizeId", "TitleId" },
                values: new object[] { 5L, 1L, 2L });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "SizeId", "TitleId" },
                values: new object[] { 6L, 2L, 2L });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "SizeId", "TitleId" },
                values: new object[] { 7L, 3L, 2L });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "SizeId", "TitleId" },
                values: new object[] { 8L, 4L, 2L });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "SizeId", "TitleId" },
                values: new object[] { 9L, 1L, 3L });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "SizeId", "TitleId" },
                values: new object[] { 10L, 2L, 3L });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "SizeId", "TitleId" },
                values: new object[] { 11L, 3L, 3L });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "SizeId", "TitleId" },
                values: new object[] { 12L, 4L, 3L });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "SizeId", "TitleId" },
                values: new object[] { 13L, 1L, 4L });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "SizeId", "TitleId" },
                values: new object[] { 14L, 2L, 4L });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "SizeId", "TitleId" },
                values: new object[] { 15L, 3L, 4L });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "SizeId", "TitleId" },
                values: new object[] { 16L, 4L, 4L });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "SizeId", "TitleId" },
                values: new object[] { 17L, 1L, 5L });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "SizeId", "TitleId" },
                values: new object[] { 18L, 2L, 5L });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "SizeId", "TitleId" },
                values: new object[] { 19L, 3L, 5L });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "SizeId", "TitleId" },
                values: new object[] { 20L, 4L, 5L });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "SizeId", "TitleId" },
                values: new object[] { 21L, 1L, 6L });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "SizeId", "TitleId" },
                values: new object[] { 22L, 2L, 6L });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "SizeId", "TitleId" },
                values: new object[] { 23L, 3L, 6L });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "SizeId", "TitleId" },
                values: new object[] { 24L, 4L, 6L });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_BasketId",
                table: "BasketItems",
                column: "BasketId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_ProductId",
                table: "BasketItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_CustomerId",
                table: "Baskets",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_DeliveryOptionId",
                table: "Baskets",
                column: "DeliveryOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_Name",
                table: "Brands",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Colors_Name",
                table: "Colors",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Couriers_Name",
                table: "Couriers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourierServices_CourierId",
                table: "CourierServices",
                column: "CourierId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDeliveryAddresses_CustomerId",
                table: "CustomerDeliveryAddresses",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_BasketId",
                table: "Customers",
                column: "BasketId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryOptions_ShippingServiceId",
                table: "DeliveryOptions",
                column: "ShippingServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryAddressId",
                table: "Orders",
                column: "DeliveryAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShippingServiceId",
                table: "Orders",
                column: "ShippingServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SizeId",
                table: "Products",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_TitleId",
                table: "Products",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTitles_BrandId",
                table: "ProductTitles",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTitles_CategoryId",
                table: "ProductTitles",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTitles_ColorId",
                table: "ProductTitles",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Sizes_Name",
                table: "Sizes",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Baskets_BasketId",
                table: "BasketItems",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "BasketId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Customers_CustomerId",
                table: "Baskets",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Baskets_BasketId",
                table: "Customers");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BasketItems");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "CustomerDeliveryAddresses");

            migrationBuilder.DropTable(
                name: "ProductTitles");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "Baskets");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "DeliveryOptions");

            migrationBuilder.DropTable(
                name: "CourierServices");

            migrationBuilder.DropTable(
                name: "Couriers");
        }
    }
}
