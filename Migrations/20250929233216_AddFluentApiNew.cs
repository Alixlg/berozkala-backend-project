using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace berozkala_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddFluentApiNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSubCategory_ProductCategorys_ProductCategoryId",
                table: "ProductSubCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSubCategory_Products_ProductId",
                table: "ProductSubCategory");

            migrationBuilder.DropIndex(
                name: "IX_ProductSubCategory_ProductId",
                table: "ProductSubCategory");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductSubCategory");

            migrationBuilder.AlterColumn<int>(
                name: "ProductCategoryId",
                table: "ProductSubCategory",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ProductProductSubCategory",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProductSubCategory", x => new { x.CategoryId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_ProductProductSubCategory_ProductSubCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ProductSubCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductProductSubCategory_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductSubCategory_ProductsId",
                table: "ProductProductSubCategory",
                column: "ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSubCategory_ProductCategorys_ProductCategoryId",
                table: "ProductSubCategory",
                column: "ProductCategoryId",
                principalTable: "ProductCategorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSubCategory_ProductCategorys_ProductCategoryId",
                table: "ProductSubCategory");

            migrationBuilder.DropTable(
                name: "ProductProductSubCategory");

            migrationBuilder.AlterColumn<int>(
                name: "ProductCategoryId",
                table: "ProductSubCategory",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductSubCategory",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubCategory_ProductId",
                table: "ProductSubCategory",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSubCategory_ProductCategorys_ProductCategoryId",
                table: "ProductSubCategory",
                column: "ProductCategoryId",
                principalTable: "ProductCategorys",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSubCategory_Products_ProductId",
                table: "ProductSubCategory",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
