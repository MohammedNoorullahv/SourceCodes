using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _126UpdateInvoiceDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDetails_articleDetails_FKArticle",
                table: "InvoiceDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDetails_lookUpMasters_FKIIUom",
                table: "InvoiceDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDetails_materials_FKMaterial",
                table: "InvoiceDetails");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceDetails_FKArticle",
                table: "InvoiceDetails");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceDetails_FKIIUom",
                table: "InvoiceDetails");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceDetails_FKMaterial",
                table: "InvoiceDetails");

            migrationBuilder.AddColumn<string>(
                name: "EANCode",
                table: "InvoiceDetails",
                type: "varchar(13)",
                maxLength: 13,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FKCustomer",
                table: "InvoiceDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MaterialorFinishedProduct",
                table: "InvoiceDetails",
                type: "varchar(2)",
                maxLength: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StockNo",
                table: "InvoiceDetails",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EANCode",
                table: "InvoiceDetails");

            migrationBuilder.DropColumn(
                name: "FKCustomer",
                table: "InvoiceDetails");

            migrationBuilder.DropColumn(
                name: "MaterialorFinishedProduct",
                table: "InvoiceDetails");

            migrationBuilder.DropColumn(
                name: "StockNo",
                table: "InvoiceDetails");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_FKArticle",
                table: "InvoiceDetails",
                column: "FKArticle");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_FKIIUom",
                table: "InvoiceDetails",
                column: "FKIIUom");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_FKMaterial",
                table: "InvoiceDetails",
                column: "FKMaterial");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDetails_articleDetails_FKArticle",
                table: "InvoiceDetails",
                column: "FKArticle",
                principalTable: "articleDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDetails_lookUpMasters_FKIIUom",
                table: "InvoiceDetails",
                column: "FKIIUom",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDetails_materials_FKMaterial",
                table: "InvoiceDetails",
                column: "FKMaterial",
                principalTable: "materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
