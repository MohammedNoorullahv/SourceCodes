using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _039AddPurchaseOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "purchaseOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKTypeOfOrder = table.Column<int>(type: "int", nullable: false),
                    TypeofOrder = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    FKSeason = table.Column<int>(type: "int", nullable: false),
                    Season = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    FKSource = table.Column<int>(type: "int", nullable: false),
                    Source = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    FKUnit = table.Column<int>(type: "int", nullable: false),
                    UnitName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    FKParty = table.Column<int>(type: "int", nullable: false),
                    SupplierName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    FKDepartment = table.Column<int>(type: "int", nullable: false),
                    Department = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    FKPOType = table.Column<int>(type: "int", nullable: false),
                    POType = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    PurchaseOrderNo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    PurchaseOrderDt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FKOrderStatus = table.Column<int>(type: "int", nullable: false),
                    OrderStatus = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    FKPaymentTerms = table.Column<int>(type: "int", nullable: false),
                    PaymentTerms = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    FKCurrency = table.Column<int>(type: "int", nullable: false),
                    Currency = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    FKModeofTransport = table.Column<int>(type: "int", nullable: false),
                    ModeofTransport = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    Remarks = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    TotalOrderQty = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RecievedQty = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CancelledQty = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BalanceQty = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    POValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ExchangeRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    POValueinINR = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FKDeliveryTo = table.Column<int>(type: "int", nullable: false),
                    DeliveryTo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    IsPOManualClosed = table.Column<bool>(type: "bit", nullable: false),
                    FKSizeMaster = table.Column<int>(type: "int", nullable: false),
                    IsAssortmentOrder = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_purchaseOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_purchaseOrders_lookUpMasters_FKCurrency",
                        column: x => x.FKCurrency,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_purchaseOrders_lookUpMasters_FKDepartment",
                        column: x => x.FKDepartment,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_purchaseOrders_lookUpMasters_FKModeofTransport",
                        column: x => x.FKModeofTransport,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_purchaseOrders_lookUpMasters_FKOrderStatus",
                        column: x => x.FKOrderStatus,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_purchaseOrders_lookUpMasters_FKPaymentTerms",
                        column: x => x.FKPaymentTerms,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_purchaseOrders_lookUpMasters_FKPOType",
                        column: x => x.FKPOType,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_purchaseOrders_lookUpMasters_FKSource",
                        column: x => x.FKSource,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_purchaseOrders_lookUpMasters_FKTypeOfOrder",
                        column: x => x.FKTypeOfOrder,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_purchaseOrders_partyInfos_FKParty",
                        column: x => x.FKParty,
                        principalTable: "partyInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_purchaseOrders_seasons_FKSeason",
                        column: x => x.FKSeason,
                        principalTable: "seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_purchaseOrders_sizeMasters_FKSizeMaster",
                        column: x => x.FKSizeMaster,
                        principalTable: "sizeMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_purchaseOrders_unitMasters_FKDeliveryTo",
                        column: x => x.FKDeliveryTo,
                        principalTable: "unitMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_purchaseOrders_unitMasters_FKUnit",
                        column: x => x.FKUnit,
                        principalTable: "unitMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_purchaseOrders_FKCurrency",
                table: "purchaseOrders",
                column: "FKCurrency");

            migrationBuilder.CreateIndex(
                name: "IX_purchaseOrders_FKDeliveryTo",
                table: "purchaseOrders",
                column: "FKDeliveryTo");

            migrationBuilder.CreateIndex(
                name: "IX_purchaseOrders_FKDepartment",
                table: "purchaseOrders",
                column: "FKDepartment");

            migrationBuilder.CreateIndex(
                name: "IX_purchaseOrders_FKModeofTransport",
                table: "purchaseOrders",
                column: "FKModeofTransport");

            migrationBuilder.CreateIndex(
                name: "IX_purchaseOrders_FKOrderStatus",
                table: "purchaseOrders",
                column: "FKOrderStatus");

            migrationBuilder.CreateIndex(
                name: "IX_purchaseOrders_FKParty",
                table: "purchaseOrders",
                column: "FKParty");

            migrationBuilder.CreateIndex(
                name: "IX_purchaseOrders_FKPaymentTerms",
                table: "purchaseOrders",
                column: "FKPaymentTerms");

            migrationBuilder.CreateIndex(
                name: "IX_purchaseOrders_FKPOType",
                table: "purchaseOrders",
                column: "FKPOType");

            migrationBuilder.CreateIndex(
                name: "IX_purchaseOrders_FKSeason",
                table: "purchaseOrders",
                column: "FKSeason");

            migrationBuilder.CreateIndex(
                name: "IX_purchaseOrders_FKSizeMaster",
                table: "purchaseOrders",
                column: "FKSizeMaster");

            migrationBuilder.CreateIndex(
                name: "IX_purchaseOrders_FKSource",
                table: "purchaseOrders",
                column: "FKSource");

            migrationBuilder.CreateIndex(
                name: "IX_purchaseOrders_FKTypeOfOrder",
                table: "purchaseOrders",
                column: "FKTypeOfOrder");

            migrationBuilder.CreateIndex(
                name: "IX_purchaseOrders_FKUnit",
                table: "purchaseOrders",
                column: "FKUnit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "purchaseOrders");
        }
    }
}
