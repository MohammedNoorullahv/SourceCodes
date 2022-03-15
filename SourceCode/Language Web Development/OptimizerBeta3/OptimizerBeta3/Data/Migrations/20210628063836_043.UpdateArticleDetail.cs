using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _043UpdateArticleDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FKLiningColour",
                table: "articleDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKSocksColour",
                table: "articleDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKSoleColour",
                table: "articleDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_articleDetails_FKLiningColour",
                table: "articleDetails",
                column: "FKLiningColour");

            migrationBuilder.CreateIndex(
                name: "IX_articleDetails_FKSocksColour",
                table: "articleDetails",
                column: "FKSocksColour");

            migrationBuilder.CreateIndex(
                name: "IX_articleDetails_FKSoleColour",
                table: "articleDetails",
                column: "FKSoleColour");

            migrationBuilder.AddForeignKey(
                name: "FK_articleDetails_colorMasters_FKLiningColour",
                table: "articleDetails",
                column: "FKLiningColour",
                principalTable: "colorMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_articleDetails_colorMasters_FKSocksColour",
                table: "articleDetails",
                column: "FKSocksColour",
                principalTable: "colorMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_articleDetails_colorMasters_FKSoleColour",
                table: "articleDetails",
                column: "FKSoleColour",
                principalTable: "colorMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_articleDetails_colorMasters_FKLiningColour",
                table: "articleDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_articleDetails_colorMasters_FKSocksColour",
                table: "articleDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_articleDetails_colorMasters_FKSoleColour",
                table: "articleDetails");

            migrationBuilder.DropIndex(
                name: "IX_articleDetails_FKLiningColour",
                table: "articleDetails");

            migrationBuilder.DropIndex(
                name: "IX_articleDetails_FKSocksColour",
                table: "articleDetails");

            migrationBuilder.DropIndex(
                name: "IX_articleDetails_FKSoleColour",
                table: "articleDetails");

            migrationBuilder.DropColumn(
                name: "FKLiningColour",
                table: "articleDetails");

            migrationBuilder.DropColumn(
                name: "FKSocksColour",
                table: "articleDetails");

            migrationBuilder.DropColumn(
                name: "FKSoleColour",
                table: "articleDetails");
        }
    }
}
