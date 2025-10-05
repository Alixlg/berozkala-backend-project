using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace berozkala_backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFluentApli8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsSubCategorys",
                table: "ProductsSubCategorys");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsSubCategorys",
                table: "ProductsSubCategorys",
                columns: new[] { "Id", "ProductId", "SubCategoryId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductsSubCategorys_ProductId",
                table: "ProductsSubCategorys",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsSubCategorys",
                table: "ProductsSubCategorys");

            migrationBuilder.DropIndex(
                name: "IX_ProductsSubCategorys_ProductId",
                table: "ProductsSubCategorys");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsSubCategorys",
                table: "ProductsSubCategorys",
                columns: new[] { "ProductId", "SubCategoryId" });
        }
    }
}
