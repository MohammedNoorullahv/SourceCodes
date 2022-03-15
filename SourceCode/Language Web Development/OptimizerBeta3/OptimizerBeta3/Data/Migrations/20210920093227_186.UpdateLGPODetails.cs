using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _186UpdateLGPODetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAssortmentOrder",
                table: "LGPurchaseOrders");

            migrationBuilder.RenameColumn(
                name: "EnteredQuantity",
                table: "LGPurchaseOrders",
                newName: "MainEnteredQuantity");

            migrationBuilder.AddColumn<int>(
                name: "DtlEnteredQuantity",
                table: "LGPurchaseOrders",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DtlEnteredQuantity",
                table: "LGPurchaseOrders");

            migrationBuilder.RenameColumn(
                name: "MainEnteredQuantity",
                table: "LGPurchaseOrders",
                newName: "EnteredQuantity");

            migrationBuilder.AddColumn<bool>(
                name: "IsAssortmentOrder",
                table: "LGPurchaseOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
