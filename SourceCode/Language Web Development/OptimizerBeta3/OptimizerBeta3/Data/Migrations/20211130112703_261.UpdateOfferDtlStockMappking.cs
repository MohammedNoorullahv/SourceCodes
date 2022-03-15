using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _261UpdateOfferDtlStockMappking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OfferAddMode",
                table: "TempOfferStockMappings",
                type: "varchar(5)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OfferAddMode",
                table: "OfferDtlStockMappings",
                type: "varchar(5)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OfferAddMode",
                table: "TempOfferStockMappings");

            migrationBuilder.DropColumn(
                name: "OfferAddMode",
                table: "OfferDtlStockMappings");
        }
    }
}
