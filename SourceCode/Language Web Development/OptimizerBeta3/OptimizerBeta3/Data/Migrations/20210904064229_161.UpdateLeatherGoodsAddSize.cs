using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _161UpdateLeatherGoodsAddSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FKSize",
                table: "leatherGoodsDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "leatherGoodsDetails",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_leatherGoodsDetails_FKSize",
                table: "leatherGoodsDetails",
                column: "FKSize");

            migrationBuilder.AddForeignKey(
                name: "FK_leatherGoodsDetails_SizeMasterforLeatherGoods_FKSize",
                table: "leatherGoodsDetails",
                column: "FKSize",
                principalTable: "SizeMasterforLeatherGoods",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_leatherGoodsDetails_SizeMasterforLeatherGoods_FKSize",
                table: "leatherGoodsDetails");

            migrationBuilder.DropIndex(
                name: "IX_leatherGoodsDetails_FKSize",
                table: "leatherGoodsDetails");

            migrationBuilder.DropColumn(
                name: "FKSize",
                table: "leatherGoodsDetails");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "leatherGoodsDetails");
        }
    }
}
