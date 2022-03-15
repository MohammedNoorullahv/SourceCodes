using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _107UpdateInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_partyInfos_FKBillTo",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_partyInfos_FKNotifyTo",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_FKBillTo",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_FKNotifyTo",
                table: "Invoices");

            migrationBuilder.AddColumn<string>(
                name: "BillToCustomerName",
                table: "Invoices",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Invoices",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Invoices",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryToCustomerName",
                table: "Invoices",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "Invoices",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModeofTransport",
                table: "Invoices",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NotifyToCustomerName",
                table: "Invoices",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentTerms",
                table: "Invoices",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Season",
                table: "Invoices",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeofInvoice",
                table: "Invoices",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitName",
                table: "Invoices",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillToCustomerName",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "DeliveryToCustomerName",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ModeofTransport",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "NotifyToCustomerName",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "PaymentTerms",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Season",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "TypeofInvoice",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "UnitName",
                table: "Invoices");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_FKBillTo",
                table: "Invoices",
                column: "FKBillTo");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_FKNotifyTo",
                table: "Invoices",
                column: "FKNotifyTo");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_partyInfos_FKBillTo",
                table: "Invoices",
                column: "FKBillTo",
                principalTable: "partyInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_partyInfos_FKNotifyTo",
                table: "Invoices",
                column: "FKNotifyTo",
                principalTable: "partyInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
