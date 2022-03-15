using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _065UpdateInward : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "inwards",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "inwards",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocumentType",
                table: "inwards",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "POType",
                table: "inwards",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "inwards",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Season",
                table: "inwards",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "inwards",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SupplierName",
                table: "inwards",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitName",
                table: "inwards",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "inwards");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "inwards");

            migrationBuilder.DropColumn(
                name: "DocumentType",
                table: "inwards");

            migrationBuilder.DropColumn(
                name: "POType",
                table: "inwards");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "inwards");

            migrationBuilder.DropColumn(
                name: "Season",
                table: "inwards");

            migrationBuilder.DropColumn(
                name: "Source",
                table: "inwards");

            migrationBuilder.DropColumn(
                name: "SupplierName",
                table: "inwards");

            migrationBuilder.DropColumn(
                name: "UnitName",
                table: "inwards");
        }
    }
}
