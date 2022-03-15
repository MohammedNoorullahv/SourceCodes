using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _142UpdatesinInvoiceAndStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FKHSNCode",
                table: "stocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKFromState",
                table: "InvoiceToPersons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKToState",
                table: "InvoiceToPersons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKFromState",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKToState",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FKHSNCode",
                table: "stocks");

            migrationBuilder.DropColumn(
                name: "FKFromState",
                table: "InvoiceToPersons");

            migrationBuilder.DropColumn(
                name: "FKToState",
                table: "InvoiceToPersons");

            migrationBuilder.DropColumn(
                name: "FKFromState",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "FKToState",
                table: "Invoices");
        }
    }
}
