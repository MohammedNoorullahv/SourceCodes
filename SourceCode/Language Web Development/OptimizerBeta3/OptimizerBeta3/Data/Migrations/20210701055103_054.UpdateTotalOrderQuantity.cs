using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _054UpdateTotalOrderQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RecievedQuantity",
                table: "purchaseOrders",
                newName: "ReceivedQuantity");

            migrationBuilder.RenameColumn(
                name: "OrderQuantity",
                table: "purchaseOrderMains",
                newName: "TotalOrderQuantity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReceivedQuantity",
                table: "purchaseOrders",
                newName: "RecievedQuantity");

            migrationBuilder.RenameColumn(
                name: "TotalOrderQuantity",
                table: "purchaseOrderMains",
                newName: "OrderQuantity");
        }
    }
}
