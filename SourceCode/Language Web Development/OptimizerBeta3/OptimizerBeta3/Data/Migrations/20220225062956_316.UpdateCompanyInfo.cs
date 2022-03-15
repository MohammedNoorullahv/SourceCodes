using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _316UpdateCompanyInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_companyInfos_FKAreaMaster",
                table: "companyInfos",
                column: "FKAreaMaster");

            migrationBuilder.AddForeignKey(
                name: "FK_companyInfos_lookUpMasters_FKAreaMaster",
                table: "companyInfos",
                column: "FKAreaMaster",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_companyInfos_lookUpMasters_FKAreaMaster",
                table: "companyInfos");

            migrationBuilder.DropIndex(
                name: "IX_companyInfos_FKAreaMaster",
                table: "companyInfos");
        }
    }
}
