using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _046UpdatePurchaseOrderMain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PurchaseOrderNo",
                table: "purchaseOrders",
                type: "varchar(12)",
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "PurchaseOrderNo",
                table: "purchaseOrderMains",
                type: "varchar(12)",
                maxLength: 12,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryLocation",
                table: "purchaseOrderMains",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryTo",
                table: "purchaseOrderMains",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FKDeliveryLocation",
                table: "purchaseOrderMains",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKDeliveryTo",
                table: "purchaseOrderMains",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKModeofTransport",
                table: "purchaseOrderMains",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKSizeMaster",
                table: "purchaseOrderMains",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ModeofTransport",
                table: "purchaseOrderMains",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PurchaseOrderMainNo",
                table: "purchaseOrderMains",
                type: "varchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_purchaseOrderMains_FKDeliveryTo",
                table: "purchaseOrderMains",
                column: "FKDeliveryTo");

            migrationBuilder.CreateIndex(
                name: "IX_purchaseOrderMains_FKModeofTransport",
                table: "purchaseOrderMains",
                column: "FKModeofTransport");

            migrationBuilder.CreateIndex(
                name: "IX_purchaseOrderMains_FKSizeMaster",
                table: "purchaseOrderMains",
                column: "FKSizeMaster");

            migrationBuilder.AddForeignKey(
                name: "FK_purchaseOrderMains_lookUpMasters_FKDeliveryTo",
                table: "purchaseOrderMains",
                column: "FKDeliveryTo",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_purchaseOrderMains_lookUpMasters_FKModeofTransport",
                table: "purchaseOrderMains",
                column: "FKModeofTransport",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_purchaseOrderMains_sizeMasters_FKSizeMaster",
                table: "purchaseOrderMains",
                column: "FKSizeMaster",
                principalTable: "sizeMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_purchaseOrderMains_lookUpMasters_FKDeliveryTo",
                table: "purchaseOrderMains");

            migrationBuilder.DropForeignKey(
                name: "FK_purchaseOrderMains_lookUpMasters_FKModeofTransport",
                table: "purchaseOrderMains");

            migrationBuilder.DropForeignKey(
                name: "FK_purchaseOrderMains_sizeMasters_FKSizeMaster",
                table: "purchaseOrderMains");

            migrationBuilder.DropIndex(
                name: "IX_purchaseOrderMains_FKDeliveryTo",
                table: "purchaseOrderMains");

            migrationBuilder.DropIndex(
                name: "IX_purchaseOrderMains_FKModeofTransport",
                table: "purchaseOrderMains");

            migrationBuilder.DropIndex(
                name: "IX_purchaseOrderMains_FKSizeMaster",
                table: "purchaseOrderMains");

            migrationBuilder.DropColumn(
                name: "DeliveryLocation",
                table: "purchaseOrderMains");

            migrationBuilder.DropColumn(
                name: "DeliveryTo",
                table: "purchaseOrderMains");

            migrationBuilder.DropColumn(
                name: "FKDeliveryLocation",
                table: "purchaseOrderMains");

            migrationBuilder.DropColumn(
                name: "FKDeliveryTo",
                table: "purchaseOrderMains");

            migrationBuilder.DropColumn(
                name: "FKModeofTransport",
                table: "purchaseOrderMains");

            migrationBuilder.DropColumn(
                name: "FKSizeMaster",
                table: "purchaseOrderMains");

            migrationBuilder.DropColumn(
                name: "ModeofTransport",
                table: "purchaseOrderMains");

            migrationBuilder.DropColumn(
                name: "PurchaseOrderMainNo",
                table: "purchaseOrderMains");

            migrationBuilder.AlterColumn<string>(
                name: "PurchaseOrderNo",
                table: "purchaseOrders",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(12)",
                oldMaxLength: 12);

            migrationBuilder.AlterColumn<string>(
                name: "PurchaseOrderNo",
                table: "purchaseOrderMains",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(12)",
                oldMaxLength: 12,
                oldNullable: true);
        }
    }
}
