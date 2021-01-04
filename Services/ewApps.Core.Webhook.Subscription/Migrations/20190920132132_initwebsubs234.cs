using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.Core.Webhook.Subscription.Migrations
{
    public partial class initwebsubs234 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ServerHostEndPoint",
                schema: "core",
                table: "WebhookClientSubscription",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Payload",
                schema: "core",
                table: "WebhookClientEventQueue",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 4000,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ServerHostEndPoint",
                schema: "core",
                table: "WebhookClientSubscription",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Payload",
                schema: "core",
                table: "WebhookClientEventQueue",
                maxLength: 4000,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
