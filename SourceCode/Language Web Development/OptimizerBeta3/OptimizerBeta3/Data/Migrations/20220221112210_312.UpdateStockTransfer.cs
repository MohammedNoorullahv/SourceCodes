using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _312UpdateStockTransfer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FKQuality",
                table: "StockTransfers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Quality",
                table: "StockTransfers",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FKQuality",
                table: "StockTransferDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Quality",
                table: "StockTransferDetails",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FKQuality",
                table: "StockTransfers");

            migrationBuilder.DropColumn(
                name: "Quality",
                table: "StockTransfers");

            migrationBuilder.DropColumn(
                name: "FKQuality",
                table: "StockTransferDetails");

            migrationBuilder.DropColumn(
                name: "Quality",
                table: "StockTransferDetails");
        }
    }
}
