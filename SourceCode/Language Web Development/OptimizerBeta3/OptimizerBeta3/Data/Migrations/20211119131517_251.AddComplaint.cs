using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _251AddComplaint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Complaints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComplaintDt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FKMenuCategory = table.Column<int>(type: "int", nullable: false),
                    FKMenuName = table.Column<int>(type: "int", nullable: false),
                    FKLocation = table.Column<int>(type: "int", nullable: false),
                    FKAdminName = table.Column<int>(type: "int", nullable: false),
                    FKUserName = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    AddressLink = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    FKStatus = table.Column<int>(type: "int", nullable: false),
                    CommentBySupportTeam = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    PlannedDt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedDt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteBy = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complaints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Complaints_ComplaintLookUpMasters_FKAdminName",
                        column: x => x.FKAdminName,
                        principalTable: "ComplaintLookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Complaints_ComplaintLookUpMasters_FKLocation",
                        column: x => x.FKLocation,
                        principalTable: "ComplaintLookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Complaints_ComplaintLookUpMasters_FKMenuCategory",
                        column: x => x.FKMenuCategory,
                        principalTable: "ComplaintLookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Complaints_ComplaintLookUpMasters_FKMenuName",
                        column: x => x.FKMenuName,
                        principalTable: "ComplaintLookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Complaints_ComplaintLookUpMasters_FKStatus",
                        column: x => x.FKStatus,
                        principalTable: "ComplaintLookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Complaints_ComplaintLookUpMasters_FKUserName",
                        column: x => x.FKUserName,
                        principalTable: "ComplaintLookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_FKAdminName",
                table: "Complaints",
                column: "FKAdminName");

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_FKLocation",
                table: "Complaints",
                column: "FKLocation");

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_FKMenuCategory",
                table: "Complaints",
                column: "FKMenuCategory");

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_FKMenuName",
                table: "Complaints",
                column: "FKMenuName");

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_FKStatus",
                table: "Complaints",
                column: "FKStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_FKUserName",
                table: "Complaints",
                column: "FKUserName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Complaints");
        }
    }
}
