using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _244UpdatePurchaseOrderDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_purchaseOrderDetails_articleDetails_FKUOM",
                table: "purchaseOrderDetails");

            migrationBuilder.DropColumn(
                name: "DeliveryLocation",
                table: "purchaseOrderMains");

            migrationBuilder.DropColumn(
                name: "FKDeliveryLocation",
                table: "purchaseOrderMains");

            migrationBuilder.AddForeignKey(
                name: "FK_purchaseOrderDetails_lookUpMasters_FKUOM",
                table: "purchaseOrderDetails",
                column: "FKUOM",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_purchaseOrderDetails_lookUpMasters_FKUOM",
                table: "purchaseOrderDetails");

            migrationBuilder.AddColumn<string>(
                name: "DeliveryLocation",
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

            migrationBuilder.AddForeignKey(
                name: "FK_purchaseOrderDetails_articleDetails_FKUOM",
                table: "purchaseOrderDetails",
                column: "FKUOM",
                principalTable: "articleDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
