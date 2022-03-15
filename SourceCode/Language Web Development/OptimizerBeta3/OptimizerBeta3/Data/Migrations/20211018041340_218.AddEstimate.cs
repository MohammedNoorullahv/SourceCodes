using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _218AddEstimate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estimates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstimateNo = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true),
                    EstimateDt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FKStore = table.Column<int>(type: "int", nullable: false),
                    StoreName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    FKFromState = table.Column<int>(type: "int", nullable: false),
                    FKLocation = table.Column<int>(type: "int", nullable: false),
                    LocationName = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    FKToState = table.Column<int>(type: "int", nullable: false),
                    ItemsCount = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    GrossValue = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    GSTValues = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    OtherExpensesPlus = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    OtherExpensesMinus = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    NettValue = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    FKInvoice = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EnteredSystemId = table.Column<string>(type: "varchar(30)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteBy = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estimates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estimates_locations_FKLocation",
                        column: x => x.FKLocation,
                        principalTable: "locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Estimates_StateMasters_FKToState",
                        column: x => x.FKToState,
                        principalTable: "StateMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Estimates_unitMasters_FKStore",
                        column: x => x.FKStore,
                        principalTable: "unitMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estimates_FKLocation",
                table: "Estimates",
                column: "FKLocation");

            migrationBuilder.CreateIndex(
                name: "IX_Estimates_FKStore",
                table: "Estimates",
                column: "FKStore");

            migrationBuilder.CreateIndex(
                name: "IX_Estimates_FKToState",
                table: "Estimates",
                column: "FKToState");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estimates");
        }
    }
}
