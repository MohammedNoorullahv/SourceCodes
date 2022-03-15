using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _013AddPartyInfoDtls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "partyInfoDtls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKPartyInfo = table.Column<int>(type: "int", nullable: false),
                    FKUnitType = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true),
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
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EnteredSystemId = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteBy = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_partyInfoDtls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_partyInfoDtls_lookUpMasters_FKArea",
                        column: x => x.FKArea,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_partyInfoDtls_lookUpMasters_FKCity",
                        column: x => x.FKCity,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_partyInfoDtls_lookUpMasters_FKCountry",
                        column: x => x.FKCountry,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_partyInfoDtls_lookUpMasters_FKPincode",
                        column: x => x.FKPincode,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_partyInfoDtls_lookUpMasters_FKState",
                        column: x => x.FKState,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_partyInfoDtls_lookUpMasters_FKUnitType",
                        column: x => x.FKUnitType,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_partyInfoDtls_partyInfos_FKPartyInfo",
                        column: x => x.FKPartyInfo,
                        principalTable: "partyInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_partyInfoDtls_FKArea",
                table: "partyInfoDtls",
                column: "FKArea");

            migrationBuilder.CreateIndex(
                name: "IX_partyInfoDtls_FKCity",
                table: "partyInfoDtls",
                column: "FKCity");

            migrationBuilder.CreateIndex(
                name: "IX_partyInfoDtls_FKCountry",
                table: "partyInfoDtls",
                column: "FKCountry");

            migrationBuilder.CreateIndex(
                name: "IX_partyInfoDtls_FKPartyInfo",
                table: "partyInfoDtls",
                column: "FKPartyInfo");

            migrationBuilder.CreateIndex(
                name: "IX_partyInfoDtls_FKPincode",
                table: "partyInfoDtls",
                column: "FKPincode");

            migrationBuilder.CreateIndex(
                name: "IX_partyInfoDtls_FKState",
                table: "partyInfoDtls",
                column: "FKState");

            migrationBuilder.CreateIndex(
                name: "IX_partyInfoDtls_FKUnitType",
                table: "partyInfoDtls",
                column: "FKUnitType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "partyInfoDtls");
        }
    }
}
