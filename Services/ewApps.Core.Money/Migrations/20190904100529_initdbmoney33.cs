using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.Core.Money.Migrations
{
    public partial class initdbmoney33 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "core");

            migrationBuilder.RenameTable(
                name: "DocumentCurrency",
                newName: "DocumentCurrency",
                newSchema: "core");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "DocumentCurrency",
                schema: "core",
                newName: "DocumentCurrency");
        }
    }
}
