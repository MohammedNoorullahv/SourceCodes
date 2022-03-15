using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _158UpdateArticleGroupAddFKProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_articleGroups_FKProduct",
                table: "articleGroups",
                column: "FKProduct");

            migrationBuilder.AddForeignKey(
                name: "FK_articleGroups_lookUpMasters_FKProduct",
                table: "articleGroups",
                column: "FKProduct",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_articleGroups_lookUpMasters_FKProduct",
                table: "articleGroups");

            migrationBuilder.DropIndex(
                name: "IX_articleGroups_FKProduct",
                table: "articleGroups");
        }
    }
}
