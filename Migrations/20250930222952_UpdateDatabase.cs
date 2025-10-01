using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace berozkala_backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductImage_ImagesUrlsId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductProductSubCategory");

            migrationBuilder.DropTable(
                name: "ProductSubCategorys");

            migrationBuilder.DropTable(
                name: "ProductCategorys");

            migrationBuilder.DropIndex(
                name: "IX_Products_ImagesUrlsId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ImagesUrlsId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductImage",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Categorys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoryName = table.Column<string>(type: "TEXT", nullable: false),
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubCategorys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SubCategoryName = table.Column<string>(type: "TEXT", nullable: false),
                    ProductCategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategorys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategorys_Categorys_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "Categorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductsSubCategorys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    SubCategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsSubCategorys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductsSubCategorys_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsSubCategorys_SubCategorys_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ProductId",
                table: "ProductImage",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsSubCategorys_ProductId",
                table: "ProductsSubCategorys",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsSubCategorys_SubCategoryId",
                table: "ProductsSubCategorys",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategorys_ProductCategoryId",
                table: "SubCategorys",
                column: "ProductCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_Products_ProductId",
                table: "ProductImage",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_Products_ProductId",
                table: "ProductImage");

            migrationBuilder.DropTable(
                name: "ProductsSubCategorys");

            migrationBuilder.DropTable(
                name: "SubCategorys");

            migrationBuilder.DropTable(
                name: "Categorys");

            migrationBuilder.DropIndex(
                name: "IX_ProductImage_ProductId",
                table: "ProductImage");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductImage");

            migrationBuilder.AddColumn<int>(
                name: "ImagesUrlsId",
                table: "Products",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductCategorys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoryName = table.Column<string>(type: "TEXT", nullable: false),
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategorys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductSubCategorys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductCategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    SubCategoryName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSubCategorys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSubCategorys_ProductCategorys_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                        name: "FK_ProductProductSubCategory_ProductSubCategorys_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ProductSubCategorys",
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
                name: "IX_Products_ImagesUrlsId",
                table: "Products",
                column: "ImagesUrlsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductSubCategory_ProductsId",
                table: "ProductProductSubCategory",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubCategorys_ProductCategoryId",
                table: "ProductSubCategorys",
                column: "ProductCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductImage_ImagesUrlsId",
                table: "Products",
                column: "ImagesUrlsId",
                principalTable: "ProductImage",
                principalColumn: "Id");
        }
    }
}
