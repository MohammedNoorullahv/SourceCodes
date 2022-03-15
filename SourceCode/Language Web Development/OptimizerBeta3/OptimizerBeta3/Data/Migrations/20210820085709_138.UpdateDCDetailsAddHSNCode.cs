using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _138UpdateDCDetailsAddHSNCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FKHSNCode",
                table: "DeliveryChallanDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryChallanDetails_FKHSNCode",
                table: "DeliveryChallanDetails",
                column: "FKHSNCode");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryChallanDetails_HSNCodeMasters_FKHSNCode",
                table: "DeliveryChallanDetails",
                column: "FKHSNCode",
                principalTable: "HSNCodeMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryChallanDetails_HSNCodeMasters_FKHSNCode",
                table: "DeliveryChallanDetails");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryChallanDetails_FKHSNCode",
                table: "DeliveryChallanDetails");

            migrationBuilder.DropColumn(
                name: "FKHSNCode",
                table: "DeliveryChallanDetails");
        }
    }
}
