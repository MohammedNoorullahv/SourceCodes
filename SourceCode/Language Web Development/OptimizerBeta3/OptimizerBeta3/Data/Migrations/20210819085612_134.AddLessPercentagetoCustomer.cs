using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _134AddLessPercentagetoCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "LessPercentageonMRP",
                table: "partyInfos",
                type: "Decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LessPercentageonMRP",
                table: "partyInfos");
        }
    }
}
