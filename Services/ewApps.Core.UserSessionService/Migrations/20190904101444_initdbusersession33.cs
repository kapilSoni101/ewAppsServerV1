using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.Core.UserSessionService.Migrations
{
    public partial class initdbusersession33 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "core");

            migrationBuilder.RenameTable(
                name: "UserSession",
                newName: "UserSession",
                newSchema: "core");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "UserSession",
                schema: "core",
                newName: "UserSession");
        }
    }
}
