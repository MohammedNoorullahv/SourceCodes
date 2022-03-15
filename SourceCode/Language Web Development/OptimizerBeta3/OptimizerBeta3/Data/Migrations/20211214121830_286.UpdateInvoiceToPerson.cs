using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _286UpdateInvoiceToPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_InvoiceToPersons_FKSalesPerson",
                table: "InvoiceToPersons",
                column: "FKSalesPerson");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceToPersons_Employees_FKSalesPerson",
                table: "InvoiceToPersons",
                column: "FKSalesPerson",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceToPersons_Employees_FKSalesPerson",
                table: "InvoiceToPersons");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceToPersons_FKSalesPerson",
                table: "InvoiceToPersons");
        }
    }
}
