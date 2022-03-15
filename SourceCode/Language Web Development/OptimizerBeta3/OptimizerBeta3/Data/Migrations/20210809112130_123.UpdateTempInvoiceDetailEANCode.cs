using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _123UpdateTempInvoiceDetailEANCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InwardNo",
                table: "TempInvoiceDtlEANCodes",
                newName: "InvoiceNo");

            migrationBuilder.AddColumn<int>(
                name: "FKCustomer",
                table: "TempInvoiceDtls",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "InvoiceDt",
                table: "TempInvoiceDtls",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "InvoiceNo",
                table: "TempInvoiceDtls",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BalQty",
                table: "stockWithArticles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SoldQty",
                table: "stockWithArticles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FKCustomer",
                table: "TempInvoiceDtls");

            migrationBuilder.DropColumn(
                name: "InvoiceDt",
                table: "TempInvoiceDtls");

            migrationBuilder.DropColumn(
                name: "InvoiceNo",
                table: "TempInvoiceDtls");

            migrationBuilder.DropColumn(
                name: "BalQty",
                table: "stockWithArticles");

            migrationBuilder.DropColumn(
                name: "SoldQty",
                table: "stockWithArticles");

            migrationBuilder.RenameColumn(
                name: "InvoiceNo",
                table: "TempInvoiceDtlEANCodes",
                newName: "InwardNo");
        }
    }
}
