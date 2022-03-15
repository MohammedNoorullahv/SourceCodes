using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _076AddInwardDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "inwardDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKInwardNo = table.Column<int>(type: "int", nullable: false),
                    FKMaterial = table.Column<int>(type: "int", nullable: false),
                    FKArticle = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "Varchar(50)", nullable: true),
                    Colour = table.Column<string>(type: "Varchar(20)", nullable: true),
                    Size = table.Column<string>(type: "Varchar(5)", nullable: true),
                    HSNCode = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    FKUOM = table.Column<int>(type: "int", nullable: false),
                    IIQuantity = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    FKIIUom = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    Value = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    ValueinINR = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    DiscountValue = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    GrossValue = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    SGSTPercentage = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    SGSTValue = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    CGSTPercentage = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    CGSTValue = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    IGSTPercentage = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    IGSTValue = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    GSTTotalValue = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    OthersValuePlus = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    ItemNettValue = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    IsEntryCompleted = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_inwardDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_inwardDetails_inwards_FKInwardNo",
                        column: x => x.FKInwardNo,
                        principalTable: "inwards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_inwardDetails_lookUpMasters_FKIIUom",
                        column: x => x.FKIIUom,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_inwardDetails_lookUpMasters_FKUOM",
                        column: x => x.FKUOM,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_inwardDetails_FKIIUom",
                table: "inwardDetails",
                column: "FKIIUom");

            migrationBuilder.CreateIndex(
                name: "IX_inwardDetails_FKInwardNo",
                table: "inwardDetails",
                column: "FKInwardNo");

            migrationBuilder.CreateIndex(
                name: "IX_inwardDetails_FKUOM",
                table: "inwardDetails",
                column: "FKUOM");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "inwardDetails");
        }
    }
}
