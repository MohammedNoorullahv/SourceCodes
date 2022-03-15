using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _033UpdateArticleDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "articleDetails");

            migrationBuilder.AddColumn<string>(
                name: "ColorDescription",
                table: "articleDetails",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StockNo",
                table: "articleDetails",
                type: "varchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Variant",
                table: "articleDetails",
                type: "varchar(2)",
                maxLength: 2,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorDescription",
                table: "articleDetails");

            migrationBuilder.DropColumn(
                name: "StockNo",
                table: "articleDetails");

            migrationBuilder.DropColumn(
                name: "Variant",
                table: "articleDetails");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "articleDetails",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }
    }
}
