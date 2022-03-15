using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _041AddPurchaseOrderDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "purchaseOrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKPurchaseOrder = table.Column<int>(type: "int", nullable: false),
                    PurchaseOrderNo = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    FKArticleGroup = table.Column<int>(type: "int", nullable: false),
                    ArticleGroup = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    FKArticle = table.Column<int>(type: "int", nullable: false),
                    Article = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    ArticleColor = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    OrderReferenceNo = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    TotalOrderQuantity = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FKAssortmentId = table.Column<int>(type: "int", nullable: false),
                    NoofCartons = table.Column<int>(type: "int", nullable: false),
                    Size01 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity01 = table.Column<int>(type: "int", nullable: true),
                    Size02 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity02 = table.Column<int>(type: "int", nullable: true),
                    Size03 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity03 = table.Column<int>(type: "int", nullable: true),
                    Size04 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity04 = table.Column<int>(type: "int", nullable: true),
                    Size05 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity05 = table.Column<int>(type: "int", nullable: true),
                    Size06 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity06 = table.Column<int>(type: "int", nullable: true),
                    Size07 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity07 = table.Column<int>(type: "int", nullable: true),
                    Size08 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity08 = table.Column<int>(type: "int", nullable: true),
                    Size09 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity09 = table.Column<int>(type: "int", nullable: true),
                    Size10 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity10 = table.Column<int>(type: "int", nullable: true),
                    Size11 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity11 = table.Column<int>(type: "int", nullable: true),
                    Size12 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity12 = table.Column<int>(type: "int", nullable: true),
                    Size13 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity13 = table.Column<int>(type: "int", nullable: true),
                    Size14 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity14 = table.Column<int>(type: "int", nullable: true),
                    Size15 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity15 = table.Column<int>(type: "int", nullable: true),
                    Size16 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity16 = table.Column<int>(type: "int", nullable: true),
                    Size17 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity17 = table.Column<int>(type: "int", nullable: true),
                    Size18 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity18 = table.Column<int>(type: "int", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPartDeliveryAllowed = table.Column<bool>(type: "bit", nullable: false),
                    ReceivedQuantity = table.Column<int>(type: "int", nullable: false),
                    CancelledQuantity = table.Column<int>(type: "int", nullable: false),
                    BalanceQuantity = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_purchaseOrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_purchaseOrderDetails_articleDetails_FKArticle",
                        column: x => x.FKArticle,
                        principalTable: "articleDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_purchaseOrderDetails_articleGroups_FKArticleGroup",
                        column: x => x.FKArticleGroup,
                        principalTable: "articleGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_purchaseOrderDetails_lookUpMasters_FKOrderStatus",
                        column: x => x.FKOrderStatus,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_purchaseOrderDetails_materialPurchaseOrders_FKPurchaseOrder",
                        column: x => x.FKPurchaseOrder,
                        principalTable: "materialPurchaseOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_purchaseOrderDetails_FKArticle",
                table: "purchaseOrderDetails",
                column: "FKArticle");

            migrationBuilder.CreateIndex(
                name: "IX_purchaseOrderDetails_FKArticleGroup",
                table: "purchaseOrderDetails",
                column: "FKArticleGroup");

            migrationBuilder.CreateIndex(
                name: "IX_purchaseOrderDetails_FKOrderStatus",
                table: "purchaseOrderDetails",
                column: "FKOrderStatus");

            migrationBuilder.CreateIndex(
                name: "IX_purchaseOrderDetails_FKPurchaseOrder",
                table: "purchaseOrderDetails",
                column: "FKPurchaseOrder");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "purchaseOrderDetails");
        }
    }
}
