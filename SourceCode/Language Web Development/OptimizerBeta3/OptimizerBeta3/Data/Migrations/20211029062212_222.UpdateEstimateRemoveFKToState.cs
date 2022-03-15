using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _222UpdateEstimateRemoveFKToState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estimates_StateMasters_FKToState",
                table: "Estimates");

            migrationBuilder.DropIndex(
                name: "IX_Estimates_FKToState",
                table: "Estimates");

            migrationBuilder.DropColumn(
                name: "FKToState",
                table: "Estimates");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FKToState",
                table: "Estimates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Estimates_FKToState",
                table: "Estimates",
                column: "FKToState");

            migrationBuilder.AddForeignKey(
                name: "FK_Estimates_StateMasters_FKToState",
                table: "Estimates",
                column: "FKToState",
                principalTable: "StateMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
