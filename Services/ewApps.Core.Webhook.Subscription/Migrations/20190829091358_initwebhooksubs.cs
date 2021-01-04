using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.Core.Webhook.Subscription.Migrations
{
    public partial class initwebhooksubs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WebhookClientEventQueue",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ServerName = table.Column<string>(maxLength: 100, nullable: true),
                    SubscriptionName = table.Column<string>(maxLength: 100, nullable: true),
                    EventName = table.Column<string>(maxLength: 100, nullable: true),
                    Payload = table.Column<string>(maxLength: 4000, nullable: true),
                    ReceivedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebhookClientEventQueue", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WebhookClientSubscription",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    SubscriptionName = table.Column<string>(maxLength: 100, nullable: false),
                    ServerName = table.Column<string>(maxLength: 100, nullable: false),
                    ServerHostEndPoint = table.Column<string>(maxLength: 100, nullable: false),
                    SubscribedEvents = table.Column<string>(maxLength: 100, nullable: true),
                    CallBackEndPoint = table.Column<string>(maxLength: 100, nullable: true),
                    ServerShutDownCallBackEndPoint = table.Column<string>(maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebhookClientSubscription", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebhookClientEventQueue");

            migrationBuilder.DropTable(
                name: "WebhookClientSubscription");
        }
    }
}
