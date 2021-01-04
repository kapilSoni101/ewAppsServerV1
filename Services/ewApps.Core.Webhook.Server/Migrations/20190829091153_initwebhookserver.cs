using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.Core.Webhook.Server.Migrations
{
    public partial class initwebhookserver : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WebhookEventDeliveryLog",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ServerName = table.Column<string>(maxLength: 100, nullable: false),
                    SubscriptionName = table.Column<string>(maxLength: 100, nullable: false),
                    EventName = table.Column<string>(maxLength: 100, nullable: false),
                    Payload = table.Column<string>(maxLength: 4000, nullable: true),
                    SubscriptionCallBack = table.Column<string>(maxLength: 100, nullable: true),
                    EventQueueTime = table.Column<DateTime>(nullable: false),
                    DeliveryAttempts = table.Column<int>(nullable: false),
                    DeliveryStatus = table.Column<string>(maxLength: 20, nullable: true),
                    LastDeliveryTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebhookEventDeliveryLog", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WebhookEventQueue",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    EventName = table.Column<string>(maxLength: 100, nullable: true),
                    Payload = table.Column<string>(maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebhookEventQueue", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WebhookServer",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ServerName = table.Column<string>(maxLength: 100, nullable: false),
                    ServerEvents = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebhookServer", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WebhookSubscription",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    SubscriptionName = table.Column<string>(maxLength: 100, nullable: false),
                    ServerName = table.Column<string>(maxLength: 100, nullable: false),
                    SubscribeEvents = table.Column<string>(maxLength: 100, nullable: true),
                    CallbackEndPoint = table.Column<string>(maxLength: 100, nullable: false),
                    ServerShutDownCallBackEndPoint = table.Column<string>(maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebhookSubscription", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebhookEventDeliveryLog");

            migrationBuilder.DropTable(
                name: "WebhookEventQueue");

            migrationBuilder.DropTable(
                name: "WebhookServer");

            migrationBuilder.DropTable(
                name: "WebhookSubscription");
        }
    }
}
