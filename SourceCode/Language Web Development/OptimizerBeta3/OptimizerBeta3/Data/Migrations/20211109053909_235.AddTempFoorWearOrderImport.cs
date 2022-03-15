using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _235AddTempFoorWearOrderImport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TempFootWearOrderImports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SlNo = table.Column<int>(type: "int", nullable: false),
                    SKU = table.Column<string>(type: "varchar(20)", nullable: true),
                    UniqueID = table.Column<string>(type: "varchar(30)", nullable: true),
                    Article = table.Column<string>(type: "varchar(100)", nullable: true),
                    Color = table.Column<string>(type: "varchar(100)", nullable: true),
                    Group = table.Column<string>(type: "varchar(50)", nullable: true),
                    ArticleRef = table.Column<string>(type: "varchar(50)", nullable: true),
                    Size01 = table.Column<int>(type: "int", nullable: false),
                    Size02 = table.Column<int>(type: "int", nullable: false),
                    Size03 = table.Column<int>(type: "int", nullable: false),
                    Size04 = table.Column<int>(type: "int", nullable: false),
                    Size05 = table.Column<int>(type: "int", nullable: false),
                    Size06 = table.Column<int>(type: "int", nullable: false),
                    Size07 = table.Column<int>(type: "int", nullable: false),
                    Size08 = table.Column<int>(type: "int", nullable: false),
                    Size09 = table.Column<int>(type: "int", nullable: false),
                    Size10 = table.Column<int>(type: "int", nullable: false),
                    Size11 = table.Column<int>(type: "int", nullable: false),
                    Size12 = table.Column<int>(type: "int", nullable: false),
                    Size13 = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "varchar(50)", nullable: true),
                    Leather = table.Column<string>(type: "varchar(50)", nullable: true),
                    FACArticleNo = table.Column<string>(type: "varchar(50)", nullable: true),
                    FACColor = table.Column<string>(type: "varchar(50)", nullable: true),
                    BarcodeColor = table.Column<string>(type: "varchar(50)", nullable: true),
                    Lining = table.Column<string>(type: "varchar(50)", nullable: true),
                    Socks = table.Column<string>(type: "varchar(50)", nullable: true),
                    Sole = table.Column<string>(type: "varchar(50)", nullable: true),
                    Last = table.Column<string>(type: "varchar(50)", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remarks1 = table.Column<string>(type: "varchar(50)", nullable: true),
                    Remarks2 = table.Column<string>(type: "varchar(50)", nullable: true),
                    Remarks3 = table.Column<string>(type: "varchar(50)", nullable: true),
                    Remarks4 = table.Column<string>(type: "varchar(50)", nullable: true),
                    Remarks5 = table.Column<string>(type: "varchar(50)", nullable: true),
                    status = table.Column<string>(type: "varchar(20)", nullable: true),
                    IPAddress = table.Column<string>(type: "varchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempFootWearOrderImports", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TempFootWearOrderImports");
        }
    }
}
