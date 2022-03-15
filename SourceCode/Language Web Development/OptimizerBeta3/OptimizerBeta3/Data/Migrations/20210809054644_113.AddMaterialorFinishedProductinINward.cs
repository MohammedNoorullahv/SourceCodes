using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _113AddMaterialorFinishedProductinINward : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaterialorFinishedProduct",
                table: "TempInwardDtls",
                type: "varchar(2)",
                maxLength: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaterialorFinishedProduct",
                table: "inwardDetails",
                type: "varchar(2)",
                maxLength: 2,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaterialorFinishedProduct",
                table: "TempInwardDtls");

            migrationBuilder.DropColumn(
                name: "MaterialorFinishedProduct",
                table: "inwardDetails");
        }
    }
}
