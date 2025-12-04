using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace berozkala_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderCreateApi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_ProductGarrantys_ProductGarrantyId",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DiscountCodes_DiscountCodeId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PaymentInformaation_PaymentInformaationId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ShippingMethods_ShipmentMethodId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentInformaation_PeymentMethods_PaymentMethodId",
                table: "PaymentInformaation");

            migrationBuilder.DropTable(
                name: "PeymentMethods");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ShipmentMethodId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentInformaation",
                table: "PaymentInformaation");

            migrationBuilder.DropColumn(
                name: "ProductGuid",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "OrderItem");

            migrationBuilder.RenameTable(
                name: "PaymentInformaation",
                newName: "PaymentInformaations");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "OrderItem",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "PeymentStatus",
                table: "PaymentInformaations",
                newName: "PaymentStatus");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentInformaation_PaymentMethodId",
                table: "PaymentInformaations",
                newName: "IX_PaymentInformaations_PaymentMethodId");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ShippingMethods",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "OrderNumber",
                table: "Orders",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "DiscountCodeId",
                table: "Orders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountAmount",
                table: "Orders",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ShippingMethodId",
                table: "Orders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ProductGarrantyId",
                table: "OrderItem",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "OrderItem",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentInformaations",
                table: "PaymentInformaations",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MethodName = table.Column<string>(type: "TEXT", nullable: false),
                    MethodDescription = table.Column<string>(type: "TEXT", nullable: false),
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShippingMethodId",
                table: "Orders",
                column: "ShippingMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductId",
                table: "OrderItem",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_ProductGarrantys_ProductGarrantyId",
                table: "OrderItem",
                column: "ProductGarrantyId",
                principalTable: "ProductGarrantys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Products_ProductId",
                table: "OrderItem",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DiscountCodes_DiscountCodeId",
                table: "Orders",
                column: "DiscountCodeId",
                principalTable: "DiscountCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PaymentInformaations_PaymentInformaationId",
                table: "Orders",
                column: "PaymentInformaationId",
                principalTable: "PaymentInformaations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ShippingMethods_ShippingMethodId",
                table: "Orders",
                column: "ShippingMethodId",
                principalTable: "ShippingMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentInformaations_PaymentMethods_PaymentMethodId",
                table: "PaymentInformaations",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_ProductGarrantys_ProductGarrantyId",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Products_ProductId",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DiscountCodes_DiscountCodeId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PaymentInformaations_PaymentInformaationId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ShippingMethods_ShippingMethodId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentInformaations_PaymentMethods_PaymentMethodId",
                table: "PaymentInformaations");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ShippingMethodId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_ProductId",
                table: "OrderItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentInformaations",
                table: "PaymentInformaations");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ShippingMethods");

            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingMethodId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "OrderItem");

            migrationBuilder.RenameTable(
                name: "PaymentInformaations",
                newName: "PaymentInformaation");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "OrderItem",
                newName: "UnitPrice");

            migrationBuilder.RenameColumn(
                name: "PaymentStatus",
                table: "PaymentInformaation",
                newName: "PeymentStatus");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentInformaations_PaymentMethodId",
                table: "PaymentInformaation",
                newName: "IX_PaymentInformaation_PaymentMethodId");

            migrationBuilder.AlterColumn<long>(
                name: "OrderNumber",
                table: "Orders",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "DiscountCodeId",
                table: "Orders",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "ProductGarrantyId",
                table: "OrderItem",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductGuid",
                table: "OrderItem",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "OrderItem",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentInformaation",
                table: "PaymentInformaation",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PeymentMethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    MethodDescription = table.Column<string>(type: "TEXT", nullable: false),
                    MethodName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeymentMethods", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShipmentMethodId",
                table: "Orders",
                column: "ShipmentMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_ProductGarrantys_ProductGarrantyId",
                table: "OrderItem",
                column: "ProductGarrantyId",
                principalTable: "ProductGarrantys",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DiscountCodes_DiscountCodeId",
                table: "Orders",
                column: "DiscountCodeId",
                principalTable: "DiscountCodes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PaymentInformaation_PaymentInformaationId",
                table: "Orders",
                column: "PaymentInformaationId",
                principalTable: "PaymentInformaation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ShippingMethods_ShipmentMethodId",
                table: "Orders",
                column: "ShipmentMethodId",
                principalTable: "ShippingMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentInformaation_PeymentMethods_PaymentMethodId",
                table: "PaymentInformaation",
                column: "PaymentMethodId",
                principalTable: "PeymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
