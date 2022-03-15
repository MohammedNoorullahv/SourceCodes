using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _170UpdatePODetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_purchaseOrderDetails_FKUOM",
                table: "purchaseOrderDetails",
                column: "FKUOM");

            migrationBuilder.AddForeignKey(
                name: "FK_purchaseOrderDetails_articleDetails_FKUOM",
                table: "purchaseOrderDetails",
                column: "FKUOM",
                principalTable: "articleDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_purchaseOrderDetails_articleDetails_FKUOM",
                table: "purchaseOrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_purchaseOrderDetails_FKUOM",
                table: "purchaseOrderDetails");
        }
    }
}
