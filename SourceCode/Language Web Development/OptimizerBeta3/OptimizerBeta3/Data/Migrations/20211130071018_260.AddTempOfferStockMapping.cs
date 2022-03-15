using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _260AddTempOfferStockMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TempOfferStockMappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtId = table.Column<int>(type: "int", nullable: false),
                    ArticleGroup = table.Column<string>(type: "varchar(20)", nullable: true),
                    ArticleName = table.Column<string>(type: "varchar(100)", nullable: true),
                    ArticleColor = table.Column<string>(type: "varchar(100)", nullable: true),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    OfferId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempOfferStockMappings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TempOfferStockMappings");
        }
    }
}
