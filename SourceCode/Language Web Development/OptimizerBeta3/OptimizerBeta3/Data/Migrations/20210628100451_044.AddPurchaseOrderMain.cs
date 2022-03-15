using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _044AddPurchaseOrderMain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_purchaseOrderDetails_articleGroups_FKArticleGroup",
                table: "purchaseOrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_purchaseOrderDetails_materialPurchaseOrders_FKPurchaseOrder",
                table: "purchaseOrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_purchaseOrderDetails_FKArticleGroup",
                table: "purchaseOrderDetails");

            migrationBuilder.DropColumn(
                name: "FKArticleGroup",
                table: "purchaseOrderDetails");

            migrationBuilder.RenameColumn(
                name: "FKPurchaseOrder",
                table: "purchaseOrderDetails",
                newName: "FKPurchaseOrderMain");

            migrationBuilder.RenameIndex(
                name: "IX_purchaseOrderDetails_FKPurchaseOrder",
                table: "purchaseOrderDetails",
                newName: "IX_purchaseOrderDetails_FKPurchaseOrderMain");

            migrationBuilder.CreateTable(
                name: "purchaseOrderMains",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKPurchaseOrder = table.Column<int>(type: "int", nullable: false),
                    PurchaseOrderNo = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    FKArticleGroup = table.Column<int>(type: "int", nullable: false),
                    ArticleGroup = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    OrderReferenceNo = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    OrderQuantity = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPartDeliveryAllowed = table.Column<bool>(type: "bit", nullable: false),
                    ReceivedQuantity = table.Column<int>(type: "int", nullable: false),
                    CancelledQuantity = table.Column<int>(type: "int", nullable: false),
                    BalanceQuantity = table.Column<int>(type: "int", nullable: false),
                    FKDestination = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_purchaseOrderMains", x => x.Id);
                    table.ForeignKey(
                        name: "FK_purchaseOrderMains_articleGroups_FKArticleGroup",
                        column: x => x.FKArticleGroup,
                        principalTable: "articleGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_purchaseOrderMains_lookUpMasters_FKDestination",
                        column: x => x.FKDestination,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_purchaseOrderMains_lookUpMasters_FKOrderStatus",
                        column: x => x.FKOrderStatus,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_purchaseOrderMains_materialPurchaseOrders_FKPurchaseOrder",
                        column: x => x.FKPurchaseOrder,
                        principalTable: "materialPurchaseOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_purchaseOrderMains_FKArticleGroup",
                table: "purchaseOrderMains",
                column: "FKArticleGroup");

            migrationBuilder.CreateIndex(
                name: "IX_purchaseOrderMains_FKDestination",
                table: "purchaseOrderMains",
                column: "FKDestination");

            migrationBuilder.CreateIndex(
                name: "IX_purchaseOrderMains_FKOrderStatus",
                table: "purchaseOrderMains",
                column: "FKOrderStatus");

            migrationBuilder.CreateIndex(
                name: "IX_purchaseOrderMains_FKPurchaseOrder",
                table: "purchaseOrderMains",
                column: "FKPurchaseOrder");

            migrationBuilder.AddForeignKey(
                name: "FK_purchaseOrderDetails_purchaseOrderMains_FKPurchaseOrderMain",
                table: "purchaseOrderDetails",
                column: "FKPurchaseOrderMain",
                principalTable: "purchaseOrderMains",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_purchaseOrderDetails_purchaseOrderMains_FKPurchaseOrderMain",
                table: "purchaseOrderDetails");

            migrationBuilder.DropTable(
                name: "purchaseOrderMains");

            migrationBuilder.RenameColumn(
                name: "FKPurchaseOrderMain",
                table: "purchaseOrderDetails",
                newName: "FKPurchaseOrder");

            migrationBuilder.RenameIndex(
                name: "IX_purchaseOrderDetails_FKPurchaseOrderMain",
                table: "purchaseOrderDetails",
                newName: "IX_purchaseOrderDetails_FKPurchaseOrder");

            migrationBuilder.AddColumn<int>(
                name: "FKArticleGroup",
                table: "purchaseOrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_purchaseOrderDetails_FKArticleGroup",
                table: "purchaseOrderDetails",
                column: "FKArticleGroup");

            migrationBuilder.AddForeignKey(
                name: "FK_purchaseOrderDetails_articleGroups_FKArticleGroup",
                table: "purchaseOrderDetails",
                column: "FKArticleGroup",
                principalTable: "articleGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_purchaseOrderDetails_materialPurchaseOrders_FKPurchaseOrder",
                table: "purchaseOrderDetails",
                column: "FKPurchaseOrder",
                principalTable: "materialPurchaseOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
