using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.Core.Webhook.Subscription.Migrations
{
    public partial class initwebsubs236 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ServerShutDownCallBackEndPoint",
                schema: "core",
                table: "WebhookClientSubscription",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CallBackEndPoint",
                schema: "core",
                table: "WebhookClientSubscription",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ServerShutDownCallBackEndPoint",
                schema: "core",
                table: "WebhookClientSubscription",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CallBackEndPoint",
                schema: "core",
                table: "WebhookClientSubscription",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
