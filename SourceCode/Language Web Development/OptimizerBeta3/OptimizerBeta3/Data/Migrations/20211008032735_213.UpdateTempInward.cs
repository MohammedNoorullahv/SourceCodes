using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _213UpdateTempInward : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FKPurchaseOrder",
                table: "TempInwardDtls",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKPurchaseOrderDtl",
                table: "TempInwardDtls",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKPurchaseOrderMain",
                table: "TempInwardDtls",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FKPurchaseOrder",
                table: "TempInwardDtls");

            migrationBuilder.DropColumn(
                name: "FKPurchaseOrderDtl",
                table: "TempInwardDtls");

            migrationBuilder.DropColumn(
                name: "FKPurchaseOrderMain",
                table: "TempInwardDtls");
        }
    }
}
