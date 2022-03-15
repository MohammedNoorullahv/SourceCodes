using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _032UpdateArticleGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Code",
                table: "articleGroups",
                newName: "StockNo");

            migrationBuilder.AddColumn<bool>(
                name: "IsSeasonSpecific",
                table: "articleGroups",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSeasonSpecific",
                table: "articleGroups");

            migrationBuilder.RenameColumn(
                name: "StockNo",
                table: "articleGroups",
                newName: "Code");
        }
    }
}
