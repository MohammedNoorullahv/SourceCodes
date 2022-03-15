using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _055UpdateTotalOrderQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnteredQuantity",
                table: "purchaseOrderDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EnteredQuantity",
                table: "purchaseOrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
