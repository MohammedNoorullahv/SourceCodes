using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _237UpdateArticleDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TempFootWearOrderImports");

            migrationBuilder.AddColumn<string>(
                name: "ArtGrp",
                table: "articleDetails",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArticleCode",
                table: "articleDetails",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "POMainDefaults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsPartDeliveryAllowed = table.Column<bool>(type: "bit", nullable: false),
                    FKDestination = table.Column<int>(type: "int", nullable: false),
                    Destination = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    FKDeliveryTo = table.Column<int>(type: "int", nullable: false),
                    DeliveryTo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    FKModeofTransport = table.Column<int>(type: "int", nullable: false),
                    ModeofTransport = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    FKOrderStatus = table.Column<int>(type: "int", nullable: false),
                    OrderStatus = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POMainDefaults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_POMainDefaults_lookUpMasters_FKDeliveryTo",
                        column: x => x.FKDeliveryTo,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_POMainDefaults_lookUpMasters_FKDestination",
                        column: x => x.FKDestination,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_POMainDefaults_lookUpMasters_FKModeofTransport",
                        column: x => x.FKModeofTransport,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_POMainDefaults_lookUpMasters_FKOrderStatus",
                        column: x => x.FKOrderStatus,
                        principalTable: "lookUpMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_POMainDefaults_FKDeliveryTo",
                table: "POMainDefaults",
                column: "FKDeliveryTo");

            migrationBuilder.CreateIndex(
                name: "IX_POMainDefaults_FKDestination",
                table: "POMainDefaults",
                column: "FKDestination");

            migrationBuilder.CreateIndex(
                name: "IX_POMainDefaults_FKModeofTransport",
                table: "POMainDefaults",
                column: "FKModeofTransport");

            migrationBuilder.CreateIndex(
                name: "IX_POMainDefaults_FKOrderStatus",
                table: "POMainDefaults",
                column: "FKOrderStatus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "POMainDefaults");

            migrationBuilder.DropColumn(
                name: "ArtGrp",
                table: "articleDetails");

            migrationBuilder.DropColumn(
                name: "ArticleCode",
                table: "articleDetails");

            migrationBuilder.CreateTable(
                name: "TempFootWearOrderImports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Article = table.Column<string>(type: "varchar(100)", nullable: true),
                    ArticleRef = table.Column<string>(type: "varchar(50)", nullable: true),
                    BarcodeColor = table.Column<string>(type: "varchar(50)", nullable: true),
                    Color = table.Column<string>(type: "varchar(100)", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FACArticleNo = table.Column<string>(type: "varchar(50)", nullable: true),
                    FACColor = table.Column<string>(type: "varchar(50)", nullable: true),
                    Group = table.Column<string>(type: "varchar(50)", nullable: true),
                    IPAddress = table.Column<string>(type: "varchar(20)", nullable: true),
                    Last = table.Column<string>(type: "varchar(50)", nullable: true),
                    Leather = table.Column<string>(type: "varchar(50)", nullable: true),
                    Lining = table.Column<string>(type: "varchar(50)", nullable: true),
                    Remarks = table.Column<string>(type: "varchar(50)", nullable: true),
                    Remarks1 = table.Column<string>(type: "varchar(50)", nullable: true),
                    Remarks2 = table.Column<string>(type: "varchar(50)", nullable: true),
                    Remarks3 = table.Column<string>(type: "varchar(50)", nullable: true),
                    Remarks4 = table.Column<string>(type: "varchar(50)", nullable: true),
                    Remarks5 = table.Column<string>(type: "varchar(50)", nullable: true),
                    SKU = table.Column<string>(type: "varchar(20)", nullable: true),
                    Size01 = table.Column<int>(type: "int", nullable: false),
                    Size02 = table.Column<int>(type: "int", nullable: false),
                    Size03 = table.Column<int>(type: "int", nullable: false),
                    Size04 = table.Column<int>(type: "int", nullable: false),
                    Size05 = table.Column<int>(type: "int", nullable: false),
                    Size06 = table.Column<int>(type: "int", nullable: false),
                    Size07 = table.Column<int>(type: "int", nullable: false),
                    Size08 = table.Column<int>(type: "int", nullable: false),
                    Size09 = table.Column<int>(type: "int", nullable: false),
                    Size10 = table.Column<int>(type: "int", nullable: false),
                    Size11 = table.Column<int>(type: "int", nullable: false),
                    Size12 = table.Column<int>(type: "int", nullable: false),
                    Size13 = table.Column<int>(type: "int", nullable: false),
                    SlNo = table.Column<int>(type: "int", nullable: false),
                    Socks = table.Column<string>(type: "varchar(50)", nullable: true),
                    Sole = table.Column<string>(type: "varchar(50)", nullable: true),
                    Total = table.Column<int>(type: "int", nullable: false),
                    UniqueID = table.Column<string>(type: "varchar(30)", nullable: true),
                    status = table.Column<string>(type: "varchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempFootWearOrderImports", x => x.Id);
                });
        }
    }
}
