using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _029AddSizeAssortments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sizeAssortments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKSizeMaster = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "varchar(6)", maxLength: 6, nullable: false),
                    Description = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Size01 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity01 = table.Column<int>(type: "int", nullable: true),
                    Size02 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity02 = table.Column<int>(type: "int", nullable: true),
                    Size03 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity03 = table.Column<int>(type: "int", nullable: true),
                    Size04 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity04 = table.Column<int>(type: "int", nullable: true),
                    Size05 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity05 = table.Column<int>(type: "int", nullable: true),
                    Size06 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity06 = table.Column<int>(type: "int", nullable: true),
                    Size07 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity07 = table.Column<int>(type: "int", nullable: true),
                    Size08 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity08 = table.Column<int>(type: "int", nullable: true),
                    Size09 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity09 = table.Column<int>(type: "int", nullable: true),
                    Size10 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity10 = table.Column<int>(type: "int", nullable: true),
                    Size11 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity11 = table.Column<int>(type: "int", nullable: true),
                    Size12 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity12 = table.Column<int>(type: "int", nullable: true),
                    Size13 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity13 = table.Column<int>(type: "int", nullable: true),
                    Size14 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity14 = table.Column<int>(type: "int", nullable: true),
                    Size15 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity15 = table.Column<int>(type: "int", nullable: true),
                    Size16 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity16 = table.Column<int>(type: "int", nullable: true),
                    Size17 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity17 = table.Column<int>(type: "int", nullable: true),
                    Size18 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity18 = table.Column<int>(type: "int", nullable: true),
                    TotalQuantity = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EnteredSystemId = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteBy = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sizeAssortments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sizeAssortments_sizeMasters_FKSizeMaster",
                        column: x => x.FKSizeMaster,
                        principalTable: "sizeMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_sizeAssortments_FKSizeMaster",
                table: "sizeAssortments",
                column: "FKSizeMaster");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sizeAssortments");
        }
    }
}
