using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace berozkala_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GuId",
                table: "Products",
                newName: "Guid");

            migrationBuilder.RenameColumn(
                name: "GuId",
                table: "Orders",
                newName: "Guid");

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "SubmitedDiscountCode",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "ProductSubCategory",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "ProductImage",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "ProductGarranty",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "ProductCategorys",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "ProductAttribute",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "PaymentInformaation",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "OrderItem",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "FavoriteProduct",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "DiscountCodes",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "BasketProduct",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "AttributeSubset",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "Address",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Guid",
                table: "SubmitedDiscountCode");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "ProductSubCategory");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "ProductImage");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "ProductGarranty");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "ProductCategorys");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "ProductAttribute");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "PaymentInformaation");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "FavoriteProduct");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "DiscountCodes");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "BasketProduct");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "AttributeSubset");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Address");

            migrationBuilder.RenameColumn(
                name: "Guid",
                table: "Products",
                newName: "GuId");

            migrationBuilder.RenameColumn(
                name: "Guid",
                table: "Orders",
                newName: "GuId");
        }
    }
}
