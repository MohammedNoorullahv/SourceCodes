using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _077UpdateInwardDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inwardDetails_lookUpMasters_FKIIUom",
                table: "inwardDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_inwardDetails_lookUpMasters_FKUOM",
                table: "inwardDetails");

            migrationBuilder.DropIndex(
                name: "IX_inwardDetails_FKIIUom",
                table: "inwardDetails");

            migrationBuilder.DropIndex(
                name: "IX_inwardDetails_FKUOM",
                table: "inwardDetails");

            migrationBuilder.DropColumn(
                name: "FKIIUom",
                table: "inwardDetails");

            migrationBuilder.DropColumn(
                name: "FKUOM",
                table: "inwardDetails");

            migrationBuilder.AddColumn<DateTime>(
                name: "InwardDt",
                table: "inwardDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "InwardNo",
                table: "inwardDetails",
                type: "varchar(20)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InwardDt",
                table: "inwardDetails");

            migrationBuilder.DropColumn(
                name: "InwardNo",
                table: "inwardDetails");

            migrationBuilder.AddColumn<int>(
                name: "FKIIUom",
                table: "inwardDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKUOM",
                table: "inwardDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_inwardDetails_FKIIUom",
                table: "inwardDetails",
                column: "FKIIUom");

            migrationBuilder.CreateIndex(
                name: "IX_inwardDetails_FKUOM",
                table: "inwardDetails",
                column: "FKUOM");

            migrationBuilder.AddForeignKey(
                name: "FK_inwardDetails_lookUpMasters_FKIIUom",
                table: "inwardDetails",
                column: "FKIIUom",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_inwardDetails_lookUpMasters_FKUOM",
                table: "inwardDetails",
                column: "FKUOM",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
