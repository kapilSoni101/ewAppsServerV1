using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.Core.Webhook.Server.Migrations
{
    public partial class initwebserver908 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ServerShutDownCallBackEndPoint",
                schema: "core",
                table: "WebhookSubscription",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CallbackEndPoint",
                schema: "core",
                table: "WebhookSubscription",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Payload",
                schema: "core",
                table: "WebhookEventQueue",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Payload",
                schema: "core",
                table: "WebhookEventDeliveryLog",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 4000,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ServerShutDownCallBackEndPoint",
                schema: "core",
                table: "WebhookSubscription",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CallbackEndPoint",
                schema: "core",
                table: "WebhookSubscription",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Payload",
                schema: "core",
                table: "WebhookEventQueue",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Payload",
                schema: "core",
                table: "WebhookEventDeliveryLog",
                maxLength: 4000,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
