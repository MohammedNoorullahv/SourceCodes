using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _026AddCustomerPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customerPerson",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Gender = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    Address = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    FKArea = table.Column<int>(type: "int", nullable: false),
                    FKCity = table.Column<int>(type: "int", nullable: false),
                    FKPincode = table.Column<int>(type: "int", nullable: false),
                    FKState = table.Column<int>(type: "int", nullable: false),
                    FKCountry = table.Column<int>(type: "int", nullable: false),
                    OfficePhoneNo = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    HomePhoneNo = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    MobileNo = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    EMailId = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
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
                    table.PrimaryKey("PK_customerPerson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_customerPerson_lookUpMasters_FKArea",
                        column: x => x.FKArea,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_customerPerson_lookUpMasters_FKCity",
                        column: x => x.FKCity,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_customerPerson_lookUpMasters_FKCountry",
                        column: x => x.FKCountry,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_customerPerson_lookUpMasters_FKPincode",
                        column: x => x.FKPincode,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_customerPerson_lookUpMasters_FKState",
                        column: x => x.FKState,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_customerPerson_FKArea",
                table: "customerPerson",
                column: "FKArea");

            migrationBuilder.CreateIndex(
                name: "IX_customerPerson_FKCity",
                table: "customerPerson",
                column: "FKCity");

            migrationBuilder.CreateIndex(
                name: "IX_customerPerson_FKCountry",
                table: "customerPerson",
                column: "FKCountry");

            migrationBuilder.CreateIndex(
                name: "IX_customerPerson_FKPincode",
                table: "customerPerson",
                column: "FKPincode");

            migrationBuilder.CreateIndex(
                name: "IX_customerPerson_FKState",
                table: "customerPerson",
                column: "FKState");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customerPerson");
        }
    }
}
