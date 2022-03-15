using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _240UpdatePurchaseOrderAddOrderMethod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FKOrderMethod",
                table: "purchaseOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OrderMethod",
                table: "purchaseOrders",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FKOrderMethod",
                table: "purchaseOrders");

            migrationBuilder.DropColumn(
                name: "OrderMethod",
                table: "purchaseOrders");
        }
    }
}
