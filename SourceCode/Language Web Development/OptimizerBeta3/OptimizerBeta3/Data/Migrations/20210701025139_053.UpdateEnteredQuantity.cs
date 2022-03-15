using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _053UpdateEnteredQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BalanceQty",
                table: "purchaseOrders");

            migrationBuilder.DropColumn(
                name: "CancelledQty",
                table: "purchaseOrders");

            migrationBuilder.DropColumn(
                name: "RecievedQty",
                table: "purchaseOrders");

            migrationBuilder.RenameColumn(
                name: "TotalOrderQty",
                table: "purchaseOrders",
                newName: "TotalOrderQuantity");

            migrationBuilder.AddColumn<int>(
                name: "BalanceQuantity",
                table: "purchaseOrders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CancelledQuantity",
                table: "purchaseOrders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EnteredQuantity",
                table: "purchaseOrders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecievedQuantity",
                table: "purchaseOrders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EnteredQuantity",
                table: "purchaseOrderMains",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EnteredQuantity",
                table: "purchaseOrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BalanceQuantity",
                table: "purchaseOrders");

            migrationBuilder.DropColumn(
                name: "CancelledQuantity",
                table: "purchaseOrders");

            migrationBuilder.DropColumn(
                name: "EnteredQuantity",
                table: "purchaseOrders");

            migrationBuilder.DropColumn(
                name: "RecievedQuantity",
                table: "purchaseOrders");

            migrationBuilder.DropColumn(
                name: "EnteredQuantity",
                table: "purchaseOrderMains");

            migrationBuilder.DropColumn(
                name: "EnteredQuantity",
                table: "purchaseOrderDetails");

            migrationBuilder.RenameColumn(
                name: "TotalOrderQuantity",
                table: "purchaseOrders",
                newName: "TotalOrderQty");

            migrationBuilder.AddColumn<decimal>(
                name: "BalanceQty",
                table: "purchaseOrders",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CancelledQty",
                table: "purchaseOrders",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RecievedQty",
                table: "purchaseOrders",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
