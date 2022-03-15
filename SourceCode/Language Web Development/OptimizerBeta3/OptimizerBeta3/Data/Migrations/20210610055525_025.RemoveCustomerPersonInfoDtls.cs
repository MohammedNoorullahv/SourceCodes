using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _025RemoveCustomerPersonInfoDtls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customerPersonInfoDtls");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customerPersonInfoDtls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteBy = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EMailId = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    EnteredSystemId = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true),
                    FKPartyInfo = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    HomePhoneNo = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsMarried = table.Column<bool>(type: "bit", nullable: true),
                    MobileNo = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OfficePhoneNo = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    WeddingDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customerPersonInfoDtls", x => x.Id);
                });
        }
    }
}
