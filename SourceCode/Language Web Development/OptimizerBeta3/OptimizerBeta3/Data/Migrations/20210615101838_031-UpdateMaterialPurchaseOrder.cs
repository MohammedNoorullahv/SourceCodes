using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _031UpdateMaterialPurchaseOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "materialPurchaseOrders",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryTo",
                table: "materialPurchaseOrders",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "materialPurchaseOrders",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModeofTransport",
                table: "materialPurchaseOrders",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderStatus",
                table: "materialPurchaseOrders",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "POType",
                table: "materialPurchaseOrders",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentTerms",
                table: "materialPurchaseOrders",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Season",
                table: "materialPurchaseOrders",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "materialPurchaseOrders",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SupplierName",
                table: "materialPurchaseOrders",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeofOrder",
                table: "materialPurchaseOrders",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitName",
                table: "materialPurchaseOrders",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "materialPurchaseOrders");

            migrationBuilder.DropColumn(
                name: "DeliveryTo",
                table: "materialPurchaseOrders");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "materialPurchaseOrders");

            migrationBuilder.DropColumn(
                name: "ModeofTransport",
                table: "materialPurchaseOrders");

            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "materialPurchaseOrders");

            migrationBuilder.DropColumn(
                name: "POType",
                table: "materialPurchaseOrders");

            migrationBuilder.DropColumn(
                name: "PaymentTerms",
                table: "materialPurchaseOrders");

            migrationBuilder.DropColumn(
                name: "Season",
                table: "materialPurchaseOrders");

            migrationBuilder.DropColumn(
                name: "Source",
                table: "materialPurchaseOrders");

            migrationBuilder.DropColumn(
                name: "SupplierName",
                table: "materialPurchaseOrders");

            migrationBuilder.DropColumn(
                name: "TypeofOrder",
                table: "materialPurchaseOrders");

            migrationBuilder.DropColumn(
                name: "UnitName",
                table: "materialPurchaseOrders");
        }
    }
}
