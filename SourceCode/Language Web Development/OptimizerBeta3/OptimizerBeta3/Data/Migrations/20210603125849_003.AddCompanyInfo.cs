using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _003AddCompanyInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "companyInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true),
                    CompanyName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    ShortName = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    Address1 = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Address2 = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    FKArea = table.Column<int>(type: "int", nullable: false),
                    FKCity = table.Column<int>(type: "int", nullable: false),
                    FKPincode = table.Column<int>(type: "int", nullable: false),
                    FKState = table.Column<int>(type: "int", nullable: false),
                    ContactPersonName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    ContactNo = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    MailId = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    PANNumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    GSTNumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
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
                    table.PrimaryKey("PK_companyInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_companyInfos_lookUpMasters_FKArea",
                        column: x => x.FKArea,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_companyInfos_lookUpMasters_FKCity",
                        column: x => x.FKCity,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_companyInfos_lookUpMasters_FKPincode",
                        column: x => x.FKPincode,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_companyInfos_lookUpMasters_FKState",
                        column: x => x.FKState,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_companyInfos_FKArea",
                table: "companyInfos",
                column: "FKArea");

            migrationBuilder.CreateIndex(
                name: "IX_companyInfos_FKCity",
                table: "companyInfos",
                column: "FKCity");

            migrationBuilder.CreateIndex(
                name: "IX_companyInfos_FKPincode",
                table: "companyInfos",
                column: "FKPincode");

            migrationBuilder.CreateIndex(
                name: "IX_companyInfos_FKState",
                table: "companyInfos",
                column: "FKState");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "companyInfos");
        }
    }
}
