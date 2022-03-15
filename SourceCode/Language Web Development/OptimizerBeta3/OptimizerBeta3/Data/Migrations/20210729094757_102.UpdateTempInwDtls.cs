using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _102UpdateTempInwDtls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FKCurrency",
                table: "TempInwardDtls",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKLocation",
                table: "TempInwardDtls",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKSource",
                table: "TempInwardDtls",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKUOM",
                table: "TempInwardDtls",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKUnit",
                table: "TempInwardDtls",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OrderReferenceNo",
                table: "TempInwardDtls",
                type: "Varchar(50)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FKCurrency",
                table: "TempInwardDtls");

            migrationBuilder.DropColumn(
                name: "FKLocation",
                table: "TempInwardDtls");

            migrationBuilder.DropColumn(
                name: "FKSource",
                table: "TempInwardDtls");

            migrationBuilder.DropColumn(
                name: "FKUOM",
                table: "TempInwardDtls");

            migrationBuilder.DropColumn(
                name: "FKUnit",
                table: "TempInwardDtls");

            migrationBuilder.DropColumn(
                name: "OrderReferenceNo",
                table: "TempInwardDtls");
        }
    }
}
