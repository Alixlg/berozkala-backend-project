using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace berozkala_backend.Migrations
{
    /// <inheritdoc />
    public partial class BigUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketProduct_Users_UserAccountId",
                table: "BasketProduct");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ImagesUrl",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AdminPermissions",
                table: "Admins");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "Users",
                newName: "AddressId");

            migrationBuilder.RenameColumn(
                name: "UserAccountId",
                table: "BasketProduct",
                newName: "SelectedGarrantyId");

            migrationBuilder.RenameColumn(
                name: "Count",
                table: "BasketProduct",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketProduct_UserAccountId",
                table: "BasketProduct",
                newName: "IX_BasketProduct_SelectedGarrantyId");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "Admins",
                newName: "AddressId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountPercent",
                table: "Products",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AddColumn<int>(
                name: "ImagesUrlsId",
                table: "Products",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductCount",
                table: "BasketProduct",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AddressBody = table.Column<string>(type: "TEXT", nullable: false),
                    PostalCode = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    AdminAccountId = table.Column<int>(type: "INTEGER", nullable: true),
                    UserAccountId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_Admins_AdminAccountId",
                        column: x => x.AdminAccountId,
                        principalTable: "Admins",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Address_Users_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DiscountCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    EndOfCredit = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MinProductPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    MaxUsageCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductGuid = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserAccountId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteProduct_Users_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PeymentMethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    MethodName = table.Column<string>(type: "TEXT", nullable: false),
                    MethodDescription = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeymentMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategorys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoryName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategorys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ImagePath = table.Column<string>(type: "TEXT", nullable: false),
                    ImageName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShippingMethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    MethodName = table.Column<string>(type: "TEXT", nullable: false),
                    MethodDescription = table.Column<string>(type: "TEXT", nullable: false),
                    ShipmentCost = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubmitedDiscountCode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    DiscountCodeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmitedDiscountCode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubmitedDiscountCode_DiscountCodes_DiscountCodeId",
                        column: x => x.DiscountCodeId,
                        principalTable: "DiscountCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubmitedDiscountCode_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentInformaation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PaymentMethodId = table.Column<int>(type: "INTEGER", nullable: false),
                    PeymentStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    PaymentTrackingCode = table.Column<string>(type: "TEXT", nullable: true),
                    PaymentTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentInformaation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentInformaation_PeymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PeymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductSubCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SubCategoryName = table.Column<string>(type: "TEXT", nullable: false),
                    ProductCategoryId = table.Column<int>(type: "INTEGER", nullable: true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSubCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSubCategory_ProductCategorys_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategorys",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductSubCategory_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GuId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DateToAdd = table.Column<DateTime>(type: "TEXT", nullable: false),
                    OrderStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderNumber = table.Column<long>(type: "INTEGER", nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    SenderAddress = table.Column<string>(type: "TEXT", nullable: false),
                    ReceiverAddressId = table.Column<int>(type: "INTEGER", nullable: false),
                    ReceiverFullName = table.Column<string>(type: "TEXT", nullable: false),
                    PaymentInformaationId = table.Column<int>(type: "INTEGER", nullable: false),
                    ShipmentMethodId = table.Column<int>(type: "INTEGER", nullable: false),
                    DiscountCodeId = table.Column<int>(type: "INTEGER", nullable: true),
                    BasketTotalPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Address_ReceiverAddressId",
                        column: x => x.ReceiverAddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_DiscountCodes_DiscountCodeId",
                        column: x => x.DiscountCodeId,
                        principalTable: "DiscountCodes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_PaymentInformaation_PaymentInformaationId",
                        column: x => x.PaymentInformaationId,
                        principalTable: "PaymentInformaation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_ShippingMethods_ShipmentMethodId",
                        column: x => x.ShipmentMethodId,
                        principalTable: "ShippingMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductBrand = table.Column<string>(type: "TEXT", nullable: false),
                    ProductTitle = table.Column<string>(type: "TEXT", nullable: false),
                    ProductGuid = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductGarrantyId = table.Column<int>(type: "INTEGER", nullable: true),
                    ProductCount = table.Column<int>(type: "INTEGER", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderItem_ProductGarranty_ProductGarrantyId",
                        column: x => x.ProductGarrantyId,
                        principalTable: "ProductGarranty",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ImagesUrlsId",
                table: "Products",
                column: "ImagesUrlsId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketProduct_UserId",
                table: "BasketProduct",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_AdminAccountId",
                table: "Address",
                column: "AdminAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_UserAccountId",
                table: "Address",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteProduct_UserAccountId",
                table: "FavoriteProduct",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductGarrantyId",
                table: "OrderItem",
                column: "ProductGarrantyId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DiscountCodeId",
                table: "Orders",
                column: "DiscountCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentInformaationId",
                table: "Orders",
                column: "PaymentInformaationId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ReceiverAddressId",
                table: "Orders",
                column: "ReceiverAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShipmentMethodId",
                table: "Orders",
                column: "ShipmentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentInformaation_PaymentMethodId",
                table: "PaymentInformaation",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubCategory_ProductCategoryId",
                table: "ProductSubCategory",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubCategory_ProductId",
                table: "ProductSubCategory",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmitedDiscountCode_DiscountCodeId",
                table: "SubmitedDiscountCode",
                column: "DiscountCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmitedDiscountCode_UserId",
                table: "SubmitedDiscountCode",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProduct_ProductGarranty_SelectedGarrantyId",
                table: "BasketProduct",
                column: "SelectedGarrantyId",
                principalTable: "ProductGarranty",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProduct_Users_UserId",
                table: "BasketProduct",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductImage_ImagesUrlsId",
                table: "Products",
                column: "ImagesUrlsId",
                principalTable: "ProductImage",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketProduct_ProductGarranty_SelectedGarrantyId",
                table: "BasketProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketProduct_Users_UserId",
                table: "BasketProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductImage_ImagesUrlsId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "FavoriteProduct");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "ProductImage");

            migrationBuilder.DropTable(
                name: "ProductSubCategory");

            migrationBuilder.DropTable(
                name: "SubmitedDiscountCode");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ProductCategorys");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "DiscountCodes");

            migrationBuilder.DropTable(
                name: "PaymentInformaation");

            migrationBuilder.DropTable(
                name: "ShippingMethods");

            migrationBuilder.DropTable(
                name: "PeymentMethods");

            migrationBuilder.DropIndex(
                name: "IX_Products_ImagesUrlsId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_BasketProduct_UserId",
                table: "BasketProduct");

            migrationBuilder.DropColumn(
                name: "ImagesUrlsId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductCount",
                table: "BasketProduct");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Users",
                newName: "Age");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "BasketProduct",
                newName: "Count");

            migrationBuilder.RenameColumn(
                name: "SelectedGarrantyId",
                table: "BasketProduct",
                newName: "UserAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketProduct_SelectedGarrantyId",
                table: "BasketProduct",
                newName: "IX_BasketProduct_UserAccountId");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Admins",
                newName: "Age");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Products",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<double>(
                name: "DiscountPercent",
                table: "Products",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Products",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagesUrl",
                table: "Products",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdminPermissions",
                table: "Admins",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProduct_Users_UserAccountId",
                table: "BasketProduct",
                column: "UserAccountId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
