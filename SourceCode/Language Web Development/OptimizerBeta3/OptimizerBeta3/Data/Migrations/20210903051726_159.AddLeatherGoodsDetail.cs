using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _159AddLeatherGoodsDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "leatherGoodsDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKArticleGroup = table.Column<int>(type: "int", nullable: false),
                    StockNo = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    FKLeather = table.Column<int>(type: "int", nullable: false),
                    Leather = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    FKColour = table.Column<int>(type: "int", nullable: false),
                    ColorDescription = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    VersionNo = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    AdditionalInfo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Variant = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true),
                    ArticleImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Picture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    MRP = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    DealerPrice = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    CostPrice = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    ProductTax = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    FKEntryType = table.Column<int>(type: "int", nullable: false),
                    EntryType = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    FKCategory = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    FKFeatures = table.Column<int>(type: "int", nullable: false),
                    Features = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    ArticleNo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Type = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    FKHSNCode = table.Column<int>(type: "int", nullable: false),
                    HSNCode = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_leatherGoodsDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_leatherGoodsDetails_colorMasters_FKColour",
                        column: x => x.FKColour,
                        principalTable: "colorMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_leatherGoodsDetails_HSNCodeMasters_FKHSNCode",
                        column: x => x.FKHSNCode,
                        principalTable: "HSNCodeMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_leatherGoodsDetails_LeatherGoodsGroups_FKArticleGroup",
                        column: x => x.FKArticleGroup,
                        principalTable: "LeatherGoodsGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_leatherGoodsDetails_lookUpMasters_FKCategory",
                        column: x => x.FKCategory,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_leatherGoodsDetails_lookUpMasters_FKEntryType",
                        column: x => x.FKEntryType,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_leatherGoodsDetails_lookUpMasters_FKFeatures",
                        column: x => x.FKFeatures,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_leatherGoodsDetails_lookUpMasters_FKLeather",
                        column: x => x.FKLeather,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_leatherGoodsDetails_FKArticleGroup",
                table: "leatherGoodsDetails",
                column: "FKArticleGroup");

            migrationBuilder.CreateIndex(
                name: "IX_leatherGoodsDetails_FKCategory",
                table: "leatherGoodsDetails",
                column: "FKCategory");

            migrationBuilder.CreateIndex(
                name: "IX_leatherGoodsDetails_FKColour",
                table: "leatherGoodsDetails",
                column: "FKColour");

            migrationBuilder.CreateIndex(
                name: "IX_leatherGoodsDetails_FKEntryType",
                table: "leatherGoodsDetails",
                column: "FKEntryType");

            migrationBuilder.CreateIndex(
                name: "IX_leatherGoodsDetails_FKFeatures",
                table: "leatherGoodsDetails",
                column: "FKFeatures");

            migrationBuilder.CreateIndex(
                name: "IX_leatherGoodsDetails_FKHSNCode",
                table: "leatherGoodsDetails",
                column: "FKHSNCode");

            migrationBuilder.CreateIndex(
                name: "IX_leatherGoodsDetails_FKLeather",
                table: "leatherGoodsDetails",
                column: "FKLeather");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "leatherGoodsDetails");
        }
    }
}
