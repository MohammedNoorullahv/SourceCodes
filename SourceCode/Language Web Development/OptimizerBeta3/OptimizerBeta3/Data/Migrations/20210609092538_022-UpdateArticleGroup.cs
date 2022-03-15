using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _022UpdateArticleGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_articleGroups_lookUpMasters_FKArea",
                table: "articleGroups");

            migrationBuilder.DropIndex(
                name: "IX_articleGroups_FKArea",
                table: "articleGroups");

            migrationBuilder.DropColumn(
                name: "FKArea",
                table: "articleGroups");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FKArea",
                table: "articleGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_articleGroups_FKArea",
                table: "articleGroups",
                column: "FKArea");

            migrationBuilder.AddForeignKey(
                name: "FK_articleGroups_lookUpMasters_FKArea",
                table: "articleGroups",
                column: "FKArea",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
