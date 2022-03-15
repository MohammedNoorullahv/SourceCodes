using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _158AddLeatherGoodsGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeatherGoodsGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    FKBrand = table.Column<int>(type: "int", nullable: false),
                    Brand = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    Product = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    ArticleNo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    ArticleName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    FKGroup = table.Column<int>(type: "int", nullable: false),
                    LGGroupName = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    VersionNo = table.Column<int>(type: "int", nullable: false),
                    FKCategory = table.Column<int>(type: "int", nullable: false),
                    FKProduct = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_LeatherGoodsGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeatherGoodsGroups_lookUpMasters_FKBrand",
                        column: x => x.FKBrand,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LeatherGoodsGroups_lookUpMasters_FKCategory",
                        column: x => x.FKCategory,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LeatherGoodsGroups_lookUpMasters_FKGroup",
                        column: x => x.FKGroup,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LeatherGoodsGroups_lookUpMasters_FKProduct",
                        column: x => x.FKProduct,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeatherGoodsGroups_FKBrand",
                table: "LeatherGoodsGroups",
                column: "FKBrand");

            migrationBuilder.CreateIndex(
                name: "IX_LeatherGoodsGroups_FKCategory",
                table: "LeatherGoodsGroups",
                column: "FKCategory");

            migrationBuilder.CreateIndex(
                name: "IX_LeatherGoodsGroups_FKGroup",
                table: "LeatherGoodsGroups",
                column: "FKGroup");

            migrationBuilder.CreateIndex(
                name: "IX_LeatherGoodsGroups_FKProduct",
                table: "LeatherGoodsGroups",
                column: "FKProduct");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeatherGoodsGroups");
        }
    }
}
