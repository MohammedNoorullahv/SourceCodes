using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _231UpdateDeliveryChallan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryChallans_unitMasters_FKToUnit",
                table: "DeliveryChallans");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryChallans_FKToUnit",
                table: "DeliveryChallans");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DeliveryChallans_FKToUnit",
                table: "DeliveryChallans",
                column: "FKToUnit");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryChallans_unitMasters_FKToUnit",
                table: "DeliveryChallans",
                column: "FKToUnit",
                principalTable: "unitMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
