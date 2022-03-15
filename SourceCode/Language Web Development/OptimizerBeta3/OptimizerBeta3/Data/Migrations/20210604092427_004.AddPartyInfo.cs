using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _004AddPartyInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "partyInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKCategory = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "varchar(5)", nullable: true),
                    CompanyName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    ShortName = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    Address1 = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Address2 = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    FKArea = table.Column<int>(type: "int", nullable: false),
                    FKCity = table.Column<int>(type: "int", nullable: false),
                    FKPincode = table.Column<int>(type: "int", nullable: false),
                    FKState = table.Column<int>(type: "int", nullable: false),
                    FKCountry = table.Column<int>(type: "int", nullable: false),
                    ContactPersonName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    ContactNo = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    MailId = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    PANNumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    GSTNumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EnteredSystemId = table.Column<string>(type: "varchar(5)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteBy = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_partyInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_partyInfos_lookUpMasters_FKArea",
                        column: x => x.FKArea,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_partyInfos_lookUpMasters_FKCategory",
                        column: x => x.FKCategory,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_partyInfos_lookUpMasters_FKCity",
                        column: x => x.FKCity,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_partyInfos_lookUpMasters_FKCountry",
                        column: x => x.FKCountry,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_partyInfos_lookUpMasters_FKPincode",
                        column: x => x.FKPincode,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_partyInfos_lookUpMasters_FKState",
                        column: x => x.FKState,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_partyInfos_FKArea",
                table: "partyInfos",
                column: "FKArea");

            migrationBuilder.CreateIndex(
                name: "IX_partyInfos_FKCategory",
                table: "partyInfos",
                column: "FKCategory");

            migrationBuilder.CreateIndex(
                name: "IX_partyInfos_FKCity",
                table: "partyInfos",
                column: "FKCity");

            migrationBuilder.CreateIndex(
                name: "IX_partyInfos_FKCountry",
                table: "partyInfos",
                column: "FKCountry");

            migrationBuilder.CreateIndex(
                name: "IX_partyInfos_FKPincode",
                table: "partyInfos",
                column: "FKPincode");

            migrationBuilder.CreateIndex(
                name: "IX_partyInfos_FKState",
                table: "partyInfos",
                column: "FKState");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "partyInfos");
        }
    }
}
