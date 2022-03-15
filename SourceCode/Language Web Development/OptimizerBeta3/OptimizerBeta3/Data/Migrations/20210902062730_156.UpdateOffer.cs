using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _156UpdateOffer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OfferonPrsCount",
                table: "offers",
                newName: "OfferPair");

            migrationBuilder.AddColumn<int>(
                name: "BuyPair",
                table: "offers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsProductCompliment",
                table: "offers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyPair",
                table: "offers");

            migrationBuilder.DropColumn(
                name: "IsProductCompliment",
                table: "offers");

            migrationBuilder.RenameColumn(
                name: "OfferPair",
                table: "offers",
                newName: "OfferonPrsCount");
        }
    }
}
