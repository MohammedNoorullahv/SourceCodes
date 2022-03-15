using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _259AddOfferDtlStockMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OfferDtlStockMappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKOffer = table.Column<int>(type: "int", nullable: false),
                    FKArticleGroup = table.Column<int>(type: "int", nullable: false),
                    FKArticle = table.Column<int>(type: "int", nullable: false),
                    StockNo = table.Column<string>(type: "varchar(15)", nullable: true),
                    EANCode = table.Column<string>(type: "varchar(30)", nullable: true),
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
                    table.PrimaryKey("PK_OfferDtlStockMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfferDtlStockMappings_offers_FKOffer",
                        column: x => x.FKOffer,
                        principalTable: "offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OfferDtlStockMappings_FKOffer",
                table: "OfferDtlStockMappings",
                column: "FKOffer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OfferDtlStockMappings");
        }
    }
}
