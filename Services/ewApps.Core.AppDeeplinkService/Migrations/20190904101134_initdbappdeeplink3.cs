using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.Core.AppDeeplinkService.Migrations
{
    public partial class initdbappdeeplink3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "core");

            migrationBuilder.RenameTable(
                name: "AppDeeplinkAccessLog",
                newName: "AppDeeplinkAccessLog",
                newSchema: "core");

            migrationBuilder.RenameTable(
                name: "AppDeeplink",
                newName: "AppDeeplink",
                newSchema: "core");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "AppDeeplinkAccessLog",
                schema: "core",
                newName: "AppDeeplinkAccessLog");

            migrationBuilder.RenameTable(
                name: "AppDeeplink",
                schema: "core",
                newName: "AppDeeplink");
        }
    }
}
