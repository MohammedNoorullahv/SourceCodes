using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _154UpdateSalesPromotion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesPromotionOffers_lookUpMasters_LookUpOfferId",
                table: "SalesPromotionOffers");

            migrationBuilder.DropIndex(
                name: "IX_SalesPromotionOffers_LookUpOfferId",
                table: "SalesPromotionOffers");

            migrationBuilder.DropColumn(
                name: "LookUpOfferId",
                table: "SalesPromotionOffers");

            migrationBuilder.CreateIndex(
                name: "IX_SalesPromotionOffers_FKOffer",
                table: "SalesPromotionOffers",
                column: "FKOffer");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesPromotionOffers_lookUpMasters_FKOffer",
                table: "SalesPromotionOffers",
                column: "FKOffer",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesPromotionOffers_lookUpMasters_FKOffer",
                table: "SalesPromotionOffers");

            migrationBuilder.DropIndex(
                name: "IX_SalesPromotionOffers_FKOffer",
                table: "SalesPromotionOffers");

            migrationBuilder.AddColumn<int>(
                name: "LookUpOfferId",
                table: "SalesPromotionOffers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesPromotionOffers_LookUpOfferId",
                table: "SalesPromotionOffers",
                column: "LookUpOfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesPromotionOffers_lookUpMasters_LookUpOfferId",
                table: "SalesPromotionOffers",
                column: "LookUpOfferId",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
