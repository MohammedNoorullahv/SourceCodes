using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _252UpdateInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InvoiceValue",
                table: "Invoices",
                newName: "RoundoffPlus");

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercentage",
                table: "Invoices",
                type: "Decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountValue",
                table: "Invoices",
                type: "Decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ItemsValue",
                table: "Invoices",
                type: "Decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RoundoffMinus",
                table: "Invoices",
                type: "Decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPercentage",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "DiscountValue",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ItemsValue",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "RoundoffMinus",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "RoundoffPlus",
                table: "Invoices",
                newName: "InvoiceValue");
        }
    }
}
