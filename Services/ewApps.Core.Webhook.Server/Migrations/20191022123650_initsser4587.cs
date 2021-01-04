using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.Core.Webhook.Server.Migrations
{
    public partial class initsser4587 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SubscribeEvents",
                schema: "core",
                table: "WebhookSubscription",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ServerEvents",
                schema: "core",
                table: "WebhookServer",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EventName",
                schema: "core",
                table: "WebhookEventQueue",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SubscriptionCallBack",
                schema: "core",
                table: "WebhookEventDeliveryLog",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EventName",
                schema: "core",
                table: "WebhookEventDeliveryLog",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SubscribeEvents",
                schema: "core",
                table: "WebhookSubscription",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ServerEvents",
                schema: "core",
                table: "WebhookServer",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EventName",
                schema: "core",
                table: "WebhookEventQueue",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SubscriptionCallBack",
                schema: "core",
                table: "WebhookEventDeliveryLog",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EventName",
                schema: "core",
                table: "WebhookEventDeliveryLog",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
