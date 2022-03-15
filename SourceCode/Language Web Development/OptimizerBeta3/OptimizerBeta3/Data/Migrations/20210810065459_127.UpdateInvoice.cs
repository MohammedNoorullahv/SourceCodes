using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _127UpdateInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_partyInfos_FKDeliveryTo",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_FKDeliveryTo",
                table: "Invoices");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Invoices",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FKCategory",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IncludeBillTo",
                table: "Invoices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IncludeDeliveryTo",
                table: "Invoices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IncludeNotifyTo",
                table: "Invoices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_FKCategory",
                table: "Invoices",
                column: "FKCategory");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_FKDestination",
                table: "Invoices",
                column: "FKDestination");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_FKLocation",
                table: "Invoices",
                column: "FKLocation");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_locations_FKLocation",
                table: "Invoices",
                column: "FKLocation",
                principalTable: "locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_lookUpMasters_FKCategory",
                table: "Invoices",
                column: "FKCategory",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_lookUpMasters_FKDestination",
                table: "Invoices",
                column: "FKDestination",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_locations_FKLocation",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_lookUpMasters_FKCategory",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_lookUpMasters_FKDestination",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_FKCategory",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_FKDestination",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_FKLocation",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "FKCategory",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "IncludeBillTo",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "IncludeDeliveryTo",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "IncludeNotifyTo",
                table: "Invoices");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_FKDeliveryTo",
                table: "Invoices",
                column: "FKDeliveryTo");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_partyInfos_FKDeliveryTo",
                table: "Invoices",
                column: "FKDeliveryTo",
                principalTable: "partyInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
