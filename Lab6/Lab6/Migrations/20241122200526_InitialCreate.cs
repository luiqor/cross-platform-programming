using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab6.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Line1 = table.Column<string>(type: "TEXT", maxLength: 90, nullable: false),
                    Line2 = table.Column<string>(type: "TEXT", maxLength: 90, nullable: true),
                    Line3 = table.Column<string>(type: "TEXT", maxLength: 90, nullable: true),
                    City = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    ZipPostcode = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    StateProvinceCounty = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Country = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    OtherAddressDetails = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CustomerPhone = table.Column<string>(type: "TEXT", maxLength: 12, nullable: false),
                    CustomerEmail = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    OtherCustomerDetails = table.Column<string>(type: "TEXT", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierCode = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    SupplierName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    OtherSupplierDetails = table.Column<string>(type: "TEXT", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierCode);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAddresses",
                columns: table => new
                {
                    DateFrom = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    AddressId = table.Column<int>(type: "INTEGER", nullable: false),
                    AddressTypeCode = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    DateTo = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAddresses", x => new { x.CustomerId, x.AddressId, x.DateFrom });
                    table.ForeignKey(
                        name: "FK_CustomerAddresses_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerAddresses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerPaymentMethods",
                columns: table => new
                {
                    CustomerPaymentMethodId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: true),
                    PaymentMethodCode = table.Column<string>(type: "TEXT", maxLength: 5, nullable: false),
                    CardNumber = table.Column<string>(type: "TEXT", maxLength: 21, nullable: false),
                    DateFrom = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateTo = table.Column<DateTime>(type: "TEXT", nullable: true),
                    OtherDetails = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPaymentMethods", x => x.CustomerPaymentMethodId);
                    table.ForeignKey(
                        name: "FK_CustomerPaymentMethods_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ParentProductId = table.Column<int>(type: "INTEGER", nullable: true),
                    ProductTypeCode = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    SupplierCode = table.Column<string>(type: "TEXT", nullable: true),
                    ProductPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    BookIsbn = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    BookAuthor = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    BookPublicationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    BookTitle = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    FoodContainsYn = table.Column<bool>(type: "INTEGER", nullable: false),
                    FoodName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    FoodDescription = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    FoodFlavor = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    FoodIngredients = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    OtherProductDetails = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Products_ParentProductId",
                        column: x => x.ParentProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId");
                    table.ForeignKey(
                        name: "FK_Products_Suppliers_SupplierCode",
                        column: x => x.SupplierCode,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierCode");
                });

            migrationBuilder.CreateTable(
                name: "CustomerOrders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: true),
                    CustomerPaymentMethodId = table.Column<int>(type: "INTEGER", nullable: true),
                    OrderStatusCode = table.Column<string>(type: "TEXT", maxLength: 5, nullable: false),
                    DateOrderPlaced = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateOrderPaid = table.Column<DateTime>(type: "TEXT", nullable: false),
                    OrderTotalPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    OtherOrderDetails = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerOrders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_CustomerOrders_CustomerPaymentMethods_CustomerPaymentMethodId",
                        column: x => x.CustomerPaymentMethodId,
                        principalTable: "CustomerPaymentMethods",
                        principalColumn: "CustomerPaymentMethodId");
                    table.ForeignKey(
                        name: "FK_CustomerOrders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                });

            migrationBuilder.CreateTable(
                name: "ProductPrices",
                columns: table => new
                {
                    DateFrom = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPrices", x => new { x.ProductId, x.DateFrom });
                    table.ForeignKey(
                        name: "FK_ProductPrices_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerOrdersDeliveries",
                columns: table => new
                {
                    DateReported = table.Column<DateTime>(type: "TEXT", nullable: false),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    DeliveryStatusCode = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerOrdersDeliveries", x => x.DateReported);
                    table.ForeignKey(
                        name: "FK_CustomerOrdersDeliveries_CustomerOrders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "CustomerOrders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerOrdersProducts",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Comments = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerOrdersProducts", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_CustomerOrdersProducts_CustomerOrders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "CustomerOrders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerOrdersProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddresses_AddressId",
                table: "CustomerAddresses",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrders_CustomerId",
                table: "CustomerOrders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrders_CustomerPaymentMethodId",
                table: "CustomerOrders",
                column: "CustomerPaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrdersDeliveries_OrderId",
                table: "CustomerOrdersDeliveries",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrdersProducts_ProductId",
                table: "CustomerOrdersProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPaymentMethods_CustomerId",
                table: "CustomerPaymentMethods",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ParentProductId",
                table: "Products",
                column: "ParentProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplierCode",
                table: "Products",
                column: "SupplierCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerAddresses");

            migrationBuilder.DropTable(
                name: "CustomerOrdersDeliveries");

            migrationBuilder.DropTable(
                name: "CustomerOrdersProducts");

            migrationBuilder.DropTable(
                name: "ProductPrices");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "CustomerOrders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "CustomerPaymentMethods");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
