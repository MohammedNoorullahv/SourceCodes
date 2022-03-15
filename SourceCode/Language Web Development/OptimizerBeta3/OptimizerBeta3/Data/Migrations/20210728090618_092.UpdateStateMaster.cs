using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _092UpdateStateMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_companyInfos_StateMasters_StateMasterId",
                table: "companyInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_customerPerson_lookUpMasters_FKState",
                table: "customerPerson");

            migrationBuilder.DropForeignKey(
                name: "FK_partyInfoDtls_lookUpMasters_FKState",
                table: "partyInfoDtls");

            migrationBuilder.DropForeignKey(
                name: "FK_partyInfos_lookUpMasters_FKState",
                table: "partyInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_unitMasters_lookUpMasters_FKState",
                table: "unitMasters");

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
                name: "FK_companyInfos_StateMasters_FKState",
                table: "companyInfos",
                column: "FKState",
                principalTable: "StateMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_customerPerson_StateMasters_FKState",
                table: "customerPerson",
                column: "FKState",
                principalTable: "StateMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_partyInfoDtls_StateMasters_FKState",
                table: "partyInfoDtls",
                column: "FKState",
                principalTable: "StateMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_partyInfos_StateMasters_FKState",
                table: "partyInfos",
                column: "FKState",
                principalTable: "StateMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_unitMasters_StateMasters_FKState",
                table: "unitMasters",
                column: "FKState",
                principalTable: "StateMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_companyInfos_StateMasters_FKState",
                table: "companyInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_customerPerson_StateMasters_FKState",
                table: "customerPerson");

            migrationBuilder.DropForeignKey(
                name: "FK_partyInfoDtls_StateMasters_FKState",
                table: "partyInfoDtls");

            migrationBuilder.DropForeignKey(
                name: "FK_partyInfos_StateMasters_FKState",
                table: "partyInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_unitMasters_StateMasters_FKState",
                table: "unitMasters");

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

            migrationBuilder.AddForeignKey(
                name: "FK_customerPerson_lookUpMasters_FKState",
                table: "customerPerson",
                column: "FKState",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_partyInfoDtls_lookUpMasters_FKState",
                table: "partyInfoDtls",
                column: "FKState",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_partyInfos_lookUpMasters_FKState",
                table: "partyInfos",
                column: "FKState",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_unitMasters_lookUpMasters_FKState",
                table: "unitMasters",
                column: "FKState",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
