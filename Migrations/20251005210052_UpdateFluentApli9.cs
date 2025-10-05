using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace berozkala_backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFluentApli9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsSubCategorys",
                table: "ProductsSubCategorys");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ProductsSubCategorys",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsSubCategorys",
                table: "ProductsSubCategorys",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsSubCategorys",
                table: "ProductsSubCategorys");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ProductsSubCategorys",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsSubCategorys",
                table: "ProductsSubCategorys",
                columns: new[] { "Id", "ProductId", "SubCategoryId" });
        }
    }
}
