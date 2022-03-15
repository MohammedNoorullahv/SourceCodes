using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _130UpdateStockTransferDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockTransferDetails_articleDetails_FKArticle",
                table: "StockTransferDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransferDetails_materials_FKMaterial",
                table: "StockTransferDetails");

            migrationBuilder.DropIndex(
                name: "IX_StockTransferDetails_FKArticle",
                table: "StockTransferDetails");

            migrationBuilder.DropIndex(
                name: "IX_StockTransferDetails_FKMaterial",
                table: "StockTransferDetails");

            migrationBuilder.RenameColumn(
                name: "FKArticle",
                table: "StockTransferDetails",
                newName: "FKArticleDetail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FKArticleDetail",
                table: "StockTransferDetails",
                newName: "FKArticle");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransferDetails_FKArticle",
                table: "StockTransferDetails",
                column: "FKArticle");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransferDetails_FKMaterial",
                table: "StockTransferDetails",
                column: "FKMaterial");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransferDetails_articleDetails_FKArticle",
                table: "StockTransferDetails",
                column: "FKArticle",
                principalTable: "articleDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransferDetails_materials_FKMaterial",
                table: "StockTransferDetails",
                column: "FKMaterial",
                principalTable: "materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
