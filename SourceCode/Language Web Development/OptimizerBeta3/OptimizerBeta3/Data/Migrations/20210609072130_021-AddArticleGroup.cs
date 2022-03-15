using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _021AddArticleGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "articleGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKParty = table.Column<int>(type: "int", nullable: false),
                    FKSeason = table.Column<int>(type: "int", nullable: false),
                    FKArticleType = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "varchar(6)", maxLength: 6, nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    FKArea = table.Column<int>(type: "int", nullable: false),
                    FKBrand = table.Column<int>(type: "int", nullable: false),
                    Product = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    ArticleNo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    ArticleName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    FKGroup = table.Column<int>(type: "int", nullable: false),
                    FKSizeFor = table.Column<int>(type: "int", nullable: false),
                    VersionNo = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_articleGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_articleGroups_lookUpMasters_FKArea",
                        column: x => x.FKArea,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_articleGroups_lookUpMasters_FKArticleType",
                        column: x => x.FKArticleType,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_articleGroups_lookUpMasters_FKBrand",
                        column: x => x.FKBrand,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_articleGroups_lookUpMasters_FKGroup",
                        column: x => x.FKGroup,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_articleGroups_lookUpMasters_FKSizeFor",
                        column: x => x.FKSizeFor,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_articleGroups_partyInfos_FKParty",
                        column: x => x.FKParty,
                        principalTable: "partyInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_articleGroups_seasons_FKSeason",
                        column: x => x.FKSeason,
                        principalTable: "seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_articleGroups_FKArea",
                table: "articleGroups",
                column: "FKArea");

            migrationBuilder.CreateIndex(
                name: "IX_articleGroups_FKArticleType",
                table: "articleGroups",
                column: "FKArticleType");

            migrationBuilder.CreateIndex(
                name: "IX_articleGroups_FKBrand",
                table: "articleGroups",
                column: "FKBrand");

            migrationBuilder.CreateIndex(
                name: "IX_articleGroups_FKGroup",
                table: "articleGroups",
                column: "FKGroup");

            migrationBuilder.CreateIndex(
                name: "IX_articleGroups_FKParty",
                table: "articleGroups",
                column: "FKParty");

            migrationBuilder.CreateIndex(
                name: "IX_articleGroups_FKSeason",
                table: "articleGroups",
                column: "FKSeason");

            migrationBuilder.CreateIndex(
                name: "IX_articleGroups_FKSizeFor",
                table: "articleGroups",
                column: "FKSizeFor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "articleGroups");
        }
    }
}
