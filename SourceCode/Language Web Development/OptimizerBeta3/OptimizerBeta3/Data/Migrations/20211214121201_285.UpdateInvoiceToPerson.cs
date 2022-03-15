using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _285UpdateInvoiceToPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FKSalesPerson",
                table: "InvoiceToPersons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SalesPerson",
                table: "InvoiceToPersons",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FKSalesPerson",
                table: "InvoiceToPersons");

            migrationBuilder.DropColumn(
                name: "SalesPerson",
                table: "InvoiceToPersons");
        }
    }
}
