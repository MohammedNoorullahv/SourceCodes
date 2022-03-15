using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _265UpdateStockDtlMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArticleColor",
                table: "OfferDtlStockMappings",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArticleGroup",
                table: "OfferDtlStockMappings",
                type: "varchar(30)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArticleName",
                table: "OfferDtlStockMappings",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Stock",
                table: "OfferDtlStockMappings",
                type: "varchar(20)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArticleColor",
                table: "OfferDtlStockMappings");

            migrationBuilder.DropColumn(
                name: "ArticleGroup",
                table: "OfferDtlStockMappings");

            migrationBuilder.DropColumn(
                name: "ArticleName",
                table: "OfferDtlStockMappings");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "OfferDtlStockMappings");
        }
    }
}
