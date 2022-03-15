using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _234UpdateInvoiceRemoveFKPartyLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_partyInfos_FKParty",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_FKParty",
                table: "Invoices");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Invoices_FKParty",
                table: "Invoices",
                column: "FKParty");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_partyInfos_FKParty",
                table: "Invoices",
                column: "FKParty",
                principalTable: "partyInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
