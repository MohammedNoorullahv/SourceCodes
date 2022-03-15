using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _140UpdateArticleGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_articleGroups_FKCategory",
                table: "articleGroups",
                column: "FKCategory");

            migrationBuilder.AddForeignKey(
                name: "FK_articleGroups_lookUpMasters_FKCategory",
                table: "articleGroups",
                column: "FKCategory",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_articleGroups_lookUpMasters_FKCategory",
                table: "articleGroups");

            migrationBuilder.DropIndex(
                name: "IX_articleGroups_FKCategory",
                table: "articleGroups");
        }
    }
}
