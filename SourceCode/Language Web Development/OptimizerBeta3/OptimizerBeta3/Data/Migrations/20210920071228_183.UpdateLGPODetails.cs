using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _183UpdateLGPODetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LGPurchaseOrderDetails_purchaseOrderMains_FKPurchaseOrderMain",
                table: "LGPurchaseOrderDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_LGPurchaseOrderDetails_LGPurchaseOrderMains_FKPurchaseOrderMain",
                table: "LGPurchaseOrderDetails",
                column: "FKPurchaseOrderMain",
                principalTable: "LGPurchaseOrderMains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LGPurchaseOrderDetails_LGPurchaseOrderMains_FKPurchaseOrderMain",
                table: "LGPurchaseOrderDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_LGPurchaseOrderDetails_purchaseOrderMains_FKPurchaseOrderMain",
                table: "LGPurchaseOrderDetails",
                column: "FKPurchaseOrderMain",
                principalTable: "purchaseOrderMains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
