using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _208UpdateCounterInvoiceSetInvoiceTo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaterialorFinishedProduct",
                table: "InvoiceToPersons",
                newName: "InvoiceTo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InvoiceTo",
                table: "InvoiceToPersons",
                newName: "MaterialorFinishedProduct");
        }
    }
}
