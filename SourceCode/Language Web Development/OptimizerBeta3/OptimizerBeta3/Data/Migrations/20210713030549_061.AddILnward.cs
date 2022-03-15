using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _061AddILnward : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "inwards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKSeason = table.Column<int>(type: "int", nullable: false),
                    FKSource = table.Column<int>(type: "int", nullable: false),
                    FKUnit = table.Column<int>(type: "int", nullable: false),
                    FKParty = table.Column<int>(type: "int", nullable: false),
                    FKDepartment = table.Column<int>(type: "int", nullable: false),
                    FKPOType = table.Column<int>(type: "int", nullable: false),
                    MaterialorFinishedProduct = table.Column<string>(type: "varchar(1)", nullable: true),
                    InwardNo = table.Column<string>(type: "varchar(20)", nullable: true),
                    InwardDt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FKDocumentType = table.Column<int>(type: "int", nullable: false),
                    SupplierDocumentNo = table.Column<string>(type: "varchar(30)", nullable: true),
                    SupplierDocumentDt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FKCurrency = table.Column<int>(type: "int", nullable: false),
                    ExchangeRate = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    InvoiceGrossValue = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    GSTValues = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    OtherExpensesPlus = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    OtherExpensesMinus = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    InvoiceNettValue = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    InvoiceDtlValue = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    DifferenceValue = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_inwards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_inwards_lookUpMasters_FKCurrency",
                        column: x => x.FKCurrency,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inwards_lookUpMasters_FKDepartment",
                        column: x => x.FKDepartment,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_inwards_lookUpMasters_FKDocumentType",
                        column: x => x.FKDocumentType,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_inwards_lookUpMasters_FKPOType",
                        column: x => x.FKPOType,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_inwards_lookUpMasters_FKSource",
                        column: x => x.FKSource,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_inwards_partyInfos_FKParty",
                        column: x => x.FKParty,
                        principalTable: "partyInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_inwards_seasons_FKSeason",
                        column: x => x.FKSeason,
                        principalTable: "seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_inwards_unitMasters_FKUnit",
                        column: x => x.FKUnit,
                        principalTable: "unitMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_inwards_FKCurrency",
                table: "inwards",
                column: "FKCurrency");

            migrationBuilder.CreateIndex(
                name: "IX_inwards_FKDepartment",
                table: "inwards",
                column: "FKDepartment");

            migrationBuilder.CreateIndex(
                name: "IX_inwards_FKDocumentType",
                table: "inwards",
                column: "FKDocumentType");

            migrationBuilder.CreateIndex(
                name: "IX_inwards_FKParty",
                table: "inwards",
                column: "FKParty");

            migrationBuilder.CreateIndex(
                name: "IX_inwards_FKPOType",
                table: "inwards",
                column: "FKPOType");

            migrationBuilder.CreateIndex(
                name: "IX_inwards_FKSeason",
                table: "inwards",
                column: "FKSeason");

            migrationBuilder.CreateIndex(
                name: "IX_inwards_FKSource",
                table: "inwards",
                column: "FKSource");

            migrationBuilder.CreateIndex(
                name: "IX_inwards_FKUnit",
                table: "inwards",
                column: "FKUnit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "inwards");
        }
    }
}
