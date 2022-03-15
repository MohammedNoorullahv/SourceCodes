using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _143AddInvoiceToPersonDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoiceToPersonDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKInvoiceNo = table.Column<int>(type: "int", nullable: false),
                    InvoiceNo = table.Column<string>(type: "varchar(20)", nullable: true),
                    InvoiceDt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FKMaterial = table.Column<int>(type: "int", nullable: false),
                    FKArticle = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "Varchar(50)", nullable: true),
                    Colour = table.Column<string>(type: "Varchar(20)", nullable: true),
                    Size = table.Column<string>(type: "Varchar(5)", nullable: true),
                    HSNCode = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    FKUOM = table.Column<int>(type: "int", nullable: false),
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
                    StockNo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    FKCustomer = table.Column<int>(type: "int", nullable: false),
                    MaterialorFinishedProduct = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true),
                    IsEntryCompleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EnteredSystemId = table.Column<string>(type: "varchar(30)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteBy = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceToPersonDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceToPersonDetails_InvoiceToPersons_FKInvoiceNo",
                        column: x => x.FKInvoiceNo,
                        principalTable: "InvoiceToPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_InvoiceToPersonDetails_lookUpMasters_FKUOM",
                        column: x => x.FKUOM,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceToPersonDetails_FKInvoiceNo",
                table: "InvoiceToPersonDetails",
                column: "FKInvoiceNo");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceToPersonDetails_FKUOM",
                table: "InvoiceToPersonDetails",
                column: "FKUOM");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceToPersonDetails");
        }
    }
}
