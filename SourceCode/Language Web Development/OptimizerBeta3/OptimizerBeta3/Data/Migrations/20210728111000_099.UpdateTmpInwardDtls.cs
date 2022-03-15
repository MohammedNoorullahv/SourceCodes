using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _099UpdateTmpInwardDtls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Size",
                table: "TempInwardDtls",
                type: "Decimal(18,1)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(string),
                oldType: "Varchar(5)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Size",
                table: "TempInwardDtls",
                type: "Varchar(5)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Decimal(18,1)");
        }
    }
}
