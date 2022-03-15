using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _166AddTempStockForOfferMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TempStockForOfferMappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKCategory = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    FKUnit = table.Column<int>(type: "int", nullable: false),
                    UnitName = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    Description = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    ArticleGroupName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    FKLocation = table.Column<int>(type: "int", nullable: false),
                    LocationName = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    FKArticleDetail = table.Column<int>(type: "int", nullable: false),
                    ArticleDescription = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Colour = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    OrderReferenceNo = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    Size = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    Quantity = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    MRP = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    FKStage = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempStockForOfferMappings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TempStockForOfferMappings");
        }
    }
}
