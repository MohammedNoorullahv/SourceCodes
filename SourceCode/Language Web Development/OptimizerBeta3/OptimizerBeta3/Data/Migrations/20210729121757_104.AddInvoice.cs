using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _104AddInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialorFinishedProduct = table.Column<string>(type: "varchar(1)", nullable: true),
                    FKTypeOfInvoice = table.Column<int>(type: "int", nullable: false),
                    FKSeason = table.Column<int>(type: "int", nullable: false),
                    FKUnit = table.Column<int>(type: "int", nullable: false),
                    FKParty = table.Column<int>(type: "int", nullable: false),
                    FKDepartment = table.Column<int>(type: "int", nullable: false),
                    InvoiceNo = table.Column<string>(type: "varchar(20)", nullable: true),
                    InvoiceDt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FKBillTo = table.Column<int>(type: "int", nullable: false),
                    FKNotifyTo = table.Column<int>(type: "int", nullable: false),
                    FKDeliveryTo = table.Column<int>(type: "int", nullable: false),
                    FKPaymentTerms = table.Column<int>(type: "int", nullable: false),
                    FKCurrency = table.Column<int>(type: "int", nullable: false),
                    FKModeofTransport = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "Varchar(250)", nullable: true),
                    Quantity = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    ExchangeRate = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    InvoiceValue = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    ValueinINR = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    GSTValues = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    OtherExpensesPlus = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    OtherExpensesMinus = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    NettValue = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    IsEntryCompleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EnteredSystemId = table.Column<string>(type: "varchar(30)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteBy = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_lookUpMasters_FKCurrency",
                        column: x => x.FKCurrency,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Invoices_lookUpMasters_FKDepartment",
                        column: x => x.FKDepartment,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Invoices_lookUpMasters_FKModeofTransport",
                        column: x => x.FKModeofTransport,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Invoices_lookUpMasters_FKPaymentTerms",
                        column: x => x.FKPaymentTerms,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Invoices_lookUpMasters_FKTypeOfInvoice",
                        column: x => x.FKTypeOfInvoice,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Invoices_partyInfos_FKBillTo",
                        column: x => x.FKBillTo,
                        principalTable: "partyInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Invoices_partyInfos_FKDeliveryTo",
                        column: x => x.FKDeliveryTo,
                        principalTable: "partyInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Invoices_partyInfos_FKNotifyTo",
                        column: x => x.FKNotifyTo,
                        principalTable: "partyInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Invoices_partyInfos_FKParty",
                        column: x => x.FKParty,
                        principalTable: "partyInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Invoices_seasons_FKSeason",
                        column: x => x.FKSeason,
                        principalTable: "seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Invoices_unitMasters_FKUnit",
                        column: x => x.FKUnit,
                        principalTable: "unitMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_FKBillTo",
                table: "Invoices",
                column: "FKBillTo");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_FKCurrency",
                table: "Invoices",
                column: "FKCurrency");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_FKDeliveryTo",
                table: "Invoices",
                column: "FKDeliveryTo");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_FKDepartment",
                table: "Invoices",
                column: "FKDepartment");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_FKModeofTransport",
                table: "Invoices",
                column: "FKModeofTransport");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_FKNotifyTo",
                table: "Invoices",
                column: "FKNotifyTo");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_FKParty",
                table: "Invoices",
                column: "FKParty");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_FKPaymentTerms",
                table: "Invoices",
                column: "FKPaymentTerms");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_FKSeason",
                table: "Invoices",
                column: "FKSeason");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_FKTypeOfInvoice",
                table: "Invoices",
                column: "FKTypeOfInvoice");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_FKUnit",
                table: "Invoices",
                column: "FKUnit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoices");
        }
    }
}
