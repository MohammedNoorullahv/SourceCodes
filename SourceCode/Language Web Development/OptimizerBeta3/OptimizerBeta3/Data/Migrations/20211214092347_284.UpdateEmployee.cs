using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _284UpdateEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FKGender",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_FKGender",
                table: "Employees",
                column: "FKGender");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_lookUpMasters_FKGender",
                table: "Employees",
                column: "FKGender",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_lookUpMasters_FKGender",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_FKGender",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "FKGender",
                table: "Employees");
        }
    }
}
