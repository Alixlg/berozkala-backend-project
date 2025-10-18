using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace berozkala_backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFluentApli13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Admins_AdminAccountId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_Users_UserAccountId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Address_ReceiverAddressId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

            migrationBuilder.RenameTable(
                name: "Address",
                newName: "Addresses");

            migrationBuilder.RenameColumn(
                name: "UserAccountId",
                table: "Addresses",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "AdminAccountId",
                table: "Addresses",
                newName: "AdminId");

            migrationBuilder.RenameIndex(
                name: "IX_Address_UserAccountId",
                table: "Addresses",
                newName: "IX_Addresses_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Address_AdminAccountId",
                table: "Addresses",
                newName: "IX_Addresses_AdminId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Admins_AdminId",
                table: "Addresses",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Users_UserId",
                table: "Addresses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Addresses_ReceiverAddressId",
                table: "Orders",
                column: "ReceiverAddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Admins_AdminId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Users_UserId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Addresses_ReceiverAddressId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Address",
                newName: "UserAccountId");

            migrationBuilder.RenameColumn(
                name: "AdminId",
                table: "Address",
                newName: "AdminAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_UserId",
                table: "Address",
                newName: "IX_Address_UserAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_AdminId",
                table: "Address",
                newName: "IX_Address_AdminAccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Admins_AdminAccountId",
                table: "Address",
                column: "AdminAccountId",
                principalTable: "Admins",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Users_UserAccountId",
                table: "Address",
                column: "UserAccountId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Address_ReceiverAddressId",
                table: "Orders",
                column: "ReceiverAddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
