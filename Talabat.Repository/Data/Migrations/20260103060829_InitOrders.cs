using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Talabat.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShippingAdress_Lname",
                table: "Orders",
                newName: "ShippingAdress_LastName");

            migrationBuilder.RenameColumn(
                name: "ShippingAdress_Fname",
                table: "Orders",
                newName: "ShippingAdress_FirstName");

            migrationBuilder.RenameColumn(
                name: "Prodcut_ProductName",
                table: "OrderItems",
                newName: "ItemOrdered_ProductName");

            migrationBuilder.RenameColumn(
                name: "Prodcut_ProductId",
                table: "OrderItems",
                newName: "ItemOrdered_ProductId");

            migrationBuilder.RenameColumn(
                name: "Prodcut_PictureUrl",
                table: "OrderItems",
                newName: "ItemOrdered_PictureUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShippingAdress_LastName",
                table: "Orders",
                newName: "ShippingAdress_Lname");

            migrationBuilder.RenameColumn(
                name: "ShippingAdress_FirstName",
                table: "Orders",
                newName: "ShippingAdress_Fname");

            migrationBuilder.RenameColumn(
                name: "ItemOrdered_ProductName",
                table: "OrderItems",
                newName: "Prodcut_ProductName");

            migrationBuilder.RenameColumn(
                name: "ItemOrdered_ProductId",
                table: "OrderItems",
                newName: "Prodcut_ProductId");

            migrationBuilder.RenameColumn(
                name: "ItemOrdered_PictureUrl",
                table: "OrderItems",
                newName: "Prodcut_PictureUrl");
        }
    }
}
