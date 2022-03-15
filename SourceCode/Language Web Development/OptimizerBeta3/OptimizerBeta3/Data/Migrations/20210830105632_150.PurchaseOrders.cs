using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _150PurchaseOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_purchaseOrders_FKCategory",
                table: "purchaseOrders",
                column: "FKCategory");

            migrationBuilder.CreateIndex(
                name: "IX_materialPurchaseOrders_FKCategory",
                table: "materialPurchaseOrders",
                column: "FKCategory");

            migrationBuilder.AddForeignKey(
                name: "FK_materialPurchaseOrders_lookUpMasters_FKCategory",
                table: "materialPurchaseOrders",
                column: "FKCategory",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_purchaseOrders_lookUpMasters_FKCategory",
                table: "purchaseOrders",
                column: "FKCategory",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_materialPurchaseOrders_lookUpMasters_FKCategory",
                table: "materialPurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_purchaseOrders_lookUpMasters_FKCategory",
                table: "purchaseOrders");

            migrationBuilder.DropIndex(
                name: "IX_purchaseOrders_FKCategory",
                table: "purchaseOrders");

            migrationBuilder.DropIndex(
                name: "IX_materialPurchaseOrders_FKCategory",
                table: "materialPurchaseOrders");
        }
    }
}
