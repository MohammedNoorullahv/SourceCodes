using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _205UpdateInwardAddDecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaterialorFinishedProduct",
                table: "inwardDetails");

            migrationBuilder.AlterColumn<decimal>(
                name: "DocumentQuantity",
                table: "inwards",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "ArrivedQuantity",
                table: "inwards",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "FKPurchaseOrder",
                table: "inwardDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKPurchaseOrderDetail",
                table: "inwardDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKPurchaseOrderMain",
                table: "inwardDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FLAM",
                table: "inwardDetails",
                type: "varchar(1)",
                maxLength: 1,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FKPurchaseOrder",
                table: "inwardDetails");

            migrationBuilder.DropColumn(
                name: "FKPurchaseOrderDetail",
                table: "inwardDetails");

            migrationBuilder.DropColumn(
                name: "FKPurchaseOrderMain",
                table: "inwardDetails");

            migrationBuilder.DropColumn(
                name: "FLAM",
                table: "inwardDetails");

            migrationBuilder.AlterColumn<int>(
                name: "DocumentQuantity",
                table: "inwards",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "ArrivedQuantity",
                table: "inwards",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "MaterialorFinishedProduct",
                table: "inwardDetails",
                type: "varchar(2)",
                maxLength: 2,
                nullable: true);
        }
    }
}
