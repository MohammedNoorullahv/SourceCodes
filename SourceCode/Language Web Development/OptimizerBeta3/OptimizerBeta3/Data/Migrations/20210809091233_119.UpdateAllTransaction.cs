using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _119UpdateAllTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllTransactions_lookUpMasters_FKFromStage",
                table: "AllTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_AllTransactions_lookUpMasters_FKToStage",
                table: "AllTransactions");

            migrationBuilder.DropIndex(
                name: "IX_AllTransactions_FKFromStage",
                table: "AllTransactions");

            migrationBuilder.DropIndex(
                name: "IX_AllTransactions_FKToStage",
                table: "AllTransactions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AllTransactions_FKFromStage",
                table: "AllTransactions",
                column: "FKFromStage");

            migrationBuilder.CreateIndex(
                name: "IX_AllTransactions_FKToStage",
                table: "AllTransactions",
                column: "FKToStage");

            migrationBuilder.AddForeignKey(
                name: "FK_AllTransactions_lookUpMasters_FKFromStage",
                table: "AllTransactions",
                column: "FKFromStage",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AllTransactions_lookUpMasters_FKToStage",
                table: "AllTransactions",
                column: "FKToStage",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
