using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.Core.EmailService.Migrations
{
    public partial class initdbemailservice3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "core");

            migrationBuilder.RenameTable(
                name: "EmailQueue",
                newName: "EmailQueue",
                newSchema: "core");

            migrationBuilder.RenameTable(
                name: "EmailDeliveryLog",
                newName: "EmailDeliveryLog",
                newSchema: "core");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "EmailQueue",
                schema: "core",
                newName: "EmailQueue");

            migrationBuilder.RenameTable(
                name: "EmailDeliveryLog",
                schema: "core",
                newName: "EmailDeliveryLog");
        }
    }
}
