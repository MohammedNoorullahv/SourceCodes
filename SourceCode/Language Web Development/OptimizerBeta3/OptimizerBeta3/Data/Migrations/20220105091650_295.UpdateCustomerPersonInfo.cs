using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _295UpdateCustomerPersonInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMarried",
                table: "customerPerson");

            migrationBuilder.AddColumn<int>(
                name: "FKGender",
                table: "customerPerson",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKMaritalStatus",
                table: "customerPerson",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FKGender",
                table: "customerPerson");

            migrationBuilder.DropColumn(
                name: "FKMaritalStatus",
                table: "customerPerson");

            migrationBuilder.AddColumn<bool>(
                name: "IsMarried",
                table: "customerPerson",
                type: "bit",
                nullable: true);
        }
    }
}
