using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _207UpdateInvoiceSetInvoiceTo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaterialorFinishedProduct",
                table: "Invoices",
                newName: "InvoiceTo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InvoiceTo",
                table: "Invoices",
                newName: "MaterialorFinishedProduct");
        }
    }
}
