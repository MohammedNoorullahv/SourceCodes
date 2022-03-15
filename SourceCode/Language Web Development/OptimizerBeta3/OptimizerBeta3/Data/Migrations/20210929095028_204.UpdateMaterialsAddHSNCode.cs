using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _204UpdateMaterialsAddHSNCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_materials_FKHSNCode",
                table: "materials",
                column: "FKHSNCode");

            migrationBuilder.AddForeignKey(
                name: "FK_materials_HSNCodeMasters_FKHSNCode",
                table: "materials",
                column: "FKHSNCode",
                principalTable: "HSNCodeMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_materials_HSNCodeMasters_FKHSNCode",
                table: "materials");

            migrationBuilder.DropIndex(
                name: "IX_materials_FKHSNCode",
                table: "materials");
        }
    }
}
