using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _017AddCusomerPersonInfoDtls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customerPersonInfoDtls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKPartyInfo = table.Column<int>(type: "int", nullable: false),
                    OfficePhoneNo = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    HomePhoneNo = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    MobileNo = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    EMailId = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    Gender = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsMarried = table.Column<bool>(type: "bit", nullable: true),
                    WeddingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                    table.PrimaryKey("PK_customerPersonInfoDtls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_customerPersonInfoDtls_partyInfos_FKPartyInfo",
                        column: x => x.FKPartyInfo,
                        principalTable: "partyInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_customerPersonInfoDtls_FKPartyInfo",
                table: "customerPersonInfoDtls",
                column: "FKPartyInfo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customerPersonInfoDtls");
        }
    }
}
