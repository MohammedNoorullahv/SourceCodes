using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _275UpdateEstimate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FKOffer",
                table: "Estimates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OfferPercentage",
                table: "Estimates",
                type: "Decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OfferValue",
                table: "Estimates",
                type: "Decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RoundOff",
                table: "Estimates",
                type: "Decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FKOffer",
                table: "Estimates");

            migrationBuilder.DropColumn(
                name: "OfferPercentage",
                table: "Estimates");

            migrationBuilder.DropColumn(
                name: "OfferValue",
                table: "Estimates");

            migrationBuilder.DropColumn(
                name: "RoundOff",
                table: "Estimates");
        }
    }
}
