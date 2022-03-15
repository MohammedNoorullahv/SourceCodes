using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _238AddTempFootwearOrderForIMport : Migration
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
                    OrderNo = table.Column<string>(type: "varchar(20)", nullable: true),
                    OrderDt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SizeCode = table.Column<string>(type: "varchar(10)", nullable: true),
                    ArticleCode = table.Column<string>(type: "varchar(20)", nullable: true),
                    SKU = table.Column<string>(type: "varchar(20)", nullable: true),
                    Article = table.Column<string>(type: "varchar(100)", nullable: true),
                    Color = table.Column<string>(type: "varchar(100)", nullable: true),
                    Group = table.Column<string>(type: "varchar(50)", nullable: true),
                    Size01 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Size02 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Size03 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Size04 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Size05 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Size06 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Size07 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Size08 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Size09 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Size10 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Size11 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Size12 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Size13 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Size14 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Size15 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Size16 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Size17 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Size18 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity01 = table.Column<int>(type: "int", nullable: false),
                    Quantity02 = table.Column<int>(type: "int", nullable: false),
                    Quantity03 = table.Column<int>(type: "int", nullable: false),
                    Quantity04 = table.Column<int>(type: "int", nullable: false),
                    Quantity05 = table.Column<int>(type: "int", nullable: false),
                    Quantity06 = table.Column<int>(type: "int", nullable: false),
                    Quantity07 = table.Column<int>(type: "int", nullable: false),
                    Quantity08 = table.Column<int>(type: "int", nullable: false),
                    Quantity09 = table.Column<int>(type: "int", nullable: false),
                    Quantity10 = table.Column<int>(type: "int", nullable: false),
                    Quantity11 = table.Column<int>(type: "int", nullable: false),
                    Quantity12 = table.Column<int>(type: "int", nullable: false),
                    Quantity13 = table.Column<int>(type: "int", nullable: false),
                    Quantity14 = table.Column<int>(type: "int", nullable: false),
                    Quantity15 = table.Column<int>(type: "int", nullable: false),
                    Quantity16 = table.Column<int>(type: "int", nullable: false),
                    Quantity17 = table.Column<int>(type: "int", nullable: false),
                    Quantity18 = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false),
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
