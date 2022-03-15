using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _128AddStockTransfer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockTransfers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialorFinishedProduct = table.Column<string>(type: "varchar(1)", nullable: true),
                    FKSeason = table.Column<int>(type: "int", nullable: false),
                    Season = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    FKOutwardType = table.Column<int>(type: "int", nullable: false),
                    OutwardType = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    FKFromUnit = table.Column<int>(type: "int", nullable: false),
                    FromUnitName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    FKFromDepartment = table.Column<int>(type: "int", nullable: false),
                    FromDepartment = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    FKFromLocation = table.Column<int>(type: "int", nullable: false),
                    FromLocation = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    FKToUnit = table.Column<int>(type: "int", nullable: false),
                    ToUnitName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    FKToDepartment = table.Column<int>(type: "int", nullable: false),
                    ToDepartment = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    FKToLocation = table.Column<int>(type: "int", nullable: false),
                    ToLocation = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    STNo = table.Column<string>(type: "varchar(20)", nullable: true),
                    STDt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransferredQty = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    ReceivedQty = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    GrossValue = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    GSTValues = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    OtherExpensesPlus = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    OtherExpensesMinus = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    NettValue = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    DtlValue = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    DifferenceValue = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    Remarks = table.Column<string>(type: "Varchar(250)", nullable: true),
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
                    table.PrimaryKey("PK_StockTransfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockTransfers_lookUpMasters_FKFromDepartment",
                        column: x => x.FKFromDepartment,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_StockTransfers_lookUpMasters_FKFromLocation",
                        column: x => x.FKFromLocation,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_StockTransfers_lookUpMasters_FKOutwardType",
                        column: x => x.FKOutwardType,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_StockTransfers_lookUpMasters_FKToDepartment",
                        column: x => x.FKToDepartment,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_StockTransfers_lookUpMasters_FKToLocation",
                        column: x => x.FKToLocation,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_StockTransfers_seasons_FKSeason",
                        column: x => x.FKSeason,
                        principalTable: "seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_StockTransfers_unitMasters_FKFromUnit",
                        column: x => x.FKFromUnit,
                        principalTable: "unitMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_StockTransfers_unitMasters_FKToUnit",
                        column: x => x.FKToUnit,
                        principalTable: "unitMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockTransfers_FKFromDepartment",
                table: "StockTransfers",
                column: "FKFromDepartment");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransfers_FKFromLocation",
                table: "StockTransfers",
                column: "FKFromLocation");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransfers_FKFromUnit",
                table: "StockTransfers",
                column: "FKFromUnit");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransfers_FKOutwardType",
                table: "StockTransfers",
                column: "FKOutwardType");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransfers_FKSeason",
                table: "StockTransfers",
                column: "FKSeason");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransfers_FKToDepartment",
                table: "StockTransfers",
                column: "FKToDepartment");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransfers_FKToLocation",
                table: "StockTransfers",
                column: "FKToLocation");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransfers_FKToUnit",
                table: "StockTransfers",
                column: "FKToUnit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockTransfers");
        }
    }
}
