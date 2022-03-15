using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _153UpdateSalesPromotion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OfferName",
                table: "SalesPromotionOffers",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountValue",
                table: "SalesPromotionOffers",
                type: "Decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "FKOffer",
                table: "SalesPromotionOffers",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesPromotionOffers_lookUpMasters_LookUpOfferId",
                table: "SalesPromotionOffers");

            migrationBuilder.DropIndex(
                name: "IX_SalesPromotionOffers_LookUpOfferId",
                table: "SalesPromotionOffers");

            migrationBuilder.DropColumn(
                name: "DiscountValue",
                table: "SalesPromotionOffers");

            migrationBuilder.DropColumn(
                name: "FKOffer",
                table: "SalesPromotionOffers");

            migrationBuilder.DropColumn(
                name: "LookUpOfferId",
                table: "SalesPromotionOffers");

            migrationBuilder.AlterColumn<string>(
                name: "OfferName",
                table: "SalesPromotionOffers",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30,
                oldNullable: true);
        }
    }
}
