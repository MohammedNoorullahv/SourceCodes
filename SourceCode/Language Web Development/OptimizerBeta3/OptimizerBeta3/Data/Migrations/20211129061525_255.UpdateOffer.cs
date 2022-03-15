using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _255UpdateOffer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_offers_FKOfferType",
                table: "offers",
                column: "FKOfferType");

            migrationBuilder.AddForeignKey(
                name: "FK_offers_lookUpMasters_FKOfferType",
                table: "offers",
                column: "FKOfferType",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_offers_lookUpMasters_FKOfferType",
                table: "offers");

            migrationBuilder.DropIndex(
                name: "IX_offers_FKOfferType",
                table: "offers");
        }
    }
}
