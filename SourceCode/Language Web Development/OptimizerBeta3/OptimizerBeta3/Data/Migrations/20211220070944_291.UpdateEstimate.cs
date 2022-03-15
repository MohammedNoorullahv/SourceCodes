using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _291UpdateEstimate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InvDiscountValue",
                table: "InvoiceToPersons",
                newName: "InvDiscountValuePWise");

            migrationBuilder.RenameColumn(
                name: "InvDiscountPercentage",
                table: "InvoiceToPersons",
                newName: "InvDiscountValueBWise");

            migrationBuilder.RenameColumn(
                name: "OfferValue",
                table: "Estimates",
                newName: "OfferValuePWise");

            migrationBuilder.RenameColumn(
                name: "OfferPercentage",
                table: "Estimates",
                newName: "OfferValueBWise");

            migrationBuilder.RenameColumn(
                name: "DiscountValue",
                table: "EstimateDetails",
                newName: "DiscountValuePWise");

            migrationBuilder.RenameColumn(
                name: "DiscountPercentage",
                table: "EstimateDetails",
                newName: "DiscountValueBWise");

            migrationBuilder.AddColumn<decimal>(
                name: "InvDiscountPercentageBWise",
                table: "InvoiceToPersons",
                type: "Decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "InvDiscountPercentagePWise",
                table: "InvoiceToPersons",
                type: "Decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OfferPercentageBWise",
                table: "Estimates",
                type: "Decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OfferPercentagePWise",
                table: "Estimates",
                type: "Decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercentageBWise",
                table: "EstimateDetails",
                type: "Decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercentagePWise",
                table: "EstimateDetails",
                type: "Decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvDiscountPercentageBWise",
                table: "InvoiceToPersons");

            migrationBuilder.DropColumn(
                name: "InvDiscountPercentagePWise",
                table: "InvoiceToPersons");

            migrationBuilder.DropColumn(
                name: "OfferPercentageBWise",
                table: "Estimates");

            migrationBuilder.DropColumn(
                name: "OfferPercentagePWise",
                table: "Estimates");

            migrationBuilder.DropColumn(
                name: "DiscountPercentageBWise",
                table: "EstimateDetails");

            migrationBuilder.DropColumn(
                name: "DiscountPercentagePWise",
                table: "EstimateDetails");

            migrationBuilder.RenameColumn(
                name: "InvDiscountValuePWise",
                table: "InvoiceToPersons",
                newName: "InvDiscountValue");

            migrationBuilder.RenameColumn(
                name: "InvDiscountValueBWise",
                table: "InvoiceToPersons",
                newName: "InvDiscountPercentage");

            migrationBuilder.RenameColumn(
                name: "OfferValuePWise",
                table: "Estimates",
                newName: "OfferValue");

            migrationBuilder.RenameColumn(
                name: "OfferValueBWise",
                table: "Estimates",
                newName: "OfferPercentage");

            migrationBuilder.RenameColumn(
                name: "DiscountValuePWise",
                table: "EstimateDetails",
                newName: "DiscountValue");

            migrationBuilder.RenameColumn(
                name: "DiscountValueBWise",
                table: "EstimateDetails",
                newName: "DiscountPercentage");
        }
    }
}
