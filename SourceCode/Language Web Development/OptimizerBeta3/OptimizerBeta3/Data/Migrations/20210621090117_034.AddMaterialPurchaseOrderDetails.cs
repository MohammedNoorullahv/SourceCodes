using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _034AddMaterialPurchaseOrderDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "materialPurchaseOrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKPurchaseOrder = table.Column<int>(type: "int", nullable: false),
                    PurchaseOrderNo = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    OrderReferenceNo = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    FKMaterial = table.Column<int>(type: "int", nullable: false),
                    Material = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    UOM = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPartDeliveryAllowed = table.Column<bool>(type: "bit", nullable: false),
                    ReceivedQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CancelledQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BalanceQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_materialPurchaseOrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_materialPurchaseOrderDetails_materialPurchaseOrders_FKPurchaseOrder",
                        column: x => x.FKPurchaseOrder,
                        principalTable: "materialPurchaseOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_materialPurchaseOrderDetails_materials_FKMaterial",
                        column: x => x.FKMaterial,
                        principalTable: "materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_materialPurchaseOrderDetails_FKMaterial",
                table: "materialPurchaseOrderDetails",
                column: "FKMaterial");

            migrationBuilder.CreateIndex(
                name: "IX_materialPurchaseOrderDetails_FKPurchaseOrder",
                table: "materialPurchaseOrderDetails",
                column: "FKPurchaseOrder");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "materialPurchaseOrderDetails");
        }
    }
}
