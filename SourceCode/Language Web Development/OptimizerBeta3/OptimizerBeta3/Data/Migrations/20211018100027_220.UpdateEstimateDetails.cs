using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _220UpdateEstimateDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EANCode",
                table: "EstimateDetails",
                type: "varchar(13)",
                maxLength: 13,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StockNo",
                table: "EstimateDetails",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EANCode",
                table: "EstimateDetails");

            migrationBuilder.DropColumn(
                name: "StockNo",
                table: "EstimateDetails");
        }
    }
}
