using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _185UpdateLGPODetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LGPurchaseOrderDetails_articleDetails_FKUOM",
                table: "LGPurchaseOrderDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_LGPurchaseOrderDetails_lookUpMasters_FKUOM",
                table: "LGPurchaseOrderDetails",
                column: "FKUOM",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LGPurchaseOrderDetails_lookUpMasters_FKUOM",
                table: "LGPurchaseOrderDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_LGPurchaseOrderDetails_articleDetails_FKUOM",
                table: "LGPurchaseOrderDetails",
                column: "FKUOM",
                principalTable: "articleDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
