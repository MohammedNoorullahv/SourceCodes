using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _174AddLGPurchaseOrderMain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LGPurchaseOrderMains",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKPurchaseOrder = table.Column<int>(type: "int", nullable: false),
                    PurchaseOrderNo = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    PurchaseOrderMainNo = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true),
                    FKLeatherGoodsGroup = table.Column<int>(type: "int", nullable: false),
                    LeatherGoodsGroup = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    Article = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    OrderReferenceNo = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    TotalOrderQuantity = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPartDeliveryAllowed = table.Column<bool>(type: "bit", nullable: false),
                    FKSizeMaster = table.Column<int>(type: "int", nullable: false),
                    SizeCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    EnteredQuantity = table.Column<int>(type: "int", nullable: false),
                    ReceivedQuantity = table.Column<int>(type: "int", nullable: false),
                    CancelledQuantity = table.Column<int>(type: "int", nullable: false),
                    BalanceQuantity = table.Column<int>(type: "int", nullable: false),
                    FKDestination = table.Column<int>(type: "int", nullable: false),
                    Destination = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    FKDeliveryTo = table.Column<int>(type: "int", nullable: false),
                    DeliveryTo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    FKDeliveryLocation = table.Column<int>(type: "int", nullable: false),
                    DeliveryLocation = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    FKModeofTransport = table.Column<int>(type: "int", nullable: false),
                    ModeofTransport = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    FKOrderStatus = table.Column<int>(type: "int", nullable: false),
                    OrderStatus = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
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
                    table.PrimaryKey("PK_LGPurchaseOrderMains", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LGPurchaseOrderMains_LeatherGoodsGroups_FKLeatherGoodsGroup",
                        column: x => x.FKLeatherGoodsGroup,
                        principalTable: "LeatherGoodsGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LGPurchaseOrderMains_lookUpMasters_FKDeliveryTo",
                        column: x => x.FKDeliveryTo,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LGPurchaseOrderMains_lookUpMasters_FKDestination",
                        column: x => x.FKDestination,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LGPurchaseOrderMains_lookUpMasters_FKModeofTransport",
                        column: x => x.FKModeofTransport,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LGPurchaseOrderMains_lookUpMasters_FKOrderStatus",
                        column: x => x.FKOrderStatus,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LGPurchaseOrderMains_purchaseOrders_FKPurchaseOrder",
                        column: x => x.FKPurchaseOrder,
                        principalTable: "purchaseOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LGPurchaseOrderMains_sizeMasters_FKSizeMaster",
                        column: x => x.FKSizeMaster,
                        principalTable: "sizeMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LGPurchaseOrderMains_FKDeliveryTo",
                table: "LGPurchaseOrderMains",
                column: "FKDeliveryTo");

            migrationBuilder.CreateIndex(
                name: "IX_LGPurchaseOrderMains_FKDestination",
                table: "LGPurchaseOrderMains",
                column: "FKDestination");

            migrationBuilder.CreateIndex(
                name: "IX_LGPurchaseOrderMains_FKLeatherGoodsGroup",
                table: "LGPurchaseOrderMains",
                column: "FKLeatherGoodsGroup");

            migrationBuilder.CreateIndex(
                name: "IX_LGPurchaseOrderMains_FKModeofTransport",
                table: "LGPurchaseOrderMains",
                column: "FKModeofTransport");

            migrationBuilder.CreateIndex(
                name: "IX_LGPurchaseOrderMains_FKOrderStatus",
                table: "LGPurchaseOrderMains",
                column: "FKOrderStatus");

            migrationBuilder.CreateIndex(
                name: "IX_LGPurchaseOrderMains_FKPurchaseOrder",
                table: "LGPurchaseOrderMains",
                column: "FKPurchaseOrder");

            migrationBuilder.CreateIndex(
                name: "IX_LGPurchaseOrderMains_FKSizeMaster",
                table: "LGPurchaseOrderMains",
                column: "FKSizeMaster");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LGPurchaseOrderMains");
        }
    }
}
