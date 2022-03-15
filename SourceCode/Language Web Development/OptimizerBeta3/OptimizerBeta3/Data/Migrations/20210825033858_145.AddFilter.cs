using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _145AddFilter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Filters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ControllerName = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    ActionMethod = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    TableName = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    FKLookUpCategory = table.Column<int>(type: "int", nullable: false),
                    LookUPCategory = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    FKLookUpMaster = table.Column<int>(type: "int", nullable: false),
                    LookUPMaster = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    ConditionIn = table.Column<bool>(type: "bit", nullable: false),
                    ConditionNotIn = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_Filters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Filters_lookUpCategories_FKLookUpCategory",
                        column: x => x.FKLookUpCategory,
                        principalTable: "lookUpCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Filters_lookUpMasters_FKLookUpMaster",
                        column: x => x.FKLookUpMaster,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Filters_FKLookUpCategory",
                table: "Filters",
                column: "FKLookUpCategory");

            migrationBuilder.CreateIndex(
                name: "IX_Filters_FKLookUpMaster",
                table: "Filters",
                column: "FKLookUpMaster");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Filters");
        }
    }
}
