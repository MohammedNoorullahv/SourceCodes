using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _079AddHSNCodeAndUpdateStockWithArticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FKArticleDetailId",
                table: "stockWithArticles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKOrderDetailId",
                table: "stockWithArticles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "HSNCodeMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HSNCode = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    FKPercentageType = table.Column<int>(type: "int", nullable: false),
                    PercentageType = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    GSTPercentage = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_HSNCodeMasters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HSNCodeMasters_lookUpMasters_FKPercentageType",
                        column: x => x.FKPercentageType,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HSNCodeMasters_FKPercentageType",
                table: "HSNCodeMasters",
                column: "FKPercentageType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HSNCodeMasters");

            migrationBuilder.DropColumn(
                name: "FKArticleDetailId",
                table: "stockWithArticles");

            migrationBuilder.DropColumn(
                name: "FKOrderDetailId",
                table: "stockWithArticles");
        }
    }
}
