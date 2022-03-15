using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _221UpdateStockTransferRemoveDepartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockTransfers_lookUpMasters_FKFromDepartment",
                table: "StockTransfers");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransfers_lookUpMasters_FKToDepartment",
                table: "StockTransfers");

            migrationBuilder.DropIndex(
                name: "IX_StockTransfers_FKFromDepartment",
                table: "StockTransfers");

            migrationBuilder.DropIndex(
                name: "IX_StockTransfers_FKToDepartment",
                table: "StockTransfers");

            migrationBuilder.CreateTable(
                name: "TempTransferDtlEANCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IPAddress = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    TransferNo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    EANCode = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true),
                    Quantity = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    IsMatching = table.Column<bool>(type: "bit", nullable: false),
                    Reason = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempTransferDtlEANCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempTransferDtls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKTransferNo = table.Column<int>(type: "int", nullable: false),
                    TransferNo = table.Column<string>(type: "varchar(20)", nullable: true),
                    TransferDt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FKMaterial = table.Column<int>(type: "int", nullable: false),
                    FKArticle = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "Varchar(100)", nullable: true),
                    Colour = table.Column<string>(type: "Varchar(20)", nullable: true),
                    Size = table.Column<string>(type: "Varchar(5)", nullable: true),
                    HSNCode = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    FKUOM = table.Column<int>(type: "int", nullable: false),
                    IIQuantity = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    FKIIUom = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    Value = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    ValueinINR = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    DiscountValue = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    GrossValue = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    SGSTPercentage = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    SGSTValue = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    CGSTPercentage = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    CGSTValue = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    IGSTPercentage = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    IGSTValue = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    GSTTotalValue = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    OthersValuePlus = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    OthersValueMinus = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    ItemNettValue = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    EANCode = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: true),
                    ReadyforImport = table.Column<bool>(type: "bit", nullable: false),
                    Reason = table.Column<string>(type: "Varchar(20)", nullable: true),
                    OrderReferenceNo = table.Column<string>(type: "Varchar(50)", nullable: true),
                    FKUnit = table.Column<int>(type: "int", nullable: false),
                    FKLocation = table.Column<int>(type: "int", nullable: false),
                    FKCurrency = table.Column<int>(type: "int", nullable: false),
                    FKSource = table.Column<int>(type: "int", nullable: false),
                    StockNo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    FKCustomer = table.Column<int>(type: "int", nullable: false),
                    MaterialorFinishedProduct = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true),
                    IPAddress = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempTransferDtls", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TempTransferDtlEANCodes");

            migrationBuilder.DropTable(
                name: "TempTransferDtls");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransfers_FKFromDepartment",
                table: "StockTransfers",
                column: "FKFromDepartment");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransfers_FKToDepartment",
                table: "StockTransfers",
                column: "FKToDepartment");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransfers_lookUpMasters_FKFromDepartment",
                table: "StockTransfers",
                column: "FKFromDepartment",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransfers_lookUpMasters_FKToDepartment",
                table: "StockTransfers",
                column: "FKToDepartment",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
