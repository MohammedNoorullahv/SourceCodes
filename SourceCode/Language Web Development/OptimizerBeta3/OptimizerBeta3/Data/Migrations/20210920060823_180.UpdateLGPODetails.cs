using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _180UpdateLGPODetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FKSize",
                table: "LGPurchaseOrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "LGPurchaseOrderDetails",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LGPurchaseOrderDetails_FKSize",
                table: "LGPurchaseOrderDetails",
                column: "FKSize");

            migrationBuilder.AddForeignKey(
                name: "FK_LGPurchaseOrderDetails_SizeMasterforLeatherGoods_FKSize",
                table: "LGPurchaseOrderDetails",
                column: "FKSize",
                principalTable: "SizeMasterforLeatherGoods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LGPurchaseOrderDetails_SizeMasterforLeatherGoods_FKSize",
                table: "LGPurchaseOrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_LGPurchaseOrderDetails_FKSize",
                table: "LGPurchaseOrderDetails");

            migrationBuilder.DropColumn(
                name: "FKSize",
                table: "LGPurchaseOrderDetails");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "LGPurchaseOrderDetails");
        }
    }
}
