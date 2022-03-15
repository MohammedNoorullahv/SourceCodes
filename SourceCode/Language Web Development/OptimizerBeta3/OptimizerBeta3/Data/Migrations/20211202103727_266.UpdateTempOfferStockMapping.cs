using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _266UpdateTempOfferStockMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArticleDetalId",
                table: "TempOfferStockMappings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ArticleGroupId",
                table: "TempOfferStockMappings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "TempOfferStockMappings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArticleDetalId",
                table: "TempOfferStockMappings");

            migrationBuilder.DropColumn(
                name: "ArticleGroupId",
                table: "TempOfferStockMappings");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "TempOfferStockMappings");
        }
    }
}
