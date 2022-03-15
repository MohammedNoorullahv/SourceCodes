using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _040UpdatePurchaseOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_purchaseOrders_lookUpMasters_FKDepartment",
                table: "purchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_purchaseOrders_lookUpMasters_FKPOType",
                table: "purchaseOrders");

            migrationBuilder.DropIndex(
                name: "IX_purchaseOrders_FKDepartment",
                table: "purchaseOrders");

            migrationBuilder.DropIndex(
                name: "IX_purchaseOrders_FKPOType",
                table: "purchaseOrders");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "purchaseOrders");

            migrationBuilder.DropColumn(
                name: "FKDepartment",
                table: "purchaseOrders");

            migrationBuilder.DropColumn(
                name: "FKPOType",
                table: "purchaseOrders");

            migrationBuilder.DropColumn(
                name: "POType",
                table: "purchaseOrders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "purchaseOrders",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FKDepartment",
                table: "purchaseOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKPOType",
                table: "purchaseOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "POType",
                table: "purchaseOrders",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_purchaseOrders_FKDepartment",
                table: "purchaseOrders",
                column: "FKDepartment");

            migrationBuilder.CreateIndex(
                name: "IX_purchaseOrders_FKPOType",
                table: "purchaseOrders",
                column: "FKPOType");

            migrationBuilder.AddForeignKey(
                name: "FK_purchaseOrders_lookUpMasters_FKDepartment",
                table: "purchaseOrders",
                column: "FKDepartment",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_purchaseOrders_lookUpMasters_FKPOType",
                table: "purchaseOrders",
                column: "FKPOType",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
