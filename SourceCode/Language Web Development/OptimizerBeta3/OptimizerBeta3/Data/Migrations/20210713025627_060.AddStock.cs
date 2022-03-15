using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _060AddStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "stocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialorFinishedProduct = table.Column<string>(type: "varchar(1)", nullable: true),
                    FKMaterial = table.Column<int>(type: "int", nullable: false),
                    FKArticleDetail = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(50)", nullable: true),
                    Colour = table.Column<string>(type: "varchar(50)", nullable: true),
                    Size = table.Column<string>(type: "varchar(5)", nullable: true),
                    OrderReferenceNo = table.Column<string>(type: "varchar(20)", nullable: true),
                    StockInitiatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FKUnit = table.Column<int>(type: "int", nullable: false),
                    FKLocation = table.Column<int>(type: "int", nullable: false),
                    FKStage = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    Rate = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    Value = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    FKCurrency = table.Column<int>(type: "int", nullable: false),
                    ExchangeRate = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    ValueInINR = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    LandedCost = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    LandedRate = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    FKUOM = table.Column<int>(type: "int", nullable: false),
                    FKIIUOM = table.Column<int>(type: "int", nullable: false),
                    FKSource = table.Column<int>(type: "int", nullable: false),
                    FKQuality = table.Column<int>(type: "int", nullable: false),
                    FKStatus = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EnteredSystemId = table.Column<string>(type: "varchar(30)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteBy = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_stocks_lookUpMasters_FKCurrency",
                        column: x => x.FKCurrency,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_stocks_lookUpMasters_FKIIUOM",
                        column: x => x.FKIIUOM,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_stocks_lookUpMasters_FKLocation",
                        column: x => x.FKLocation,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_stocks_lookUpMasters_FKQuality",
                        column: x => x.FKQuality,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_stocks_lookUpMasters_FKSource",
                        column: x => x.FKSource,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_stocks_lookUpMasters_FKStage",
                        column: x => x.FKStage,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_stocks_lookUpMasters_FKStatus",
                        column: x => x.FKStatus,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_stocks_lookUpMasters_FKUOM",
                        column: x => x.FKUOM,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_stocks_unitMasters_FKUnit",
                        column: x => x.FKUnit,
                        principalTable: "unitMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_stocks_FKCurrency",
                table: "stocks",
                column: "FKCurrency");

            migrationBuilder.CreateIndex(
                name: "IX_stocks_FKIIUOM",
                table: "stocks",
                column: "FKIIUOM");

            migrationBuilder.CreateIndex(
                name: "IX_stocks_FKLocation",
                table: "stocks",
                column: "FKLocation");

            migrationBuilder.CreateIndex(
                name: "IX_stocks_FKQuality",
                table: "stocks",
                column: "FKQuality");

            migrationBuilder.CreateIndex(
                name: "IX_stocks_FKSource",
                table: "stocks",
                column: "FKSource");

            migrationBuilder.CreateIndex(
                name: "IX_stocks_FKStage",
                table: "stocks",
                column: "FKStage");

            migrationBuilder.CreateIndex(
                name: "IX_stocks_FKStatus",
                table: "stocks",
                column: "FKStatus");

            migrationBuilder.CreateIndex(
                name: "IX_stocks_FKUnit",
                table: "stocks",
                column: "FKUnit");

            migrationBuilder.CreateIndex(
                name: "IX_stocks_FKUOM",
                table: "stocks",
                column: "FKUOM");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "stocks");
        }
    }
}
