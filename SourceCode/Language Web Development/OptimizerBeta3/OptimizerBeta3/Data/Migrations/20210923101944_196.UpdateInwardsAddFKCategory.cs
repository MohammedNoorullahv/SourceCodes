using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _196UpdateInwardsAddFKCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PurchaseOrderDtlNo",
                table: "purchaseOrderDetails",
                type: "varchar(23)",
                maxLength: 23,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(22)",
                oldMaxLength: 22,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_inwards_FKCategory",
                table: "inwards",
                column: "FKCategory");

            migrationBuilder.AddForeignKey(
                name: "FK_inwards_lookUpMasters_FKCategory",
                table: "inwards",
                column: "FKCategory",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inwards_lookUpMasters_FKCategory",
                table: "inwards");

            migrationBuilder.DropIndex(
                name: "IX_inwards_FKCategory",
                table: "inwards");

            migrationBuilder.AlterColumn<string>(
                name: "PurchaseOrderDtlNo",
                table: "purchaseOrderDetails",
                type: "varchar(22)",
                maxLength: 22,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(23)",
                oldMaxLength: 23,
                oldNullable: true);
        }
    }
}
