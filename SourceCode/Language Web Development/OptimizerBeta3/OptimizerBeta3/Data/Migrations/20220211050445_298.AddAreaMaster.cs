using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _298AddAreaMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AreaMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKCountry = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    FKState = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    FKCity = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    FKArea = table.Column<int>(type: "int", nullable: false),
                    Area = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    FKPincode = table.Column<int>(type: "int", nullable: false),
                    Pincode = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
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
                    table.PrimaryKey("PK_AreaMasters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AreaMasters_AreaLookUpMasters_FKArea",
                        column: x => x.FKArea,
                        principalTable: "AreaLookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AreaMasters_AreaLookUpMasters_FKCity",
                        column: x => x.FKCity,
                        principalTable: "AreaLookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AreaMasters_AreaLookUpMasters_FKCountry",
                        column: x => x.FKCountry,
                        principalTable: "AreaLookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AreaMasters_AreaLookUpMasters_FKPincode",
                        column: x => x.FKPincode,
                        principalTable: "AreaLookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AreaMasters_AreaLookUpMasters_FKState",
                        column: x => x.FKState,
                        principalTable: "AreaLookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AreaMasters_FKArea",
                table: "AreaMasters",
                column: "FKArea");

            migrationBuilder.CreateIndex(
                name: "IX_AreaMasters_FKCity",
                table: "AreaMasters",
                column: "FKCity");

            migrationBuilder.CreateIndex(
                name: "IX_AreaMasters_FKCountry",
                table: "AreaMasters",
                column: "FKCountry");

            migrationBuilder.CreateIndex(
                name: "IX_AreaMasters_FKPincode",
                table: "AreaMasters",
                column: "FKPincode");

            migrationBuilder.CreateIndex(
                name: "IX_AreaMasters_FKState",
                table: "AreaMasters",
                column: "FKState");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AreaMasters");
        }
    }
}
