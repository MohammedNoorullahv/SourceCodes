using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _245UpdateInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_lookUpMasters_FKDepartment",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_FKDepartment",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "FKDepartment",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "Department",
                table: "Invoices",
                newName: "OrderRefNo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderRefNo",
                table: "Invoices",
                newName: "Department");

            migrationBuilder.AddColumn<int>(
                name: "FKDepartment",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_FKDepartment",
                table: "Invoices",
                column: "FKDepartment");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_lookUpMasters_FKDepartment",
                table: "Invoices",
                column: "FKDepartment",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
