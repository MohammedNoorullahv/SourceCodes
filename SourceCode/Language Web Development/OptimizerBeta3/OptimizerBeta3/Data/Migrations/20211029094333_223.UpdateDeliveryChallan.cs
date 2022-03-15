using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _223UpdateDeliveryChallan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryChallans_lookUpMasters_FKFromDepartment",
                table: "DeliveryChallans");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryChallans_lookUpMasters_FKToDepartment",
                table: "DeliveryChallans");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryChallans_lookUpMasters_FKToLocation",
                table: "DeliveryChallans");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryChallans_FKFromDepartment",
                table: "DeliveryChallans");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryChallans_FKToDepartment",
                table: "DeliveryChallans");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryChallans_FKToLocation",
                table: "DeliveryChallans");

            migrationBuilder.DropColumn(
                name: "MaterialorFinishedProduct",
                table: "DeliveryChallans");

            migrationBuilder.RenameColumn(
                name: "ToDepartment",
                table: "DeliveryChallans",
                newName: "GSTNo");

            migrationBuilder.RenameColumn(
                name: "FromDepartment",
                table: "DeliveryChallans",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "FKToDepartment",
                table: "DeliveryChallans",
                newName: "Pincode");

            migrationBuilder.RenameColumn(
                name: "FKFromDepartment",
                table: "DeliveryChallans",
                newName: "FKCategory");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "DeliveryChallans",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Area",
                table: "DeliveryChallans",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "DeliveryChallans",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvoiceTo",
                table: "DeliveryChallans",
                type: "varchar(2)",
                maxLength: 2,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LookUpMasterInvoiceCategoryId",
                table: "DeliveryChallans",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryChallans_LookUpMasterInvoiceCategoryId",
                table: "DeliveryChallans",
                column: "LookUpMasterInvoiceCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryChallans_lookUpMasters_LookUpMasterInvoiceCategoryId",
                table: "DeliveryChallans",
                column: "LookUpMasterInvoiceCategoryId",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryChallans_lookUpMasters_LookUpMasterInvoiceCategoryId",
                table: "DeliveryChallans");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryChallans_LookUpMasterInvoiceCategoryId",
                table: "DeliveryChallans");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "DeliveryChallans");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "DeliveryChallans");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "DeliveryChallans");

            migrationBuilder.DropColumn(
                name: "InvoiceTo",
                table: "DeliveryChallans");

            migrationBuilder.DropColumn(
                name: "LookUpMasterInvoiceCategoryId",
                table: "DeliveryChallans");

            migrationBuilder.RenameColumn(
                name: "Pincode",
                table: "DeliveryChallans",
                newName: "FKToDepartment");

            migrationBuilder.RenameColumn(
                name: "GSTNo",
                table: "DeliveryChallans",
                newName: "ToDepartment");

            migrationBuilder.RenameColumn(
                name: "FKCategory",
                table: "DeliveryChallans",
                newName: "FKFromDepartment");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "DeliveryChallans",
                newName: "FromDepartment");

            migrationBuilder.AddColumn<string>(
                name: "MaterialorFinishedProduct",
                table: "DeliveryChallans",
                type: "varchar(1)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryChallans_FKFromDepartment",
                table: "DeliveryChallans",
                column: "FKFromDepartment");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryChallans_FKToDepartment",
                table: "DeliveryChallans",
                column: "FKToDepartment");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryChallans_FKToLocation",
                table: "DeliveryChallans",
                column: "FKToLocation");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryChallans_lookUpMasters_FKFromDepartment",
                table: "DeliveryChallans",
                column: "FKFromDepartment",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryChallans_lookUpMasters_FKToDepartment",
                table: "DeliveryChallans",
                column: "FKToDepartment",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryChallans_lookUpMasters_FKToLocation",
                table: "DeliveryChallans",
                column: "FKToLocation",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
