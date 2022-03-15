using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _049AddPurchaseOrderDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchaseOrderNo",
                table: "purchaseOrderDetails");

            migrationBuilder.AddColumn<string>(
                name: "PurchaseOrderDtlNo",
                table: "purchaseOrderDetails",
                type: "varchar(22)",
                maxLength: 22,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PurchaseOrderMainNo",
                table: "purchaseOrderDetails",
                type: "varchar(18)",
                maxLength: 18,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchaseOrderDtlNo",
                table: "purchaseOrderDetails");

            migrationBuilder.DropColumn(
                name: "PurchaseOrderMainNo",
                table: "purchaseOrderDetails");

            migrationBuilder.AddColumn<string>(
                name: "PurchaseOrderNo",
                table: "purchaseOrderDetails",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);
        }
    }
}
