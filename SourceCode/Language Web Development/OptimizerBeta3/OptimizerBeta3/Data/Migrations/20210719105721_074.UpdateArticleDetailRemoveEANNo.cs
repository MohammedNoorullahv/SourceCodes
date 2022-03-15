using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _074UpdateArticleDetailRemoveEANNo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EANNo",
                table: "articleDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EANNo",
                table: "articleDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
