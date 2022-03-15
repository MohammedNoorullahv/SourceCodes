using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _229UpdateInvoiceDtlAddUOM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FKHSNCode",
                table: "TempInvoiceDtls",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UOM",
                table: "TempInvoiceDtls",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FKHSNCode",
                table: "TempInvoiceDtls");

            migrationBuilder.DropColumn(
                name: "UOM",
                table: "TempInvoiceDtls");
        }
    }
}
