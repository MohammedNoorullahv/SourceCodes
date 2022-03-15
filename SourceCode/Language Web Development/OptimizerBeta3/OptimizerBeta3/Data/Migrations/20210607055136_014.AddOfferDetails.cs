using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _014AddOfferDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_partyInfoDtls_partyInfos_FKPartyInfo",
                table: "partyInfoDtls");

            migrationBuilder.DropIndex(
                name: "IX_partyInfoDtls_FKPartyInfo",
                table: "partyInfoDtls");

            migrationBuilder.CreateTable(
                name: "offerDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKOffer = table.Column<int>(type: "int", nullable: false),
                    FKAnniverseryInfo = table.Column<int>(type: "int", nullable: false),
                    RangeSubSlNo = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    ValueFrom = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    ValueTo = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EnteredSystemId = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteBy = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_offerDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_offerDetails_lookUpMasters_FKAnniverseryInfo",
                        column: x => x.FKAnniverseryInfo,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_offerDetails_offers_FKOffer",
                        column: x => x.FKOffer,
                        principalTable: "offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_offerDetails_FKAnniverseryInfo",
                table: "offerDetails",
                column: "FKAnniverseryInfo");

            migrationBuilder.CreateIndex(
                name: "IX_offerDetails_FKOffer",
                table: "offerDetails",
                column: "FKOffer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "offerDetails");

            migrationBuilder.CreateIndex(
                name: "IX_partyInfoDtls_FKPartyInfo",
                table: "partyInfoDtls",
                column: "FKPartyInfo");

            migrationBuilder.AddForeignKey(
                name: "FK_partyInfoDtls_partyInfos_FKPartyInfo",
                table: "partyInfoDtls",
                column: "FKPartyInfo",
                principalTable: "partyInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
