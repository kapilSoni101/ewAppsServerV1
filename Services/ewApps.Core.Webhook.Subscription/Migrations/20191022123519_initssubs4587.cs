using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.Core.Webhook.Subscription.Migrations
{
    public partial class initssubs4587 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SubscribedEvents",
                schema: "core",
                table: "WebhookClientSubscription",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EventName",
                schema: "core",
                table: "WebhookClientEventQueue",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ProcessedDate",
                schema: "core",
                table: "WebhookClientEventQueue",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "core",
                table: "WebhookClientEventQueue",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcessedDate",
                schema: "core",
                table: "WebhookClientEventQueue");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "core",
                table: "WebhookClientEventQueue");

            migrationBuilder.AlterColumn<string>(
                name: "SubscribedEvents",
                schema: "core",
                table: "WebhookClientSubscription",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EventName",
                schema: "core",
                table: "WebhookClientEventQueue",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
