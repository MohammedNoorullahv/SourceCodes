using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _216UpdateTempInvDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "InvoiceDetails",
                type: "Varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "Varchar(50)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "InvoiceDetails",
                type: "Varchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "Varchar(100)",
                oldNullable: true);
        }
    }
}
