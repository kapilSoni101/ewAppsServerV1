using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.Core.Webhook.Server.Migrations
{
    public partial class initdbwebhookserv33 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "core");

            migrationBuilder.RenameTable(
                name: "WebhookSubscription",
                newName: "WebhookSubscription",
                newSchema: "core");

            migrationBuilder.RenameTable(
                name: "WebhookServer",
                newName: "WebhookServer",
                newSchema: "core");

            migrationBuilder.RenameTable(
                name: "WebhookEventQueue",
                newName: "WebhookEventQueue",
                newSchema: "core");

            migrationBuilder.RenameTable(
                name: "WebhookEventDeliveryLog",
                newName: "WebhookEventDeliveryLog",
                newSchema: "core");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "WebhookSubscription",
                schema: "core",
                newName: "WebhookSubscription");

            migrationBuilder.RenameTable(
                name: "WebhookServer",
                schema: "core",
                newName: "WebhookServer");

            migrationBuilder.RenameTable(
                name: "WebhookEventQueue",
                schema: "core",
                newName: "WebhookEventQueue");

            migrationBuilder.RenameTable(
                name: "WebhookEventDeliveryLog",
                schema: "core",
                newName: "WebhookEventDeliveryLog");
        }
    }
}
