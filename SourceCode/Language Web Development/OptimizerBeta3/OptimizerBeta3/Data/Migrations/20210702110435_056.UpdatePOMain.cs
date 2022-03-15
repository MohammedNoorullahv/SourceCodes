using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _056UpdatePOMain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Article",
                table: "purchaseOrderDetails",
                newName: "ArticleDescription");

            migrationBuilder.AddColumn<string>(
                name: "Article",
                table: "purchaseOrderMains",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Article",
                table: "purchaseOrderMains");

            migrationBuilder.RenameColumn(
                name: "ArticleDescription",
                table: "purchaseOrderDetails",
                newName: "Article");
        }
    }
}
