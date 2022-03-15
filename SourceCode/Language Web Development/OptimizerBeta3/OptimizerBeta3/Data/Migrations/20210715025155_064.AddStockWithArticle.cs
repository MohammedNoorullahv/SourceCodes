using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _064AddStockWithArticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "stockWithArticles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockNo = table.Column<string>(type: "varchar(15)", nullable: true),
                    Brand = table.Column<string>(type: "varchar(30)", nullable: true),
                    Product = table.Column<string>(type: "varchar(30)", nullable: true),
                    ArticleNo = table.Column<string>(type: "varchar(30)", nullable: true),
                    ArticleDescription = table.Column<string>(type: "varchar(60)", nullable: true),
                    Variant = table.Column<int>(type: "int", nullable: false),
                    ColorDescription = table.Column<string>(type: "varchar(30)", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemDescription = table.Column<string>(type: "varchar(60)", nullable: true),
                    ArticleName = table.Column<string>(type: "varchar(30)", nullable: true),
                    LeatherType = table.Column<string>(type: "varchar(30)", nullable: true),
                    Group = table.Column<string>(type: "varchar(30)", nullable: true),
                    Dept = table.Column<string>(type: "varchar(30)", nullable: true),
                    EANCode = table.Column<string>(type: "varchar(30)", nullable: true),
                    Type = table.Column<string>(type: "varchar(30)", nullable: true),
                    Category = table.Column<string>(type: "varchar(30)", nullable: true),
                    Vendor = table.Column<string>(type: "varchar(30)", nullable: true),
                    DOM = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SICM = table.Column<string>(type: "varchar(30)", nullable: true),
                    MRP = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    DealerPrice = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    CostPrice = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    ProductTax = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stockWithArticles", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "stockWithArticles");
        }
    }
}
