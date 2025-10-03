using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace berozkala_backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeSubset_ProductAttribute_ProductAttributeId",
                table: "AttributeSubset");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketProduct_ProductGarranty_SelectedGarrantyId",
                table: "BasketProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_ProductGarranty_ProductGarrantyId",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttribute_Products_ProductId",
                table: "ProductAttribute");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductGarranty_Products_ProductId",
                table: "ProductGarranty");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_Products_ProductId",
                table: "ProductImage");

            migrationBuilder.DropIndex(
                name: "IX_AttributeSubset_ProductAttributeId",
                table: "AttributeSubset");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductImage",
                table: "ProductImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductGarranty",
                table: "ProductGarranty");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductAttribute",
                table: "ProductAttribute");

            migrationBuilder.DropColumn(
                name: "ProductAttributeId",
                table: "AttributeSubset");

            migrationBuilder.RenameTable(
                name: "ProductImage",
                newName: "ProductImages");

            migrationBuilder.RenameTable(
                name: "ProductGarranty",
                newName: "ProductGarrantys");

            migrationBuilder.RenameTable(
                name: "ProductAttribute",
                newName: "ProductAttributes");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImage_ProductId",
                table: "ProductImages",
                newName: "IX_ProductImages_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductGarranty_ProductId",
                table: "ProductGarrantys",
                newName: "IX_ProductGarrantys_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductAttribute_ProductId",
                table: "ProductAttributes",
                newName: "IX_ProductAttributes_ProductId");

            migrationBuilder.AddColumn<int>(
                name: "AttributeId",
                table: "AttributeSubset",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductImages",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductGarrantys",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductAttributes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductImages",
                table: "ProductImages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductGarrantys",
                table: "ProductGarrantys",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductAttributes",
                table: "ProductAttributes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeSubset_AttributeId",
                table: "AttributeSubset",
                column: "AttributeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeSubset_ProductAttributes_AttributeId",
                table: "AttributeSubset",
                column: "AttributeId",
                principalTable: "ProductAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProduct_ProductGarrantys_SelectedGarrantyId",
                table: "BasketProduct",
                column: "SelectedGarrantyId",
                principalTable: "ProductGarrantys",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_ProductGarrantys_ProductGarrantyId",
                table: "OrderItem",
                column: "ProductGarrantyId",
                principalTable: "ProductGarrantys",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributes_Products_ProductId",
                table: "ProductAttributes",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductGarrantys_Products_ProductId",
                table: "ProductGarrantys",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeSubset_ProductAttributes_AttributeId",
                table: "AttributeSubset");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketProduct_ProductGarrantys_SelectedGarrantyId",
                table: "BasketProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_ProductGarrantys_ProductGarrantyId",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_Products_ProductId",
                table: "ProductAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductGarrantys_Products_ProductId",
                table: "ProductGarrantys");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_AttributeSubset_AttributeId",
                table: "AttributeSubset");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductImages",
                table: "ProductImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductGarrantys",
                table: "ProductGarrantys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductAttributes",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "AttributeId",
                table: "AttributeSubset");

            migrationBuilder.RenameTable(
                name: "ProductImages",
                newName: "ProductImage");

            migrationBuilder.RenameTable(
                name: "ProductGarrantys",
                newName: "ProductGarranty");

            migrationBuilder.RenameTable(
                name: "ProductAttributes",
                newName: "ProductAttribute");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImage",
                newName: "IX_ProductImage_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductGarrantys_ProductId",
                table: "ProductGarranty",
                newName: "IX_ProductGarranty_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductAttributes_ProductId",
                table: "ProductAttribute",
                newName: "IX_ProductAttribute_ProductId");

            migrationBuilder.AddColumn<int>(
                name: "ProductAttributeId",
                table: "AttributeSubset",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductImage",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductGarranty",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductAttribute",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductImage",
                table: "ProductImage",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductGarranty",
                table: "ProductGarranty",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductAttribute",
                table: "ProductAttribute",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeSubset_ProductAttributeId",
                table: "AttributeSubset",
                column: "ProductAttributeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeSubset_ProductAttribute_ProductAttributeId",
                table: "AttributeSubset",
                column: "ProductAttributeId",
                principalTable: "ProductAttribute",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProduct_ProductGarranty_SelectedGarrantyId",
                table: "BasketProduct",
                column: "SelectedGarrantyId",
                principalTable: "ProductGarranty",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_ProductGarranty_ProductGarrantyId",
                table: "OrderItem",
                column: "ProductGarrantyId",
                principalTable: "ProductGarranty",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttribute_Products_ProductId",
                table: "ProductAttribute",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductGarranty_Products_ProductId",
                table: "ProductGarranty",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_Products_ProductId",
                table: "ProductImage",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
