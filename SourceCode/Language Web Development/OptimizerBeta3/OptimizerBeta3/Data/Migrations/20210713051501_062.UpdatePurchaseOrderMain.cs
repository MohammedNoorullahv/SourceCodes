using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _062UpdatePurchaseOrderMain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_purchaseOrderMains_materialPurchaseOrders_FKPurchaseOrder",
                table: "purchaseOrderMains");

            migrationBuilder.AddForeignKey(
                name: "FK_purchaseOrderMains_purchaseOrders_FKPurchaseOrder",
                table: "purchaseOrderMains",
                column: "FKPurchaseOrder",
                principalTable: "purchaseOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_purchaseOrderMains_purchaseOrders_FKPurchaseOrder",
                table: "purchaseOrderMains");

            migrationBuilder.AddForeignKey(
                name: "FK_purchaseOrderMains_materialPurchaseOrders_FKPurchaseOrder",
                table: "purchaseOrderMains",
                column: "FKPurchaseOrder",
                principalTable: "materialPurchaseOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
