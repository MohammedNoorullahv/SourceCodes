using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _147UpdateMaterialsAddColour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_materials_lookUpMasters_FKIIUom",
                table: "materials");

            migrationBuilder.DropIndex(
                name: "IX_materials_FKIIUom",
                table: "materials");

            migrationBuilder.RenameColumn(
                name: "FKIIUom",
                table: "materials",
                newName: "FKColour");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FKColour",
                table: "materials",
                newName: "FKIIUom");

            migrationBuilder.CreateIndex(
                name: "IX_materials_FKIIUom",
                table: "materials",
                column: "FKIIUom");

            migrationBuilder.AddForeignKey(
                name: "FK_materials_lookUpMasters_FKIIUom",
                table: "materials",
                column: "FKIIUom",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
