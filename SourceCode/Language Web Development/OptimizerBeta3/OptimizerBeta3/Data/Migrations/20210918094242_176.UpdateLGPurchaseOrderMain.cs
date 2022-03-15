using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _176UpdateLGPurchaseOrderMain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LGPurchaseOrderMains_sizeMasters_FKSizeMaster",
                table: "LGPurchaseOrderMains");

            migrationBuilder.DropIndex(
                name: "IX_LGPurchaseOrderMains_FKSizeMaster",
                table: "LGPurchaseOrderMains");

            migrationBuilder.DropColumn(
                name: "FKSizeMaster",
                table: "LGPurchaseOrderMains");

            migrationBuilder.DropColumn(
                name: "SizeCode",
                table: "LGPurchaseOrderMains");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FKSizeMaster",
                table: "LGPurchaseOrderMains",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SizeCode",
                table: "LGPurchaseOrderMains",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LGPurchaseOrderMains_FKSizeMaster",
                table: "LGPurchaseOrderMains",
                column: "FKSizeMaster");

            migrationBuilder.AddForeignKey(
                name: "FK_LGPurchaseOrderMains_sizeMasters_FKSizeMaster",
                table: "LGPurchaseOrderMains",
                column: "FKSizeMaster",
                principalTable: "sizeMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
