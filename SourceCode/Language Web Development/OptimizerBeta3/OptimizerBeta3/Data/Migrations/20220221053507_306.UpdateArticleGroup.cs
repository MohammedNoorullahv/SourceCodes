using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _306UpdateArticleGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_articleGroups_FKAssortmentGroup",
                table: "articleGroups",
                column: "FKAssortmentGroup");

            migrationBuilder.AddForeignKey(
                name: "FK_articleGroups_lookUpMasters_FKAssortmentGroup",
                table: "articleGroups",
                column: "FKAssortmentGroup",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_articleGroups_lookUpMasters_FKAssortmentGroup",
                table: "articleGroups");

            migrationBuilder.DropIndex(
                name: "IX_articleGroups_FKAssortmentGroup",
                table: "articleGroups");
        }
    }
}
