using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _059UpdateArticleDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LiningColour",
                table: "articleDetails",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SocksColour",
                table: "articleDetails",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoleColour",
                table: "articleDetails",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LiningColour",
                table: "articleDetails");

            migrationBuilder.DropColumn(
                name: "SocksColour",
                table: "articleDetails");

            migrationBuilder.DropColumn(
                name: "SoleColour",
                table: "articleDetails");
        }
    }
}
