using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _281AddEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKUnit = table.Column<int>(type: "int", nullable: false),
                    UnitName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    FKSalutation = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "varchar(160)", maxLength: 160, nullable: false),
                    FirstName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    MiddleName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Initials = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    FKMaritalStatus = table.Column<int>(type: "int", nullable: false),
                    HorFName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Gender = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    Address1 = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Address2 = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    FKArea = table.Column<int>(type: "int", nullable: false),
                    FKCity = table.Column<int>(type: "int", nullable: false),
                    FKPincode = table.Column<int>(type: "int", nullable: false),
                    FKState = table.Column<int>(type: "int", nullable: false),
                    MobileNo = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    EMailId = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FKDOBProofType = table.Column<int>(type: "int", nullable: false),
                    DOJ = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FKDepartment = table.Column<int>(type: "int", nullable: false),
                    FKDesignation = table.Column<int>(type: "int", nullable: false),
                    FKEmpCategory = table.Column<int>(type: "int", nullable: false),
                    FKReligion = table.Column<int>(type: "int", nullable: false),
                    FKQualification = table.Column<int>(type: "int", nullable: false),
                    NoofDependants = table.Column<int>(type: "int", nullable: false),
                    PFNo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    ESINo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    PANNo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    BankAccountNo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    ResignDate = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_lookUpMasters_FKArea",
                        column: x => x.FKArea,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Employees_lookUpMasters_FKCity",
                        column: x => x.FKCity,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Employees_lookUpMasters_FKDepartment",
                        column: x => x.FKDepartment,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Employees_lookUpMasters_FKDesignation",
                        column: x => x.FKDesignation,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Employees_lookUpMasters_FKDOBProofType",
                        column: x => x.FKDOBProofType,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Employees_lookUpMasters_FKEmpCategory",
                        column: x => x.FKEmpCategory,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Employees_lookUpMasters_FKMaritalStatus",
                        column: x => x.FKMaritalStatus,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Employees_lookUpMasters_FKPincode",
                        column: x => x.FKPincode,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Employees_lookUpMasters_FKQualification",
                        column: x => x.FKQualification,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Employees_lookUpMasters_FKReligion",
                        column: x => x.FKReligion,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Employees_lookUpMasters_FKSalutation",
                        column: x => x.FKSalutation,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Employees_StateMasters_FKState",
                        column: x => x.FKState,
                        principalTable: "StateMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Employees_unitMasters_FKUnit",
                        column: x => x.FKUnit,
                        principalTable: "unitMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_FKArea",
                table: "Employees",
                column: "FKArea");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_FKCity",
                table: "Employees",
                column: "FKCity");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_FKDepartment",
                table: "Employees",
                column: "FKDepartment");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_FKDesignation",
                table: "Employees",
                column: "FKDesignation");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_FKDOBProofType",
                table: "Employees",
                column: "FKDOBProofType");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_FKEmpCategory",
                table: "Employees",
                column: "FKEmpCategory");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_FKMaritalStatus",
                table: "Employees",
                column: "FKMaritalStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_FKPincode",
                table: "Employees",
                column: "FKPincode");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_FKQualification",
                table: "Employees",
                column: "FKQualification");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_FKReligion",
                table: "Employees",
                column: "FKReligion");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_FKSalutation",
                table: "Employees",
                column: "FKSalutation");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_FKState",
                table: "Employees",
                column: "FKState");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_FKUnit",
                table: "Employees",
                column: "FKUnit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
