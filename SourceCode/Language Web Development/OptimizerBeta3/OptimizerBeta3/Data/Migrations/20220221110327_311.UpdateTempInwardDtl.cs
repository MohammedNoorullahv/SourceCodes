using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _311UpdateTempInwardDtl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_inwardDetails_FKQuality",
                table: "inwardDetails",
                column: "FKQuality");

            migrationBuilder.AddForeignKey(
                name: "FK_inwardDetails_lookUpMasters_FKQuality",
                table: "inwardDetails",
                column: "FKQuality",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inwardDetails_lookUpMasters_FKQuality",
                table: "inwardDetails");

            migrationBuilder.DropIndex(
                name: "IX_inwardDetails_FKQuality",
                table: "inwardDetails");
        }
    }
}
