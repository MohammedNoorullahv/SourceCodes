using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _148UpdateMaterialsAddColour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_materials_FKColour",
                table: "materials",
                column: "FKColour");

            migrationBuilder.AddForeignKey(
                name: "FK_materials_lookUpMasters_FKColour",
                table: "materials",
                column: "FKColour",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_materials_lookUpMasters_FKColour",
                table: "materials");

            migrationBuilder.DropIndex(
                name: "IX_materials_FKColour",
                table: "materials");
        }
    }
}
