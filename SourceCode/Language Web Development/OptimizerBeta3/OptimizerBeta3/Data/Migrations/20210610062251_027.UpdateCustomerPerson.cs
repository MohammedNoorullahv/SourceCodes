using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _027UpdateCustomerPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FKCustomerOf",
                table: "customerPerson",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_customerPerson_FKCustomerOf",
                table: "customerPerson",
                column: "FKCustomerOf");

            migrationBuilder.AddForeignKey(
                name: "FK_customerPerson_lookUpMasters_FKCustomerOf",
                table: "customerPerson",
                column: "FKCustomerOf",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customerPerson_lookUpMasters_FKCustomerOf",
                table: "customerPerson");

            migrationBuilder.DropIndex(
                name: "IX_customerPerson_FKCustomerOf",
                table: "customerPerson");

            migrationBuilder.DropColumn(
                name: "FKCustomerOf",
                table: "customerPerson");
        }
    }
}
