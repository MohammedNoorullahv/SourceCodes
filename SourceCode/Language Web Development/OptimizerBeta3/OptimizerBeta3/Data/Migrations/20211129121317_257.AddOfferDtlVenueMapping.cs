using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _257AddOfferDtlVenueMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OfferDtlVenueMappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKOffer = table.Column<int>(type: "int", nullable: false),
                    FKUnitName = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_OfferDtlVenueMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfferDtlVenueMappings_offers_FKOffer",
                        column: x => x.FKOffer,
                        principalTable: "offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_OfferDtlVenueMappings_unitMasters_FKUnitName",
                        column: x => x.FKUnitName,
                        principalTable: "unitMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OfferDtlVenueMappings_FKOffer",
                table: "OfferDtlVenueMappings",
                column: "FKOffer");

            migrationBuilder.CreateIndex(
                name: "IX_OfferDtlVenueMappings_FKUnitName",
                table: "OfferDtlVenueMappings",
                column: "FKUnitName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OfferDtlVenueMappings");
        }
    }
}
