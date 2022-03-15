using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _030AddMaterialPurchaseOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "materialPurchaseOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKTypeOfOrder = table.Column<int>(type: "int", nullable: false),
                    FKSeason = table.Column<int>(type: "int", nullable: false),
                    FKSource = table.Column<int>(type: "int", nullable: false),
                    FKUnit = table.Column<int>(type: "int", nullable: false),
                    FKParty = table.Column<int>(type: "int", nullable: false),
                    FKDepartment = table.Column<int>(type: "int", nullable: false),
                    FKPOType = table.Column<int>(type: "int", nullable: false),
                    PurchaseOrderNo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    PurchaseOrderDt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FKOrderStatus = table.Column<int>(type: "int", nullable: false),
                    FKPaymentTerms = table.Column<int>(type: "int", nullable: false),
                    FKCurrency = table.Column<int>(type: "int", nullable: false),
                    FKModeofTransport = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    TotalOrderQty = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RecievedQty = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CancelledQty = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BalanceQty = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    POValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ExchangeRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    POValueinINR = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FKDeliveryTo = table.Column<int>(type: "int", nullable: false),
                    IsPOManualClosed = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EnteredSystemId = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteBy = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_materialPurchaseOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_materialPurchaseOrders_lookUpMasters_FKCurrency",
                        column: x => x.FKCurrency,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_materialPurchaseOrders_lookUpMasters_FKDepartment",
                        column: x => x.FKDepartment,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_materialPurchaseOrders_lookUpMasters_FKModeofTransport",
                        column: x => x.FKModeofTransport,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_materialPurchaseOrders_lookUpMasters_FKOrderStatus",
                        column: x => x.FKOrderStatus,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_materialPurchaseOrders_lookUpMasters_FKPaymentTerms",
                        column: x => x.FKPaymentTerms,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_materialPurchaseOrders_lookUpMasters_FKPOType",
                        column: x => x.FKPOType,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_materialPurchaseOrders_lookUpMasters_FKSource",
                        column: x => x.FKSource,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_materialPurchaseOrders_lookUpMasters_FKTypeOfOrder",
                        column: x => x.FKTypeOfOrder,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_materialPurchaseOrders_partyInfos_FKParty",
                        column: x => x.FKParty,
                        principalTable: "partyInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_materialPurchaseOrders_seasons_FKSeason",
                        column: x => x.FKSeason,
                        principalTable: "seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_materialPurchaseOrders_unitMasters_FKDeliveryTo",
                        column: x => x.FKDeliveryTo,
                        principalTable: "unitMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_materialPurchaseOrders_unitMasters_FKUnit",
                        column: x => x.FKUnit,
                        principalTable: "unitMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_materialPurchaseOrders_FKCurrency",
                table: "materialPurchaseOrders",
                column: "FKCurrency");

            migrationBuilder.CreateIndex(
                name: "IX_materialPurchaseOrders_FKDeliveryTo",
                table: "materialPurchaseOrders",
                column: "FKDeliveryTo");

            migrationBuilder.CreateIndex(
                name: "IX_materialPurchaseOrders_FKDepartment",
                table: "materialPurchaseOrders",
                column: "FKDepartment");

            migrationBuilder.CreateIndex(
                name: "IX_materialPurchaseOrders_FKModeofTransport",
                table: "materialPurchaseOrders",
                column: "FKModeofTransport");

            migrationBuilder.CreateIndex(
                name: "IX_materialPurchaseOrders_FKOrderStatus",
                table: "materialPurchaseOrders",
                column: "FKOrderStatus");

            migrationBuilder.CreateIndex(
                name: "IX_materialPurchaseOrders_FKParty",
                table: "materialPurchaseOrders",
                column: "FKParty");

            migrationBuilder.CreateIndex(
                name: "IX_materialPurchaseOrders_FKPaymentTerms",
                table: "materialPurchaseOrders",
                column: "FKPaymentTerms");

            migrationBuilder.CreateIndex(
                name: "IX_materialPurchaseOrders_FKPOType",
                table: "materialPurchaseOrders",
                column: "FKPOType");

            migrationBuilder.CreateIndex(
                name: "IX_materialPurchaseOrders_FKSeason",
                table: "materialPurchaseOrders",
                column: "FKSeason");

            migrationBuilder.CreateIndex(
                name: "IX_materialPurchaseOrders_FKSource",
                table: "materialPurchaseOrders",
                column: "FKSource");

            migrationBuilder.CreateIndex(
                name: "IX_materialPurchaseOrders_FKTypeOfOrder",
                table: "materialPurchaseOrders",
                column: "FKTypeOfOrder");

            migrationBuilder.CreateIndex(
                name: "IX_materialPurchaseOrders_FKUnit",
                table: "materialPurchaseOrders",
                column: "FKUnit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "materialPurchaseOrders");
        }
    }
}
