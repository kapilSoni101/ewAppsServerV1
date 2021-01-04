using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.Core.Webhook.Subscription.Migrations
{
    public partial class initdbwebhooksub33 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "core");

            migrationBuilder.RenameTable(
                name: "WebhookClientSubscription",
                newName: "WebhookClientSubscription",
                newSchema: "core");

            migrationBuilder.RenameTable(
                name: "WebhookClientEventQueue",
                newName: "WebhookClientEventQueue",
                newSchema: "core");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "WebhookClientSubscription",
                schema: "core",
                newName: "WebhookClientSubscription");

            migrationBuilder.RenameTable(
                name: "WebhookClientEventQueue",
                schema: "core",
                newName: "WebhookClientEventQueue");
        }
    }
}
