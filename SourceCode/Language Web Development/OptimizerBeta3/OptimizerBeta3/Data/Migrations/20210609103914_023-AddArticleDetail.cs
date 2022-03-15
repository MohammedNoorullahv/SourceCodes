using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _023AddArticleDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "articleDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKArticleGroup = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    FKLeather = table.Column<int>(type: "int", nullable: false),
                    FKColour = table.Column<int>(type: "int", nullable: false),
                    VersionNo = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    AdditionalInfo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
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
                    table.PrimaryKey("PK_articleDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_articleDetails_articleGroups_FKArticleGroup",
                        column: x => x.FKArticleGroup,
                        principalTable: "articleGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_articleDetails_colorMasters_FKColour",
                        column: x => x.FKColour,
                        principalTable: "colorMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_articleDetails_lookUpMasters_FKLeather",
                        column: x => x.FKLeather,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_articleDetails_FKArticleGroup",
                table: "articleDetails",
                column: "FKArticleGroup");

            migrationBuilder.CreateIndex(
                name: "IX_articleDetails_FKColour",
                table: "articleDetails",
                column: "FKColour");

            migrationBuilder.CreateIndex(
                name: "IX_articleDetails_FKLeather",
                table: "articleDetails",
                column: "FKLeather");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "articleDetails");
        }
    }
}
