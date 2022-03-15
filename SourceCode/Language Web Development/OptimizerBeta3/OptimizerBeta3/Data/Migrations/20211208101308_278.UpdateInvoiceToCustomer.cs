using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _278UpdateInvoiceToCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceToPersons_lookUpMasters_FKPaymentTerms",
                table: "InvoiceToPersons");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceToPersons_FKPaymentTerms",
                table: "InvoiceToPersons");

            migrationBuilder.DropColumn(
                name: "PaymentTerms",
                table: "InvoiceToPersons");

            migrationBuilder.RenameColumn(
                name: "FKPaymentTerms",
                table: "InvoiceToPersons",
                newName: "CardNo");

            migrationBuilder.AddColumn<decimal>(
                name: "BalCashToPay",
                table: "InvoiceToPersons",
                type: "Decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BillValue",
                table: "InvoiceToPersons",
                type: "Decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "CardInfo",
                table: "InvoiceToPersons",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CardValue",
                table: "InvoiceToPersons",
                type: "Decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CashReceived",
                table: "InvoiceToPersons",
                type: "Decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "UPIInfo",
                table: "InvoiceToPersons",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UPITranNo",
                table: "InvoiceToPersons",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "UPIValue",
                table: "InvoiceToPersons",
                type: "Decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BalCashToPay",
                table: "InvoiceToPersons");

            migrationBuilder.DropColumn(
                name: "BillValue",
                table: "InvoiceToPersons");

            migrationBuilder.DropColumn(
                name: "CardInfo",
                table: "InvoiceToPersons");

            migrationBuilder.DropColumn(
                name: "CardValue",
                table: "InvoiceToPersons");

            migrationBuilder.DropColumn(
                name: "CashReceived",
                table: "InvoiceToPersons");

            migrationBuilder.DropColumn(
                name: "UPIInfo",
                table: "InvoiceToPersons");

            migrationBuilder.DropColumn(
                name: "UPITranNo",
                table: "InvoiceToPersons");

            migrationBuilder.DropColumn(
                name: "UPIValue",
                table: "InvoiceToPersons");

            migrationBuilder.RenameColumn(
                name: "CardNo",
                table: "InvoiceToPersons",
                newName: "FKPaymentTerms");

            migrationBuilder.AddColumn<string>(
                name: "PaymentTerms",
                table: "InvoiceToPersons",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceToPersons_FKPaymentTerms",
                table: "InvoiceToPersons",
                column: "FKPaymentTerms");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceToPersons_lookUpMasters_FKPaymentTerms",
                table: "InvoiceToPersons",
                column: "FKPaymentTerms",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
