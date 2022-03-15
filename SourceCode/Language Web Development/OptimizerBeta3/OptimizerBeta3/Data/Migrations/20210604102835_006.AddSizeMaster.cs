using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _006AddSizeMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sizeMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKSizeCategory = table.Column<int>(type: "int", nullable: false),
                    FKSizeFor = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true),
                    Description = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    IsHalfSize = table.Column<bool>(type: "bit", nullable: false),
                    Size01 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Size02 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Size03 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Size04 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Size05 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Size06 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Size07 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Size08 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Size09 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Size10 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Size11 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Size12 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Size13 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Size14 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Size15 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Size16 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Size17 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Size18 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
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
                    table.PrimaryKey("PK_sizeMasters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sizeMasters_lookUpMasters_FKSizeCategory",
                        column: x => x.FKSizeCategory,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sizeMasters_lookUpMasters_FKSizeFor",
                        column: x => x.FKSizeFor,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_sizeMasters_FKSizeCategory",
                table: "sizeMasters",
                column: "FKSizeCategory");

            migrationBuilder.CreateIndex(
                name: "IX_sizeMasters_FKSizeFor",
                table: "sizeMasters",
                column: "FKSizeFor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sizeMasters");
        }
    }
}
