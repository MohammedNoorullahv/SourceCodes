using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _192UpdateMaterialPurchaseOrderAddIsEntryCompleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEntryCompleted",
                table: "materialPurchaseOrderDetails");

            migrationBuilder.AddColumn<bool>(
                name: "IsEntryCompleted",
                table: "materialPurchaseOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEntryCompleted",
                table: "materialPurchaseOrders");

            migrationBuilder.AddColumn<bool>(
                name: "IsEntryCompleted",
                table: "materialPurchaseOrderDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
