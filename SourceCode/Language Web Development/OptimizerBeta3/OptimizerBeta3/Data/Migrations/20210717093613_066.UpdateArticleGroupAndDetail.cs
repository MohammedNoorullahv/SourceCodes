using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _066UpdateArticleGroupAndDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArticleGroupName",
                table: "articleGroups",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArticleType",
                table: "articleGroups",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "articleGroups",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Season",
                table: "articleGroups",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SizeFor",
                table: "articleGroups",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CostPrice",
                table: "articleDetails",
                type: "Decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DealerPrice",
                table: "articleDetails",
                type: "Decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Leather",
                table: "articleDetails",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MRP",
                table: "articleDetails",
                type: "Decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProductTax",
                table: "articleDetails",
                type: "Decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArticleGroupName",
                table: "articleGroups");

            migrationBuilder.DropColumn(
                name: "ArticleType",
                table: "articleGroups");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "articleGroups");

            migrationBuilder.DropColumn(
                name: "Season",
                table: "articleGroups");

            migrationBuilder.DropColumn(
                name: "SizeFor",
                table: "articleGroups");

            migrationBuilder.DropColumn(
                name: "CostPrice",
                table: "articleDetails");

            migrationBuilder.DropColumn(
                name: "DealerPrice",
                table: "articleDetails");

            migrationBuilder.DropColumn(
                name: "Leather",
                table: "articleDetails");

            migrationBuilder.DropColumn(
                name: "MRP",
                table: "articleDetails");

            migrationBuilder.DropColumn(
                name: "ProductTax",
                table: "articleDetails");
        }
    }
}
