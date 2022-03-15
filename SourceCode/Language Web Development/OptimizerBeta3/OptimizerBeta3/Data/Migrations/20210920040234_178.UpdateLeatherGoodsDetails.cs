using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _178UpdateLeatherGoodsDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_leatherGoodsDetails_SizeMasterforLeatherGoods_FKSize",
                table: "leatherGoodsDetails");

            migrationBuilder.RenameColumn(
                name: "Size",
                table: "leatherGoodsDetails",
                newName: "SizeorDimension");

            migrationBuilder.RenameColumn(
                name: "FKSize",
                table: "leatherGoodsDetails",
                newName: "FKSizeorDimension");

            migrationBuilder.RenameIndex(
                name: "IX_leatherGoodsDetails_FKSize",
                table: "leatherGoodsDetails",
                newName: "IX_leatherGoodsDetails_FKSizeorDimension");

            migrationBuilder.AddForeignKey(
                name: "FK_leatherGoodsDetails_lookUpMasters_FKSizeorDimension",
                table: "leatherGoodsDetails",
                column: "FKSizeorDimension",
                principalTable: "lookUpMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_leatherGoodsDetails_lookUpMasters_FKSizeorDimension",
                table: "leatherGoodsDetails");

            migrationBuilder.RenameColumn(
                name: "SizeorDimension",
                table: "leatherGoodsDetails",
                newName: "Size");

            migrationBuilder.RenameColumn(
                name: "FKSizeorDimension",
                table: "leatherGoodsDetails",
                newName: "FKSize");

            migrationBuilder.RenameIndex(
                name: "IX_leatherGoodsDetails_FKSizeorDimension",
                table: "leatherGoodsDetails",
                newName: "IX_leatherGoodsDetails_FKSize");

            migrationBuilder.AddForeignKey(
                name: "FK_leatherGoodsDetails_SizeMasterforLeatherGoods_FKSize",
                table: "leatherGoodsDetails",
                column: "FKSize",
                principalTable: "SizeMasterforLeatherGoods",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
