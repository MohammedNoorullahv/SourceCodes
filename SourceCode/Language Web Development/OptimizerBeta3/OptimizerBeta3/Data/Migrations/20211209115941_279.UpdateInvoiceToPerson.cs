using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _279UpdateInvoiceToPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillValue",
                table: "InvoiceToPersons");

            migrationBuilder.AddColumn<int>(
                name: "FKEstimate",
                table: "InvoiceToPersons",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FKEstimate",
                table: "InvoiceToPersons");

            migrationBuilder.AddColumn<decimal>(
                name: "BillValue",
                table: "InvoiceToPersons",
                type: "Decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
