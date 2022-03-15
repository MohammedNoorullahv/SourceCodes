using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _293UpdateEstimate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ItemsDiscountValue",
                table: "Estimates",
                newName: "ItemsDiscountValuePWise");

            migrationBuilder.AddColumn<decimal>(
                name: "ItemsDiscountValueBWise",
                table: "Estimates",
                type: "Decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemsDiscountValueBWise",
                table: "Estimates");

            migrationBuilder.RenameColumn(
                name: "ItemsDiscountValuePWise",
                table: "Estimates",
                newName: "ItemsDiscountValue");
        }
    }
}
