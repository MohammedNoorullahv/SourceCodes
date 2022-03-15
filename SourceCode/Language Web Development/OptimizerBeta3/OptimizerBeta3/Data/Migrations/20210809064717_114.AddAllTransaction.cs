using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _114AddAllTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AllTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionType = table.Column<string>(type: "varchar(5)", nullable: true),
                    TranId = table.Column<int>(type: "int", nullable: false),
                    TranRefNo = table.Column<string>(type: "varchar(20)", nullable: true),
                    TranDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaterialorFinishedProduct = table.Column<string>(type: "varchar(2)", nullable: true),
                    FKMaterial = table.Column<int>(type: "int", nullable: false),
                    FKArticle = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(50)", nullable: true),
                    Colour = table.Column<string>(type: "Varchar(20)", nullable: true),
                    Size = table.Column<string>(type: "Varchar(5)", nullable: true),
                    FKFromUnit = table.Column<int>(type: "int", nullable: false),
                    FKFromLocation = table.Column<int>(type: "int", nullable: false),
                    FKFromStage = table.Column<int>(type: "int", nullable: false),
                    FKToUnit = table.Column<int>(type: "int", nullable: false),
                    FKToLocation = table.Column<int>(type: "int", nullable: false),
                    FKToStage = table.Column<int>(type: "int", nullable: false),
                    FKQuality = table.Column<int>(type: "int", nullable: false),
                    HSNCode = table.Column<int>(type: "int", nullable: false),
                    FKUOM = table.Column<int>(type: "int", nullable: false),
                    InwardQuantity = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    OutwardQuantity = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    BalanceQuantity = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    Rate = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    Value = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    FKStatus = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EnteredSystemId = table.Column<string>(type: "varchar(30)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteBy = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllTransactions_lookUpMasters_FKFromStage",
                        column: x => x.FKFromStage,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AllTransactions_lookUpMasters_FKQuality",
                        column: x => x.FKQuality,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AllTransactions_lookUpMasters_FKStatus",
                        column: x => x.FKStatus,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AllTransactions_lookUpMasters_FKToStage",
                        column: x => x.FKToStage,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AllTransactions_lookUpMasters_FKUOM",
                        column: x => x.FKUOM,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllTransactions_FKFromStage",
                table: "AllTransactions",
                column: "FKFromStage");

            migrationBuilder.CreateIndex(
                name: "IX_AllTransactions_FKQuality",
                table: "AllTransactions",
                column: "FKQuality");

            migrationBuilder.CreateIndex(
                name: "IX_AllTransactions_FKStatus",
                table: "AllTransactions",
                column: "FKStatus");

            migrationBuilder.CreateIndex(
                name: "IX_AllTransactions_FKToStage",
                table: "AllTransactions",
                column: "FKToStage");

            migrationBuilder.CreateIndex(
                name: "IX_AllTransactions_FKUOM",
                table: "AllTransactions",
                column: "FKUOM");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllTransactions");
        }
    }
}
