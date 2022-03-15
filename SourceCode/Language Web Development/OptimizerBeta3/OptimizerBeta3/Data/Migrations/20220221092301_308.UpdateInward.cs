using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _308UpdateInward : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_inwards_FKQuality",
                table: "inwards",
                column: "FKQuality");

            migrationBuilder.AddForeignKey(
                name: "FK_inwards_lookUpMasters_FKQuality",
                table: "inwards",
                column: "FKQuality",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inwards_lookUpMasters_FKQuality",
                table: "inwards");

            migrationBuilder.DropIndex(
                name: "IX_inwards_FKQuality",
                table: "inwards");
        }
    }
}
