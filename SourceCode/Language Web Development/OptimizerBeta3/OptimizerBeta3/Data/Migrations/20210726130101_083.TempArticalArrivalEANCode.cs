using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _083TempArticalArrivalEANCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TempArticalArrivalEANCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IPAddress = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    InwardNo = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    EANCode = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    IsMatching = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempArticalArrivalEANCodes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TempArticalArrivalEANCodes");
        }
    }
}
