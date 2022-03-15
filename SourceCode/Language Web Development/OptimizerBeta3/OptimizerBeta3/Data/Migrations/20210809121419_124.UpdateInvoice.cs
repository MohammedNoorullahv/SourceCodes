using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _124UpdateInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "IIQuantity",
                table: "TempInvoiceDtls",
                type: "Decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "Destination",
                table: "Invoices",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FKDestination",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKLocation",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Invoices",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaterialorFinishedProduct",
                table: "Invoices",
                type: "varchar(2)",
                maxLength: 2,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Destination",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "FKDestination",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "FKLocation",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "MaterialorFinishedProduct",
                table: "Invoices");

            migrationBuilder.AlterColumn<decimal>(
                name: "IIQuantity",
                table: "TempInvoiceDtls",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(18,2)");
        }
    }
}
