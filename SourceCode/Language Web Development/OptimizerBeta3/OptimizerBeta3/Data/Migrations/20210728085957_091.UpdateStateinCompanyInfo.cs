using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _091UpdateStateinCompanyInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_companyInfos_lookUpMasters_FKState",
                table: "companyInfos");

            migrationBuilder.DropIndex(
                name: "IX_companyInfos_FKState",
                table: "companyInfos");

            migrationBuilder.AddColumn<int>(
                name: "StateMasterId",
                table: "companyInfos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_companyInfos_StateMasterId",
                table: "companyInfos",
                column: "StateMasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_companyInfos_StateMasters_StateMasterId",
                table: "companyInfos",
                column: "StateMasterId",
                principalTable: "StateMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_companyInfos_StateMasters_StateMasterId",
                table: "companyInfos");

            migrationBuilder.DropIndex(
                name: "IX_companyInfos_StateMasterId",
                table: "companyInfos");

            migrationBuilder.DropColumn(
                name: "StateMasterId",
                table: "companyInfos");

            migrationBuilder.CreateIndex(
                name: "IX_companyInfos_FKState",
                table: "companyInfos",
                column: "FKState");

            migrationBuilder.AddForeignKey(
                name: "FK_companyInfos_lookUpMasters_FKState",
                table: "companyInfos",
                column: "FKState",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
