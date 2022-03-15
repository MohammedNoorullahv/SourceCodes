using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _173AddLGPurchaseOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LGPurchaseOrders",
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
                    PurchaseOrderNo = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    PurchaseOrderDt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FKOrderStatus = table.Column<int>(type: "int", nullable: false),
                    OrderStatus = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    FKPaymentTerms = table.Column<int>(type: "int", nullable: false),
                    PaymentTerms = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    FKCurrency = table.Column<int>(type: "int", nullable: false),
                    Currency = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    Remarks = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    TotalOrderQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EnteredQuantity = table.Column<int>(type: "int", nullable: true),
                    ReceivedQuantity = table.Column<int>(type: "int", nullable: true),
                    CancelledQuantity = table.Column<int>(type: "int", nullable: true),
                    BalanceQuantity = table.Column<int>(type: "int", nullable: true),
                    POValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ExchangeRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    POValueinINR = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsPOManualClosed = table.Column<bool>(type: "bit", nullable: false),
                    IsAssortmentOrder = table.Column<bool>(type: "bit", nullable: false),
                    FLAM = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsArticleStockGenerated = table.Column<bool>(type: "bit", nullable: false),
                    FKCategory = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
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
                    table.PrimaryKey("PK_LGPurchaseOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LGPurchaseOrders_lookUpMasters_FKCategory",
                        column: x => x.FKCategory,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LGPurchaseOrders_lookUpMasters_FKCurrency",
                        column: x => x.FKCurrency,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LGPurchaseOrders_lookUpMasters_FKOrderStatus",
                        column: x => x.FKOrderStatus,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LGPurchaseOrders_lookUpMasters_FKPaymentTerms",
                        column: x => x.FKPaymentTerms,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LGPurchaseOrders_lookUpMasters_FKSource",
                        column: x => x.FKSource,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LGPurchaseOrders_lookUpMasters_FKTypeOfOrder",
                        column: x => x.FKTypeOfOrder,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LGPurchaseOrders_partyInfos_FKParty",
                        column: x => x.FKParty,
                        principalTable: "partyInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LGPurchaseOrders_seasons_FKSeason",
                        column: x => x.FKSeason,
                        principalTable: "seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LGPurchaseOrders_unitMasters_FKUnit",
                        column: x => x.FKUnit,
                        principalTable: "unitMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LGPurchaseOrders_FKCategory",
                table: "LGPurchaseOrders",
                column: "FKCategory");

            migrationBuilder.CreateIndex(
                name: "IX_LGPurchaseOrders_FKCurrency",
                table: "LGPurchaseOrders",
                column: "FKCurrency");

            migrationBuilder.CreateIndex(
                name: "IX_LGPurchaseOrders_FKOrderStatus",
                table: "LGPurchaseOrders",
                column: "FKOrderStatus");

            migrationBuilder.CreateIndex(
                name: "IX_LGPurchaseOrders_FKParty",
                table: "LGPurchaseOrders",
                column: "FKParty");

            migrationBuilder.CreateIndex(
                name: "IX_LGPurchaseOrders_FKPaymentTerms",
                table: "LGPurchaseOrders",
                column: "FKPaymentTerms");

            migrationBuilder.CreateIndex(
                name: "IX_LGPurchaseOrders_FKSeason",
                table: "LGPurchaseOrders",
                column: "FKSeason");

            migrationBuilder.CreateIndex(
                name: "IX_LGPurchaseOrders_FKSource",
                table: "LGPurchaseOrders",
                column: "FKSource");

            migrationBuilder.CreateIndex(
                name: "IX_LGPurchaseOrders_FKTypeOfOrder",
                table: "LGPurchaseOrders",
                column: "FKTypeOfOrder");

            migrationBuilder.CreateIndex(
                name: "IX_LGPurchaseOrders_FKUnit",
                table: "LGPurchaseOrders",
                column: "FKUnit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LGPurchaseOrders");
        }
    }
}
