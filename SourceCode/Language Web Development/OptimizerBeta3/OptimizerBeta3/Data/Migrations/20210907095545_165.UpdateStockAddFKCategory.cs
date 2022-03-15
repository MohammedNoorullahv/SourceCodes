using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _165UpdateStockAddFKCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_stockWithArticles_FKCategory",
                table: "stockWithArticles",
                column: "FKCategory");

            migrationBuilder.CreateIndex(
                name: "IX_stocks_FKCategory",
                table: "stocks",
                column: "FKCategory");

            migrationBuilder.AddForeignKey(
                name: "FK_stocks_lookUpMasters_FKCategory",
                table: "stocks",
                column: "FKCategory",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_stockWithArticles_lookUpMasters_FKCategory",
                table: "stockWithArticles",
                column: "FKCategory",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_stocks_lookUpMasters_FKCategory",
                table: "stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_stockWithArticles_lookUpMasters_FKCategory",
                table: "stockWithArticles");

            migrationBuilder.DropIndex(
                name: "IX_stockWithArticles_FKCategory",
                table: "stockWithArticles");

            migrationBuilder.DropIndex(
                name: "IX_stocks_FKCategory",
                table: "stocks");
        }
    }
}
