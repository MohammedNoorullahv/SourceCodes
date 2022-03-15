using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _181UpdateLGPODetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PurchaseOrderNo",
                table: "LGPurchaseOrderDetails",
                type: "varchar(16)",
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PurchaseOrderMainNo",
                table: "LGPurchaseOrderDetails",
                type: "varchar(19)",
                maxLength: 19,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(18)",
                oldMaxLength: 18,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PurchaseOrderDtlNo",
                table: "LGPurchaseOrderDetails",
                type: "varchar(23)",
                maxLength: 23,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(22)",
                oldMaxLength: 22,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PurchaseOrderNo",
                table: "LGPurchaseOrderDetails",
                type: "varchar(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(16)",
                oldMaxLength: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PurchaseOrderMainNo",
                table: "LGPurchaseOrderDetails",
                type: "varchar(18)",
                maxLength: 18,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(19)",
                oldMaxLength: 19,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PurchaseOrderDtlNo",
                table: "LGPurchaseOrderDetails",
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
