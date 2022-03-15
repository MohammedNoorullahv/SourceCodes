using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _296UpdateCustomerPersonInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_customerPerson_FKGender",
                table: "customerPerson",
                column: "FKGender");

            migrationBuilder.CreateIndex(
                name: "IX_customerPerson_FKMaritalStatus",
                table: "customerPerson",
                column: "FKMaritalStatus");

            migrationBuilder.AddForeignKey(
                name: "FK_customerPerson_lookUpMasters_FKGender",
                table: "customerPerson",
                column: "FKGender",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_customerPerson_lookUpMasters_FKMaritalStatus",
                table: "customerPerson",
                column: "FKMaritalStatus",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customerPerson_lookUpMasters_FKGender",
                table: "customerPerson");

            migrationBuilder.DropForeignKey(
                name: "FK_customerPerson_lookUpMasters_FKMaritalStatus",
                table: "customerPerson");

            migrationBuilder.DropIndex(
                name: "IX_customerPerson_FKGender",
                table: "customerPerson");

            migrationBuilder.DropIndex(
                name: "IX_customerPerson_FKMaritalStatus",
                table: "customerPerson");
        }
    }
}
