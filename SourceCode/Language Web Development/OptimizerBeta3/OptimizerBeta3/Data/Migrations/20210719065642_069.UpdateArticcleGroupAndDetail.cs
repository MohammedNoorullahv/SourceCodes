using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _069UpdateArticcleGroupAndDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_articleDetails_FKEntryType",
                table: "articleDetails",
                column: "FKEntryType");

            migrationBuilder.AddForeignKey(
                name: "FK_articleDetails_lookUpMasters_FKEntryType",
                table: "articleDetails",
                column: "FKEntryType",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_articleDetails_lookUpMasters_FKEntryType",
                table: "articleDetails");

            migrationBuilder.DropIndex(
                name: "IX_articleDetails_FKEntryType",
                table: "articleDetails");
        }
    }
}
