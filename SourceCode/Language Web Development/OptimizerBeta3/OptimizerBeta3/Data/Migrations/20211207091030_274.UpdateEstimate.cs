using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _274UpdateEstimate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ItemsDiscountValue",
                table: "Estimates",
                type: "Decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ItemsGrossValue",
                table: "Estimates",
                type: "Decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ItemsValue",
                table: "Estimates",
                type: "Decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemsDiscountValue",
                table: "Estimates");

            migrationBuilder.DropColumn(
                name: "ItemsGrossValue",
                table: "Estimates");

            migrationBuilder.DropColumn(
                name: "ItemsValue",
                table: "Estimates");
        }
    }
}
