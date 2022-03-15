using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _305UpdateArticleGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssortmentGroup",
                table: "articleGroups",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FKAssortmentGroup",
                table: "articleGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssortmentGroup",
                table: "articleGroups");

            migrationBuilder.DropColumn(
                name: "FKAssortmentGroup",
                table: "articleGroups");
        }
    }
}
