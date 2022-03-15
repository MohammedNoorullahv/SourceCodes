using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _268AddTempOfferforInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TempOfferforInvoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferId = table.Column<int>(type: "int", nullable: false),
                    OfferCode = table.Column<string>(type: "varchar(20)", nullable: true),
                    OfferCategory = table.Column<string>(type: "varchar(20)", nullable: true),
                    OfferPair = table.Column<int>(type: "int", nullable: false),
                    BuyPair = table.Column<int>(type: "int", nullable: false),
                    IsProductCompliment = table.Column<int>(type: "int", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ApplicableForItem = table.Column<bool>(type: "bit", nullable: false),
                    ApplicableForInvoice = table.Column<bool>(type: "bit", nullable: false),
                    MinimumBillValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaximumDiscountValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsVenueBased = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempOfferforInvoices", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TempOfferforInvoices");
        }
    }
}
