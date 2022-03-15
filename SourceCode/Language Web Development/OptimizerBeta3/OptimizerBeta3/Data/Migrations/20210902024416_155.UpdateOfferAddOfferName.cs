using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _155UpdateOfferAddOfferName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OfferName",
                table: "offers",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountValue",
                table: "offers",
                type: "Decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "FKOffer",
                table: "offers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKOfferCategory",
                table: "offers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OfferCategory",
                table: "offers",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OfferonPrsCount",
                table: "offers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_offers_FKOffer",
                table: "offers",
                column: "FKOffer");

            migrationBuilder.CreateIndex(
                name: "IX_offers_FKOfferCategory",
                table: "offers",
                column: "FKOfferCategory");

            migrationBuilder.AddForeignKey(
                name: "FK_offers_lookUpMasters_FKOffer",
                table: "offers",
                column: "FKOffer",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_offers_lookUpMasters_FKOfferCategory",
                table: "offers",
                column: "FKOfferCategory",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_offers_lookUpMasters_FKOffer",
                table: "offers");

            migrationBuilder.DropForeignKey(
                name: "FK_offers_lookUpMasters_FKOfferCategory",
                table: "offers");

            migrationBuilder.DropIndex(
                name: "IX_offers_FKOffer",
                table: "offers");

            migrationBuilder.DropIndex(
                name: "IX_offers_FKOfferCategory",
                table: "offers");

            migrationBuilder.DropColumn(
                name: "DiscountValue",
                table: "offers");

            migrationBuilder.DropColumn(
                name: "FKOffer",
                table: "offers");

            migrationBuilder.DropColumn(
                name: "FKOfferCategory",
                table: "offers");

            migrationBuilder.DropColumn(
                name: "OfferCategory",
                table: "offers");

            migrationBuilder.DropColumn(
                name: "OfferonPrsCount",
                table: "offers");

            migrationBuilder.AlterColumn<string>(
                name: "OfferName",
                table: "offers",
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
