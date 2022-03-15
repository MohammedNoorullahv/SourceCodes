using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _102UpdateStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_stocks_lookUpMasters_FKIIUOM",
                table: "stocks");

            migrationBuilder.DropIndex(
                name: "IX_stocks_FKIIUOM",
                table: "stocks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_stocks_FKIIUOM",
                table: "stocks",
                column: "FKIIUOM");

            migrationBuilder.AddForeignKey(
                name: "FK_stocks_lookUpMasters_FKIIUOM",
                table: "stocks",
                column: "FKIIUOM",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
