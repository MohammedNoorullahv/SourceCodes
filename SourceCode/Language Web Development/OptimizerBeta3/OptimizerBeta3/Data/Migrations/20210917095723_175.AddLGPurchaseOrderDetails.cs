using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _175AddLGPurchaseOrderDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LGPurchaseOrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKPurchaseOrderMain = table.Column<int>(type: "int", nullable: false),
                    PurchaseOrderNo = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true),
                    PurchaseOrderMainNo = table.Column<string>(type: "varchar(18)", maxLength: 18, nullable: true),
                    PurchaseOrderDtlNo = table.Column<string>(type: "varchar(22)", maxLength: 22, nullable: true),
                    LeatherGoodsGroup = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    FKLeatherGoods = table.Column<int>(type: "int", nullable: false),
                    LeatherGoodsDescription = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    LeatherGoodsColor = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    OrderReferenceNo = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    FKLGSize = table.Column<int>(type: "int", nullable: false),
                    LeatherGoodsSize = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPartDeliveryAllowed = table.Column<bool>(type: "bit", nullable: false),
                    ReceivedQuantity = table.Column<int>(type: "int", nullable: false),
                    CancelledQuantity = table.Column<int>(type: "int", nullable: false),
                    BalanceQuantity = table.Column<int>(type: "int", nullable: false),
                    FKOrderStatus = table.Column<int>(type: "int", nullable: false),
                    OrderStatus = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    FKUOM = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_LGPurchaseOrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LGPurchaseOrderDetails_articleDetails_FKUOM",
                        column: x => x.FKUOM,
                        principalTable: "articleDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LGPurchaseOrderDetails_leatherGoodsDetails_FKLeatherGoods",
                        column: x => x.FKLeatherGoods,
                        principalTable: "leatherGoodsDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LGPurchaseOrderDetails_lookUpMasters_FKOrderStatus",
                        column: x => x.FKOrderStatus,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LGPurchaseOrderDetails_purchaseOrderMains_FKPurchaseOrderMain",
                        column: x => x.FKPurchaseOrderMain,
                        principalTable: "purchaseOrderMains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LGPurchaseOrderDetails_SizeMasterforLeatherGoods_FKLGSize",
                        column: x => x.FKLGSize,
                        principalTable: "SizeMasterforLeatherGoods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LGPurchaseOrderDetails_FKLeatherGoods",
                table: "LGPurchaseOrderDetails",
                column: "FKLeatherGoods");

            migrationBuilder.CreateIndex(
                name: "IX_LGPurchaseOrderDetails_FKLGSize",
                table: "LGPurchaseOrderDetails",
                column: "FKLGSize");

            migrationBuilder.CreateIndex(
                name: "IX_LGPurchaseOrderDetails_FKOrderStatus",
                table: "LGPurchaseOrderDetails",
                column: "FKOrderStatus");

            migrationBuilder.CreateIndex(
                name: "IX_LGPurchaseOrderDetails_FKPurchaseOrderMain",
                table: "LGPurchaseOrderDetails",
                column: "FKPurchaseOrderMain");

            migrationBuilder.CreateIndex(
                name: "IX_LGPurchaseOrderDetails_FKUOM",
                table: "LGPurchaseOrderDetails",
                column: "FKUOM");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LGPurchaseOrderDetails");
        }
    }
}
