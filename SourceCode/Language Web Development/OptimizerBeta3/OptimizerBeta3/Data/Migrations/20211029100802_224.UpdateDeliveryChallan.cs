using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _224UpdateDeliveryChallan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryChallans_lookUpMasters_LookUpMasterInvoiceCategoryId",
                table: "DeliveryChallans");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryChallans_LookUpMasterInvoiceCategoryId",
                table: "DeliveryChallans");

            migrationBuilder.DropColumn(
                name: "LookUpMasterInvoiceCategoryId",
                table: "DeliveryChallans");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LookUpMasterInvoiceCategoryId",
                table: "DeliveryChallans",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryChallans_LookUpMasterInvoiceCategoryId",
                table: "DeliveryChallans",
                column: "LookUpMasterInvoiceCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryChallans_lookUpMasters_LookUpMasterInvoiceCategoryId",
                table: "DeliveryChallans",
                column: "LookUpMasterInvoiceCategoryId",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
