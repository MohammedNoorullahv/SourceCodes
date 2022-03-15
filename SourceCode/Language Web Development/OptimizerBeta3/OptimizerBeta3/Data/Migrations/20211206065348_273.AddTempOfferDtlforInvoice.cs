using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _273AddTempOfferDtlforInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExcludedArticles",
                table: "TempOfferforInvoices",
                type: "Varchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IncludedArticles",
                table: "TempOfferforInvoices",
                type: "Varchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "InvDiscountValue",
                table: "TempOfferforInvoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "InvGrossValue",
                table: "TempOfferforInvoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "InvQuantity",
                table: "TempOfferforInvoices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "InvValue",
                table: "TempOfferforInvoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "TempOfferDtlforInvoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TempOfferId = table.Column<int>(type: "int", nullable: false),
                    OfferId = table.Column<int>(type: "int", nullable: false),
                    FKArticle = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrossValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempOfferDtlforInvoices", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TempOfferDtlforInvoices");

            migrationBuilder.DropColumn(
                name: "ExcludedArticles",
                table: "TempOfferforInvoices");

            migrationBuilder.DropColumn(
                name: "IncludedArticles",
                table: "TempOfferforInvoices");

            migrationBuilder.DropColumn(
                name: "InvDiscountValue",
                table: "TempOfferforInvoices");

            migrationBuilder.DropColumn(
                name: "InvGrossValue",
                table: "TempOfferforInvoices");

            migrationBuilder.DropColumn(
                name: "InvQuantity",
                table: "TempOfferforInvoices");

            migrationBuilder.DropColumn(
                name: "InvValue",
                table: "TempOfferforInvoices");
        }
    }
}
