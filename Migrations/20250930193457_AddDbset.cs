using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace berozkala_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddDbset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductSubCategory_ProductSubCategory_CategoryId",
                table: "ProductProductSubCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSubCategory_ProductCategorys_ProductCategoryId",
                table: "ProductSubCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSubCategory",
                table: "ProductSubCategory");

            migrationBuilder.RenameTable(
                name: "ProductSubCategory",
                newName: "ProductSubCategorys");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSubCategory_ProductCategoryId",
                table: "ProductSubCategorys",
                newName: "IX_ProductSubCategorys_ProductCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSubCategorys",
                table: "ProductSubCategorys",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProductSubCategory_ProductSubCategorys_CategoryId",
                table: "ProductProductSubCategory",
                column: "CategoryId",
                principalTable: "ProductSubCategorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSubCategorys_ProductCategorys_ProductCategoryId",
                table: "ProductSubCategorys",
                column: "ProductCategoryId",
                principalTable: "ProductCategorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductSubCategory_ProductSubCategorys_CategoryId",
                table: "ProductProductSubCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSubCategorys_ProductCategorys_ProductCategoryId",
                table: "ProductSubCategorys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSubCategorys",
                table: "ProductSubCategorys");

            migrationBuilder.RenameTable(
                name: "ProductSubCategorys",
                newName: "ProductSubCategory");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSubCategorys_ProductCategoryId",
                table: "ProductSubCategory",
                newName: "IX_ProductSubCategory_ProductCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSubCategory",
                table: "ProductSubCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProductSubCategory_ProductSubCategory_CategoryId",
                table: "ProductProductSubCategory",
                column: "CategoryId",
                principalTable: "ProductSubCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSubCategory_ProductCategorys_ProductCategoryId",
                table: "ProductSubCategory",
                column: "ProductCategoryId",
                principalTable: "ProductCategorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
