using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _314UpdateAreaLookUpMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FKQuality",
                table: "TempTransferDtls",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "SetAsDefault",
                table: "AreaLookUpMasters",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FKQuality",
                table: "TempTransferDtls");

            migrationBuilder.DropColumn(
                name: "SetAsDefault",
                table: "AreaLookUpMasters");
        }
    }
}
