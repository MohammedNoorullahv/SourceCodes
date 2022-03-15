using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _146UpdateArticleGroupRemoveFKParty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_articleGroups_partyInfos_FKParty",
                table: "articleGroups");

            migrationBuilder.DropIndex(
                name: "IX_articleGroups_FKParty",
                table: "articleGroups");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "articleGroups");

            migrationBuilder.DropColumn(
                name: "FKParty",
                table: "articleGroups");

            migrationBuilder.AlterColumn<bool>(
                name: "IsNameCompulsory",
                table: "paymentOptions",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsExpiryDateCompulsory",
                table: "paymentOptions",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CardNoMinLength",
                table: "paymentOptions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsNameCompulsory",
                table: "paymentOptions",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsExpiryDateCompulsory",
                table: "paymentOptions",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "CardNoMinLength",
                table: "paymentOptions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "articleGroups",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FKParty",
                table: "articleGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_articleGroups_FKParty",
                table: "articleGroups",
                column: "FKParty");

            migrationBuilder.AddForeignKey(
                name: "FK_articleGroups_partyInfos_FKParty",
                table: "articleGroups",
                column: "FKParty",
                principalTable: "partyInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
