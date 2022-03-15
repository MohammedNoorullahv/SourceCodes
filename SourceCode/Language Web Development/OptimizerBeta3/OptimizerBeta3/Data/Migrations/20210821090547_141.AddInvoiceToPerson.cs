using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _141AddInvoiceToPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoiceToPersons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKTypeOfInvoice = table.Column<int>(type: "int", nullable: false),
                    TypeofInvoice = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    FKCategory = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    FKSeason = table.Column<int>(type: "int", nullable: false),
                    Season = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    FKUnit = table.Column<int>(type: "int", nullable: false),
                    UnitName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    FKPerson = table.Column<int>(type: "int", nullable: false),
                    PersonName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    FKDepartment = table.Column<int>(type: "int", nullable: false),
                    Department = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    InvoiceNo = table.Column<string>(type: "varchar(20)", nullable: true),
                    InvoiceDt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IncludeDeliveryTo = table.Column<bool>(type: "bit", nullable: false),
                    FKDeliveryTo = table.Column<int>(type: "int", nullable: false),
                    DeliveryToCustomerName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    FKPaymentTerms = table.Column<int>(type: "int", nullable: false),
                    PaymentTerms = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    FKCurrency = table.Column<int>(type: "int", nullable: false),
                    Currency = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    FKModeofTransport = table.Column<int>(type: "int", nullable: false),
                    ModeofTransport = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
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
                    IsDispatched = table.Column<bool>(type: "bit", nullable: false),
                    DispatchedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FKLocation = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    FKDestination = table.Column<int>(type: "int", nullable: false),
                    Destination = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    MaterialorFinishedProduct = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true),
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
                    table.PrimaryKey("PK_InvoiceToPersons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceToPersons_customerPerson_FKPerson",
                        column: x => x.FKPerson,
                        principalTable: "customerPerson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_InvoiceToPersons_locations_FKLocation",
                        column: x => x.FKLocation,
                        principalTable: "locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_InvoiceToPersons_lookUpMasters_FKCategory",
                        column: x => x.FKCategory,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_InvoiceToPersons_lookUpMasters_FKCurrency",
                        column: x => x.FKCurrency,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_InvoiceToPersons_lookUpMasters_FKDepartment",
                        column: x => x.FKDepartment,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_InvoiceToPersons_lookUpMasters_FKDestination",
                        column: x => x.FKDestination,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceToPersons_lookUpMasters_FKModeofTransport",
                        column: x => x.FKModeofTransport,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_InvoiceToPersons_lookUpMasters_FKPaymentTerms",
                        column: x => x.FKPaymentTerms,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_InvoiceToPersons_lookUpMasters_FKTypeOfInvoice",
                        column: x => x.FKTypeOfInvoice,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_InvoiceToPersons_seasons_FKSeason",
                        column: x => x.FKSeason,
                        principalTable: "seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_InvoiceToPersons_unitMasters_FKUnit",
                        column: x => x.FKUnit,
                        principalTable: "unitMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceToPersons_FKCategory",
                table: "InvoiceToPersons",
                column: "FKCategory");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceToPersons_FKCurrency",
                table: "InvoiceToPersons",
                column: "FKCurrency");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceToPersons_FKDepartment",
                table: "InvoiceToPersons",
                column: "FKDepartment");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceToPersons_FKDestination",
                table: "InvoiceToPersons",
                column: "FKDestination");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceToPersons_FKLocation",
                table: "InvoiceToPersons",
                column: "FKLocation");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceToPersons_FKModeofTransport",
                table: "InvoiceToPersons",
                column: "FKModeofTransport");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceToPersons_FKPaymentTerms",
                table: "InvoiceToPersons",
                column: "FKPaymentTerms");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceToPersons_FKPerson",
                table: "InvoiceToPersons",
                column: "FKPerson");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceToPersons_FKSeason",
                table: "InvoiceToPersons",
                column: "FKSeason");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceToPersons_FKTypeOfInvoice",
                table: "InvoiceToPersons",
                column: "FKTypeOfInvoice");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceToPersons_FKUnit",
                table: "InvoiceToPersons",
                column: "FKUnit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceToPersons");
        }
    }
}
