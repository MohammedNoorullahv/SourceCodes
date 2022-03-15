using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _149PurchaseOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "purchaseOrders",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FKCategory",
                table: "purchaseOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "materialPurchaseOrders",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FKCategory",
                table: "materialPurchaseOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "purchaseOrders");

            migrationBuilder.DropColumn(
                name: "FKCategory",
                table: "purchaseOrders");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "materialPurchaseOrders");

            migrationBuilder.DropColumn(
                name: "FKCategory",
                table: "materialPurchaseOrders");
        }
    }
}
