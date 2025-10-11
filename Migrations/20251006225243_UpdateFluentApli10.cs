using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace berozkala_backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFluentApli10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketProduct_ProductGarrantys_SelectedGarrantyId",
                table: "BasketProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketProduct_Products_ProductId",
                table: "BasketProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketProduct_Users_UserId",
                table: "BasketProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BasketProduct",
                table: "BasketProduct");

            migrationBuilder.RenameTable(
                name: "BasketProduct",
                newName: "BasketsProducts");

            migrationBuilder.RenameIndex(
                name: "IX_BasketProduct_UserId",
                table: "BasketsProducts",
                newName: "IX_BasketsProducts_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketProduct_SelectedGarrantyId",
                table: "BasketsProducts",
                newName: "IX_BasketsProducts_SelectedGarrantyId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketProduct_ProductId",
                table: "BasketsProducts",
                newName: "IX_BasketsProducts_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BasketsProducts",
                table: "BasketsProducts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketsProducts_ProductGarrantys_SelectedGarrantyId",
                table: "BasketsProducts",
                column: "SelectedGarrantyId",
                principalTable: "ProductGarrantys",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketsProducts_Products_ProductId",
                table: "BasketsProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketsProducts_Users_UserId",
                table: "BasketsProducts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketsProducts_ProductGarrantys_SelectedGarrantyId",
                table: "BasketsProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketsProducts_Products_ProductId",
                table: "BasketsProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketsProducts_Users_UserId",
                table: "BasketsProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BasketsProducts",
                table: "BasketsProducts");

            migrationBuilder.RenameTable(
                name: "BasketsProducts",
                newName: "BasketProduct");

            migrationBuilder.RenameIndex(
                name: "IX_BasketsProducts_UserId",
                table: "BasketProduct",
                newName: "IX_BasketProduct_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketsProducts_SelectedGarrantyId",
                table: "BasketProduct",
                newName: "IX_BasketProduct_SelectedGarrantyId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketsProducts_ProductId",
                table: "BasketProduct",
                newName: "IX_BasketProduct_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BasketProduct",
                table: "BasketProduct",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProduct_ProductGarrantys_SelectedGarrantyId",
                table: "BasketProduct",
                column: "SelectedGarrantyId",
                principalTable: "ProductGarrantys",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProduct_Products_ProductId",
                table: "BasketProduct",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProduct_Users_UserId",
                table: "BasketProduct",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
