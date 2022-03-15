using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _070UpdateArticcleGroupAndDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_articleDetails_FKCategory",
                table: "articleDetails",
                column: "FKCategory");

            migrationBuilder.CreateIndex(
                name: "IX_articleDetails_FKFeatures",
                table: "articleDetails",
                column: "FKFeatures");

            migrationBuilder.AddForeignKey(
                name: "FK_articleDetails_lookUpMasters_FKCategory",
                table: "articleDetails",
                column: "FKCategory",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_articleDetails_lookUpMasters_FKFeatures",
                table: "articleDetails",
                column: "FKFeatures",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_articleDetails_lookUpMasters_FKCategory",
                table: "articleDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_articleDetails_lookUpMasters_FKFeatures",
                table: "articleDetails");

            migrationBuilder.DropIndex(
                name: "IX_articleDetails_FKCategory",
                table: "articleDetails");

            migrationBuilder.DropIndex(
                name: "IX_articleDetails_FKFeatures",
                table: "articleDetails");
        }
    }
}
