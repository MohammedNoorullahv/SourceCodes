using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _241UpdatePurchaseOrderAddOrderMethod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_purchaseOrders_FKOrderMethod",
                table: "purchaseOrders",
                column: "FKOrderMethod");

            migrationBuilder.AddForeignKey(
                name: "FK_purchaseOrders_lookUpMasters_FKOrderMethod",
                table: "purchaseOrders",
                column: "FKOrderMethod",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_purchaseOrders_lookUpMasters_FKOrderMethod",
                table: "purchaseOrders");

            migrationBuilder.DropIndex(
                name: "IX_purchaseOrders_FKOrderMethod",
                table: "purchaseOrders");
        }
    }
}
