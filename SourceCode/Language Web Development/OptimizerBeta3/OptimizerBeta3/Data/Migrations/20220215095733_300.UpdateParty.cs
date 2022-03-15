using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _300UpdateParty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GeneralDeliveryDays",
                table: "partyInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NewOrderDeliveryDays",
                table: "partyInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReplinishmentOrderDeliveryDays",
                table: "partyInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GeneralDeliveryDays",
                table: "partyInfos");

            migrationBuilder.DropColumn(
                name: "NewOrderDeliveryDays",
                table: "partyInfos");

            migrationBuilder.DropColumn(
                name: "ReplinishmentOrderDeliveryDays",
                table: "partyInfos");
        }
    }
}
