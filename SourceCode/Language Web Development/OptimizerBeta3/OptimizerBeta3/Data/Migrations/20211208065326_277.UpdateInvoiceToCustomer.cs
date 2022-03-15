using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _277UpdateInvoiceToCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceToPersons_lookUpMasters_FKCurrency",
                table: "InvoiceToPersons");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceToPersons_lookUpMasters_FKDepartment",
                table: "InvoiceToPersons");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceToPersons_lookUpMasters_FKModeofTransport",
                table: "InvoiceToPersons");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceToPersons_FKCurrency",
                table: "InvoiceToPersons");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceToPersons_FKDepartment",
                table: "InvoiceToPersons");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceToPersons_FKModeofTransport",
                table: "InvoiceToPersons");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "InvoiceToPersons");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "InvoiceToPersons");

            migrationBuilder.DropColumn(
                name: "DispatchedOn",
                table: "InvoiceToPersons");

            migrationBuilder.DropColumn(
                name: "FKCurrency",
                table: "InvoiceToPersons");

            migrationBuilder.DropColumn(
                name: "FKDepartment",
                table: "InvoiceToPersons");

            migrationBuilder.DropColumn(
                name: "FKModeofTransport",
                table: "InvoiceToPersons");

            migrationBuilder.DropColumn(
                name: "IsDispatched",
                table: "InvoiceToPersons");

            migrationBuilder.DropColumn(
                name: "ModeofTransport",
                table: "InvoiceToPersons");

            migrationBuilder.RenameColumn(
                name: "ValueinINR",
                table: "InvoiceToPersons",
                newName: "ItemDtlTaxableValue");

            migrationBuilder.RenameColumn(
                name: "OtherExpensesPlus",
                table: "InvoiceToPersons",
                newName: "ItemDtlNettValue");

            migrationBuilder.RenameColumn(
                name: "OtherExpensesMinus",
                table: "InvoiceToPersons",
                newName: "ItemDtlGrossAmount");

            migrationBuilder.RenameColumn(
                name: "NettValue",
                table: "InvoiceToPersons",
                newName: "ItemDtlGSTValue");

            migrationBuilder.RenameColumn(
                name: "InvoiceValue",
                table: "InvoiceToPersons",
                newName: "ItemDtlDiscountValue");

            migrationBuilder.RenameColumn(
                name: "GSTValues",
                table: "InvoiceToPersons",
                newName: "InvRoundOff");

            migrationBuilder.RenameColumn(
                name: "ExchangeRate",
                table: "InvoiceToPersons",
                newName: "InvOtherCharges");

            migrationBuilder.AddColumn<decimal>(
                name: "InvDiscountPercentage",
                table: "InvoiceToPersons",
                type: "Decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "InvDiscountValue",
                table: "InvoiceToPersons",
                type: "Decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "InvNettValue",
                table: "InvoiceToPersons",
                type: "Decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvDiscountPercentage",
                table: "InvoiceToPersons");

            migrationBuilder.DropColumn(
                name: "InvDiscountValue",
                table: "InvoiceToPersons");

            migrationBuilder.DropColumn(
                name: "InvNettValue",
                table: "InvoiceToPersons");

            migrationBuilder.RenameColumn(
                name: "ItemDtlTaxableValue",
                table: "InvoiceToPersons",
                newName: "ValueinINR");

            migrationBuilder.RenameColumn(
                name: "ItemDtlNettValue",
                table: "InvoiceToPersons",
                newName: "OtherExpensesPlus");

            migrationBuilder.RenameColumn(
                name: "ItemDtlGrossAmount",
                table: "InvoiceToPersons",
                newName: "OtherExpensesMinus");

            migrationBuilder.RenameColumn(
                name: "ItemDtlGSTValue",
                table: "InvoiceToPersons",
                newName: "NettValue");

            migrationBuilder.RenameColumn(
                name: "ItemDtlDiscountValue",
                table: "InvoiceToPersons",
                newName: "InvoiceValue");

            migrationBuilder.RenameColumn(
                name: "InvRoundOff",
                table: "InvoiceToPersons",
                newName: "GSTValues");

            migrationBuilder.RenameColumn(
                name: "InvOtherCharges",
                table: "InvoiceToPersons",
                newName: "ExchangeRate");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "InvoiceToPersons",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "InvoiceToPersons",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DispatchedOn",
                table: "InvoiceToPersons",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FKCurrency",
                table: "InvoiceToPersons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKDepartment",
                table: "InvoiceToPersons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKModeofTransport",
                table: "InvoiceToPersons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDispatched",
                table: "InvoiceToPersons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModeofTransport",
                table: "InvoiceToPersons",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceToPersons_FKCurrency",
                table: "InvoiceToPersons",
                column: "FKCurrency");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceToPersons_FKDepartment",
                table: "InvoiceToPersons",
                column: "FKDepartment");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceToPersons_FKModeofTransport",
                table: "InvoiceToPersons",
                column: "FKModeofTransport");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceToPersons_lookUpMasters_FKCurrency",
                table: "InvoiceToPersons",
                column: "FKCurrency",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceToPersons_lookUpMasters_FKDepartment",
                table: "InvoiceToPersons",
                column: "FKDepartment",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceToPersons_lookUpMasters_FKModeofTransport",
                table: "InvoiceToPersons",
                column: "FKModeofTransport",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
