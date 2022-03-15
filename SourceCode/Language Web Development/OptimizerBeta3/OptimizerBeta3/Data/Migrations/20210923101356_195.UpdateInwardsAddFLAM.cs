using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _195UpdateInwardsAddFLAM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaterialorFinishedProduct",
                table: "inwards");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "inwards",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FKCategory",
                table: "inwards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FLAM",
                table: "inwards",
                type: "varchar(1)",
                maxLength: 1,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "inwards");

            migrationBuilder.DropColumn(
                name: "FKCategory",
                table: "inwards");

            migrationBuilder.DropColumn(
                name: "FLAM",
                table: "inwards");

            migrationBuilder.AddColumn<string>(
                name: "MaterialorFinishedProduct",
                table: "inwards",
                type: "varchar(2)",
                maxLength: 2,
                nullable: true);
        }
    }
}
