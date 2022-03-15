using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _313UpdateStockTransfer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_StockTransfers_FKQuality",
                table: "StockTransfers",
                column: "FKQuality");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransferDetails_FKQuality",
                table: "StockTransferDetails",
                column: "FKQuality");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransferDetails_lookUpMasters_FKQuality",
                table: "StockTransferDetails",
                column: "FKQuality",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransfers_lookUpMasters_FKQuality",
                table: "StockTransfers",
                column: "FKQuality",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockTransferDetails_lookUpMasters_FKQuality",
                table: "StockTransferDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransfers_lookUpMasters_FKQuality",
                table: "StockTransfers");

            migrationBuilder.DropIndex(
                name: "IX_StockTransfers_FKQuality",
                table: "StockTransfers");

            migrationBuilder.DropIndex(
                name: "IX_StockTransferDetails_FKQuality",
                table: "StockTransferDetails");
        }
    }
}
