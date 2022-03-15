using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _169UpdateTempInwardDtls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercentageforSales",
                table: "TempInwardDtls",
                type: "Decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "FKCategory",
                table: "TempInwardDtls",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKHSNCode",
                table: "TempInwardDtls",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKOffer",
                table: "TempInwardDtls",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastTranDate",
                table: "TempInwardDtls",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "MRP",
                table: "TempInwardDtls",
                type: "Decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "OfferType",
                table: "TempInwardDtls",
                type: "varchar(1)",
                maxLength: 1,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPercentageforSales",
                table: "TempInwardDtls");

            migrationBuilder.DropColumn(
                name: "FKCategory",
                table: "TempInwardDtls");

            migrationBuilder.DropColumn(
                name: "FKHSNCode",
                table: "TempInwardDtls");

            migrationBuilder.DropColumn(
                name: "FKOffer",
                table: "TempInwardDtls");

            migrationBuilder.DropColumn(
                name: "LastTranDate",
                table: "TempInwardDtls");

            migrationBuilder.DropColumn(
                name: "MRP",
                table: "TempInwardDtls");

            migrationBuilder.DropColumn(
                name: "OfferType",
                table: "TempInwardDtls");
        }
    }
}
