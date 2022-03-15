using Microsoft.EntityFrameworkCore.Migrations;

namespace OptimizerBeta3.Data.Migrations
{
    public partial class _122AddTempInvoiceDetailEANCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TempInvoiceDtlEANCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IPAddress = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    InwardNo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    EANCode = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IsMatching = table.Column<bool>(type: "bit", nullable: false),
                    Reason = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempInvoiceDtlEANCodes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TempInvoiceDtlEANCodes");
        }
    }
}
