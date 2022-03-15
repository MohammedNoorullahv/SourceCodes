using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _253UpdateInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ItemsDiscountValue",
                table: "Invoices",
                type: "Decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ItemsGrossValue",
                table: "Invoices",
                type: "Decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ItemsNettValue",
                table: "Invoices",
                type: "Decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemsDiscountValue",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ItemsGrossValue",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ItemsNettValue",
                table: "Invoices");
        }
    }
}
