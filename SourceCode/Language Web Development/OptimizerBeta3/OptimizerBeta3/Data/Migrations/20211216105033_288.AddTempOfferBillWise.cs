using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _288AddTempOfferBillWise : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuantityForOC",
                table: "Estimates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TempOffersBillWises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferId = table.Column<int>(type: "int", nullable: false),
                    OfferCode = table.Column<string>(type: "varchar(20)", nullable: true),
                    OfferCategory = table.Column<string>(type: "varchar(20)", nullable: true),
                    OfferPair = table.Column<int>(type: "int", nullable: false),
                    BuyPair = table.Column<int>(type: "int", nullable: false),
                    IsProductCompliment = table.Column<bool>(type: "bit", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ApplicableForItem = table.Column<bool>(type: "bit", nullable: false),
                    ApplicableForInvoice = table.Column<bool>(type: "bit", nullable: false),
                    MinimumBillValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaximumDiscountValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsVenueBased = table.Column<bool>(type: "bit", nullable: false),
                    EstimateId = table.Column<int>(type: "int", nullable: false),
                    FKLocation = table.Column<int>(type: "int", nullable: false),
                    InvQuantity = table.Column<int>(type: "int", nullable: false),
                    InvValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InvDiscountValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InvGrossValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IncludedArticles = table.Column<string>(type: "Varchar(200)", nullable: true),
                    ExcludedArticles = table.Column<string>(type: "Varchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempOffersBillWises", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TempOffersBillWises");

            migrationBuilder.DropColumn(
                name: "QuantityForOC",
                table: "Estimates");
        }
    }
}
