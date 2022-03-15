using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _036UpdateMPODetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FKOrderStatus",
                table: "materialPurchaseOrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OrderStatus",
                table: "materialPurchaseOrderDetails",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_materialPurchaseOrderDetails_FKOrderStatus",
                table: "materialPurchaseOrderDetails",
                column: "FKOrderStatus");

            migrationBuilder.AddForeignKey(
                name: "FK_materialPurchaseOrderDetails_lookUpMasters_FKOrderStatus",
                table: "materialPurchaseOrderDetails",
                column: "FKOrderStatus",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_materialPurchaseOrderDetails_lookUpMasters_FKOrderStatus",
                table: "materialPurchaseOrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_materialPurchaseOrderDetails_FKOrderStatus",
                table: "materialPurchaseOrderDetails");

            migrationBuilder.DropColumn(
                name: "FKOrderStatus",
                table: "materialPurchaseOrderDetails");

            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "materialPurchaseOrderDetails");
        }
    }
}
