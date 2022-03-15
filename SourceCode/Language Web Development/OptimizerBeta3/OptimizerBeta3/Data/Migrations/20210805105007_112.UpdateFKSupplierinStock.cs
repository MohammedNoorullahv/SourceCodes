using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _112UpdateFKSupplierinStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FKSupplier",
                table: "TempInwardDtls",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKSupplier",
                table: "stocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKSupplier",
                table: "inwardDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FKSupplier",
                table: "TempInwardDtls");

            migrationBuilder.DropColumn(
                name: "FKSupplier",
                table: "stocks");

            migrationBuilder.DropColumn(
                name: "FKSupplier",
                table: "inwardDetails");
        }
    }
}
