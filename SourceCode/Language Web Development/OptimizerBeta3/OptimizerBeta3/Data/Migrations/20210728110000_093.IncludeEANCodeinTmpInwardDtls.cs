using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _093IncludeEANCodeinTmpInwardDtls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EANCode",
                table: "TempInwardDtls",
                type: "varchar(13)",
                maxLength: 13,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EANCode",
                table: "inwardDetails",
                type: "varchar(13)",
                maxLength: 13,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EANCode",
                table: "TempInwardDtls");

            migrationBuilder.DropColumn(
                name: "EANCode",
                table: "inwardDetails");
        }
    }
}
