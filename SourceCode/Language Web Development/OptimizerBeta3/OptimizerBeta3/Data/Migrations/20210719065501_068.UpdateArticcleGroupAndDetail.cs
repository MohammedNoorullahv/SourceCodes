using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _068UpdateArticcleGroupAndDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_articleGroups_lookUpMasters_FKArticleType",
                table: "articleGroups");

            migrationBuilder.DropIndex(
                name: "IX_articleGroups_FKArticleType",
                table: "articleGroups");

            migrationBuilder.DropColumn(
                name: "ArticleType",
                table: "articleGroups");

            migrationBuilder.DropColumn(
                name: "FKArticleType",
                table: "articleGroups");

            migrationBuilder.DropColumn(
                name: "StockNo",
                table: "articleGroups");

            migrationBuilder.AddColumn<string>(
                name: "ArticleNo",
                table: "articleDetails",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "articleDetails",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EntryType",
                table: "articleDetails",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FKCategory",
                table: "articleDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKEntryType",
                table: "articleDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKFeatures",
                table: "articleDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Features",
                table: "articleDetails",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArticleNo",
                table: "articleDetails");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "articleDetails");

            migrationBuilder.DropColumn(
                name: "EntryType",
                table: "articleDetails");

            migrationBuilder.DropColumn(
                name: "FKCategory",
                table: "articleDetails");

            migrationBuilder.DropColumn(
                name: "FKEntryType",
                table: "articleDetails");

            migrationBuilder.DropColumn(
                name: "FKFeatures",
                table: "articleDetails");

            migrationBuilder.DropColumn(
                name: "Features",
                table: "articleDetails");

            migrationBuilder.AddColumn<string>(
                name: "ArticleType",
                table: "articleGroups",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FKArticleType",
                table: "articleGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StockNo",
                table: "articleGroups",
                type: "varchar(6)",
                maxLength: 6,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_articleGroups_FKArticleType",
                table: "articleGroups",
                column: "FKArticleType");

            migrationBuilder.AddForeignKey(
                name: "FK_articleGroups_lookUpMasters_FKArticleType",
                table: "articleGroups",
                column: "FKArticleType",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
