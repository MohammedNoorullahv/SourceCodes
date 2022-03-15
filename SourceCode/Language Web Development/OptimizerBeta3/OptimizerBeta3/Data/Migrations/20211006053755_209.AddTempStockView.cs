using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _209AddTempStockView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TempStockViews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MyProperty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FLAM = table.Column<string>(type: "varchar(2)", nullable: true),
                    FKUnit = table.Column<int>(type: "int", nullable: false),
                    UnitName = table.Column<string>(type: "varchar(50)", nullable: true),
                    FKLocation = table.Column<int>(type: "int", nullable: false),
                    LocationName = table.Column<string>(type: "varchar(50)", nullable: true),
                    FKStage = table.Column<int>(type: "int", nullable: false),
                    Stage = table.Column<string>(type: "varchar(50)", nullable: true),
                    FKUOM = table.Column<int>(type: "int", nullable: false),
                    UOM = table.Column<string>(type: "varchar(30)", nullable: true),
                    FKSource = table.Column<int>(type: "int", nullable: false),
                    Source = table.Column<string>(type: "varchar(50)", nullable: true),
                    FKQuality = table.Column<int>(type: "int", nullable: false),
                    Quality = table.Column<string>(type: "varchar(30)", nullable: true),
                    FKStatus = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "varchar(30)", nullable: true),
                    StockNo = table.Column<string>(type: "varchar(30)", nullable: true),
                    EANCode = table.Column<int>(type: "int", nullable: false),
                    FKMaterial = table.Column<int>(type: "int", nullable: false),
                    FKArticleDetail = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", nullable: true),
                    Colour = table.Column<string>(type: "varchar(50)", nullable: true),
                    Size = table.Column<string>(type: "varchar(10)", nullable: true),
                    OrderReferenceNo = table.Column<string>(type: "varchar(30)", nullable: true),
                    Quantity = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    Rate = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    Value = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    IPAddress = table.Column<string>(type: "varchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempStockViews", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TempStockViews");
        }
    }
}
