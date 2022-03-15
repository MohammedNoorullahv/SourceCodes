using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _168UpdateStockwithArticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FKOrderId",
                table: "stockWithArticles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKOrderMainId",
                table: "stockWithArticles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FKOrderId",
                table: "stockWithArticles");

            migrationBuilder.DropColumn(
                name: "FKOrderMainId",
                table: "stockWithArticles");
        }
    }
}
