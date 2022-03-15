using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _200UpdateLGPurchaseOrderMain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LGPurchaseOrderMains_purchaseOrders_FKPurchaseOrder",
                table: "LGPurchaseOrderMains");

            migrationBuilder.AddForeignKey(
                name: "FK_LGPurchaseOrderMains_LGPurchaseOrders_FKPurchaseOrder",
                table: "LGPurchaseOrderMains",
                column: "FKPurchaseOrder",
                principalTable: "LGPurchaseOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LGPurchaseOrderMains_LGPurchaseOrders_FKPurchaseOrder",
                table: "LGPurchaseOrderMains");

            migrationBuilder.AddForeignKey(
                name: "FK_LGPurchaseOrderMains_purchaseOrders_FKPurchaseOrder",
                table: "LGPurchaseOrderMains",
                column: "FKPurchaseOrder",
                principalTable: "purchaseOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
