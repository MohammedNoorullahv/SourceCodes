using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _045UpdatePurchaseOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_purchaseOrders_lookUpMasters_FKModeofTransport",
                table: "purchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_purchaseOrders_sizeMasters_FKSizeMaster",
                table: "purchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_purchaseOrders_unitMasters_FKDeliveryTo",
                table: "purchaseOrders");

            migrationBuilder.DropIndex(
                name: "IX_purchaseOrders_FKDeliveryTo",
                table: "purchaseOrders");

            migrationBuilder.DropIndex(
                name: "IX_purchaseOrders_FKModeofTransport",
                table: "purchaseOrders");

            migrationBuilder.DropIndex(
                name: "IX_purchaseOrders_FKSizeMaster",
                table: "purchaseOrders");

            migrationBuilder.DropColumn(
                name: "DeliveryTo",
                table: "purchaseOrders");

            migrationBuilder.DropColumn(
                name: "FKDeliveryTo",
                table: "purchaseOrders");

            migrationBuilder.DropColumn(
                name: "FKModeofTransport",
                table: "purchaseOrders");

            migrationBuilder.DropColumn(
                name: "FKSizeMaster",
                table: "purchaseOrders");

            migrationBuilder.DropColumn(
                name: "ModeofTransport",
                table: "purchaseOrders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliveryTo",
                table: "purchaseOrders",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FKDeliveryTo",
                table: "purchaseOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKModeofTransport",
                table: "purchaseOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKSizeMaster",
                table: "purchaseOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ModeofTransport",
                table: "purchaseOrders",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_purchaseOrders_FKDeliveryTo",
                table: "purchaseOrders",
                column: "FKDeliveryTo");

            migrationBuilder.CreateIndex(
                name: "IX_purchaseOrders_FKModeofTransport",
                table: "purchaseOrders",
                column: "FKModeofTransport");

            migrationBuilder.CreateIndex(
                name: "IX_purchaseOrders_FKSizeMaster",
                table: "purchaseOrders",
                column: "FKSizeMaster");

            migrationBuilder.AddForeignKey(
                name: "FK_purchaseOrders_lookUpMasters_FKModeofTransport",
                table: "purchaseOrders",
                column: "FKModeofTransport",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_purchaseOrders_sizeMasters_FKSizeMaster",
                table: "purchaseOrders",
                column: "FKSizeMaster",
                principalTable: "sizeMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_purchaseOrders_unitMasters_FKDeliveryTo",
                table: "purchaseOrders",
                column: "FKDeliveryTo",
                principalTable: "unitMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
