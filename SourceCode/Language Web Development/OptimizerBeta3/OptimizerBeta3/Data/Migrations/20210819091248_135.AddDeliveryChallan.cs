using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _135AddDeliveryChallan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeliveryChallans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialorFinishedProduct = table.Column<string>(type: "varchar(1)", nullable: true),
                    FKSeason = table.Column<int>(type: "int", nullable: false),
                    Season = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    FKDCType = table.Column<int>(type: "int", nullable: false),
                    DCType = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    FKFromUnit = table.Column<int>(type: "int", nullable: false),
                    FromUnitName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    FKFromState = table.Column<int>(type: "int", nullable: false),
                    FKFromDepartment = table.Column<int>(type: "int", nullable: false),
                    FromDepartment = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    FKFromLocation = table.Column<int>(type: "int", nullable: false),
                    FromLocation = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    FKToUnit = table.Column<int>(type: "int", nullable: false),
                    ToUnitName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    FKToState = table.Column<int>(type: "int", nullable: false),
                    FKToDepartment = table.Column<int>(type: "int", nullable: false),
                    ToDepartment = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    FKToLocation = table.Column<int>(type: "int", nullable: false),
                    ToLocation = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    DCNo = table.Column<string>(type: "varchar(20)", nullable: true),
                    DCDt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FKModeofTransport = table.Column<int>(type: "int", nullable: false),
                    ModeofTranspoft = table.Column<string>(type: "varchar(20)", nullable: true),
                    VehicleNo = table.Column<string>(type: "varchar(20)", nullable: true),
                    Quantity = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    Value = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    GSTValues = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    GrossValue = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    OtherExpensesPlus = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    OtherExpensesMinus = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    NettValue = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    DtlValue = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    DifferenceValue = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    Remarks = table.Column<string>(type: "Varchar(250)", nullable: true),
                    IsEntryCompleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EnteredSystemId = table.Column<string>(type: "varchar(30)", nullable: true),
                    IsAcknowledged = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteBy = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryChallans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryChallans_lookUpMasters_FKDCType",
                        column: x => x.FKDCType,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_DeliveryChallans_lookUpMasters_FKFromDepartment",
                        column: x => x.FKFromDepartment,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_DeliveryChallans_lookUpMasters_FKFromLocation",
                        column: x => x.FKFromLocation,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_DeliveryChallans_lookUpMasters_FKModeofTransport",
                        column: x => x.FKModeofTransport,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_DeliveryChallans_lookUpMasters_FKToDepartment",
                        column: x => x.FKToDepartment,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_DeliveryChallans_lookUpMasters_FKToLocation",
                        column: x => x.FKToLocation,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_DeliveryChallans_seasons_FKSeason",
                        column: x => x.FKSeason,
                        principalTable: "seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_DeliveryChallans_unitMasters_FKFromUnit",
                        column: x => x.FKFromUnit,
                        principalTable: "unitMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_DeliveryChallans_unitMasters_FKToUnit",
                        column: x => x.FKToUnit,
                        principalTable: "unitMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryChallans_FKDCType",
                table: "DeliveryChallans",
                column: "FKDCType");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryChallans_FKFromDepartment",
                table: "DeliveryChallans",
                column: "FKFromDepartment");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryChallans_FKFromLocation",
                table: "DeliveryChallans",
                column: "FKFromLocation");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryChallans_FKFromUnit",
                table: "DeliveryChallans",
                column: "FKFromUnit");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryChallans_FKModeofTransport",
                table: "DeliveryChallans",
                column: "FKModeofTransport");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryChallans_FKSeason",
                table: "DeliveryChallans",
                column: "FKSeason");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryChallans_FKToDepartment",
                table: "DeliveryChallans",
                column: "FKToDepartment");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryChallans_FKToLocation",
                table: "DeliveryChallans",
                column: "FKToLocation");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryChallans_FKToUnit",
                table: "DeliveryChallans",
                column: "FKToUnit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryChallans");
        }
    }
}
