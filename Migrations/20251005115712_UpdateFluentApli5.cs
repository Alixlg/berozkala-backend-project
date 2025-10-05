using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace berozkala_backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFluentApli5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeSubset_ProductAttributes_AttributeId",
                table: "AttributeSubset");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeSubset",
                table: "AttributeSubset");

            migrationBuilder.RenameTable(
                name: "AttributeSubset",
                newName: "ProductSubsetAttributes");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeSubset_AttributeId",
                table: "ProductSubsetAttributes",
                newName: "IX_ProductSubsetAttributes_AttributeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSubsetAttributes",
                table: "ProductSubsetAttributes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSubsetAttributes_ProductAttributes_AttributeId",
                table: "ProductSubsetAttributes",
                column: "AttributeId",
                principalTable: "ProductAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSubsetAttributes_ProductAttributes_AttributeId",
                table: "ProductSubsetAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSubsetAttributes",
                table: "ProductSubsetAttributes");

            migrationBuilder.RenameTable(
                name: "ProductSubsetAttributes",
                newName: "AttributeSubset");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSubsetAttributes_AttributeId",
                table: "AttributeSubset",
                newName: "IX_AttributeSubset_AttributeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeSubset",
                table: "AttributeSubset",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeSubset_ProductAttributes_AttributeId",
                table: "AttributeSubset",
                column: "AttributeId",
                principalTable: "ProductAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
