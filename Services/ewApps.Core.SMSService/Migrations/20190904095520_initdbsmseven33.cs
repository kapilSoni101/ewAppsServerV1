using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.Core.SMSService.Migrations
{
    public partial class initdbsmseven33 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "core");

            migrationBuilder.RenameTable(
                name: "SMSQueue",
                newName: "SMSQueue",
                newSchema: "core");

            migrationBuilder.RenameTable(
                name: "SMSDeliveryLog",
                newName: "SMSDeliveryLog",
                newSchema: "core");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "SMSQueue",
                schema: "core",
                newName: "SMSQueue");

            migrationBuilder.RenameTable(
                name: "SMSDeliveryLog",
                schema: "core",
                newName: "SMSDeliveryLog");
        }
    }
}
