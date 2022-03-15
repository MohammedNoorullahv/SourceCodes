using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _160AddSizeMasterForLeatherGoods : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SizeMasterforLeatherGoods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "varchar(6)", maxLength: 6, nullable: true),
                    Description = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    ShortDescription = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    FKMeasurement = table.Column<int>(type: "int", nullable: false),
                    Measurement = table.Column<string>(type: "varchar(20)", nullable: true),
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
                    table.PrimaryKey("PK_SizeMasterforLeatherGoods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SizeMasterforLeatherGoods_lookUpMasters_FKMeasurement",
                        column: x => x.FKMeasurement,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SizeMasterforLeatherGoods_FKMeasurement",
                table: "SizeMasterforLeatherGoods",
                column: "FKMeasurement");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SizeMasterforLeatherGoods");
        }
    }
}
