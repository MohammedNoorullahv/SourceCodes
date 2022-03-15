using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _024AddMaterialDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "materialDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKMaterial = table.Column<int>(type: "int", nullable: false),
                    FKParty = table.Column<int>(type: "int", nullable: false),
                    FKCurrency = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<decimal>(type: "Decimal(18,4)", nullable: false),
                    MinimumOrdQty = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    MinimumTransitDays = table.Column<int>(type: "int", nullable: false),
                    IsPrimeSupplier = table.Column<bool>(type: "bit", nullable: false),
                    ApplicableFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicableTo = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_materialDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_materialDetails_lookUpMasters_FKCurrency",
                        column: x => x.FKCurrency,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_materialDetails_materials_FKMaterial",
                        column: x => x.FKMaterial,
                        principalTable: "materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_materialDetails_partyInfos_FKParty",
                        column: x => x.FKParty,
                        principalTable: "partyInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_materialDetails_FKCurrency",
                table: "materialDetails",
                column: "FKCurrency");

            migrationBuilder.CreateIndex(
                name: "IX_materialDetails_FKMaterial",
                table: "materialDetails",
                column: "FKMaterial");

            migrationBuilder.CreateIndex(
                name: "IX_materialDetails_FKParty",
                table: "materialDetails",
                column: "FKParty");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "materialDetails");
        }
    }
}
