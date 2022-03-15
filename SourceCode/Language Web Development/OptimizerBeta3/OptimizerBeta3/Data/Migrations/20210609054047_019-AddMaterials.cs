using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _019AddMaterials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customerPersonInfoDtls_partyInfos_FKPartyInfo",
                table: "customerPersonInfoDtls");

            migrationBuilder.DropIndex(
                name: "IX_customerPersonInfoDtls_FKPartyInfo",
                table: "customerPersonInfoDtls");

            migrationBuilder.CreateTable(
                name: "materials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKCategory = table.Column<int>(type: "int", nullable: false),
                    FKType = table.Column<int>(type: "int", nullable: false),
                    FKSubType = table.Column<int>(type: "int", nullable: false),
                    FKBrand = table.Column<int>(type: "int", nullable: false),
                    FKSource = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    ShortDescription = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    PrintDescription = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    FKUom = table.Column<int>(type: "int", nullable: false),
                    FKIIUom = table.Column<int>(type: "int", nullable: false),
                    IsExpirable = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_materials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_materials_lookUpMasters_FKBrand",
                        column: x => x.FKBrand,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_materials_lookUpMasters_FKCategory",
                        column: x => x.FKCategory,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_materials_lookUpMasters_FKIIUom",
                        column: x => x.FKIIUom,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_materials_lookUpMasters_FKSource",
                        column: x => x.FKSource,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_materials_lookUpMasters_FKSubType",
                        column: x => x.FKSubType,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_materials_lookUpMasters_FKType",
                        column: x => x.FKType,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_materials_lookUpMasters_FKUom",
                        column: x => x.FKUom,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_materials_FKBrand",
                table: "materials",
                column: "FKBrand");

            migrationBuilder.CreateIndex(
                name: "IX_materials_FKCategory",
                table: "materials",
                column: "FKCategory");

            migrationBuilder.CreateIndex(
                name: "IX_materials_FKIIUom",
                table: "materials",
                column: "FKIIUom");

            migrationBuilder.CreateIndex(
                name: "IX_materials_FKSource",
                table: "materials",
                column: "FKSource");

            migrationBuilder.CreateIndex(
                name: "IX_materials_FKSubType",
                table: "materials",
                column: "FKSubType");

            migrationBuilder.CreateIndex(
                name: "IX_materials_FKType",
                table: "materials",
                column: "FKType");

            migrationBuilder.CreateIndex(
                name: "IX_materials_FKUom",
                table: "materials",
                column: "FKUom");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "materials");

            migrationBuilder.CreateIndex(
                name: "IX_customerPersonInfoDtls_FKPartyInfo",
                table: "customerPersonInfoDtls",
                column: "FKPartyInfo");

            migrationBuilder.AddForeignKey(
                name: "FK_customerPersonInfoDtls_partyInfos_FKPartyInfo",
                table: "customerPersonInfoDtls",
                column: "FKPartyInfo",
                principalTable: "partyInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
