using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.Core.EmailService.Migrations
{
    public partial class initdbemailserv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailDeliveryLog",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    DeliveryType = table.Column<int>(nullable: false),
                    DeliverySubType = table.Column<int>(nullable: false),
                    MessagePart1 = table.Column<string>(maxLength: 4000, nullable: false),
                    MessagePart2 = table.Column<string>(maxLength: 4000, nullable: true),
                    Recipient = table.Column<string>(maxLength: 100, nullable: false),
                    CC = table.Column<string>(maxLength: 100, nullable: true),
                    BCC = table.Column<string>(maxLength: 100, nullable: true),
                    ReplyTo = table.Column<string>(maxLength: 100, nullable: true),
                    Sender = table.Column<string>(maxLength: 100, nullable: true),
                    SenderName = table.Column<string>(maxLength: 100, nullable: true),
                    SenderKey = table.Column<string>(maxLength: 100, nullable: false),
                    ScheduledDeliveryTime = table.Column<DateTime>(nullable: false),
                    ActualDeliveryTime = table.Column<DateTime>(nullable: false),
                    ApplicationId = table.Column<Guid>(nullable: false),
                    NotificationId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailDeliveryLog", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EmailQueue",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    DeliveryType = table.Column<int>(nullable: false),
                    DeliverySubType = table.Column<int>(nullable: false),
                    MessagePart1 = table.Column<string>(maxLength: 4000, nullable: false),
                    MessagePart2 = table.Column<string>(maxLength: 4000, nullable: true),
                    Recipient = table.Column<string>(maxLength: 100, nullable: false),
                    CC = table.Column<string>(maxLength: 100, nullable: true),
                    BCC = table.Column<string>(maxLength: 100, nullable: true),
                    ReplyTo = table.Column<string>(maxLength: 100, nullable: true),
                    Sender = table.Column<string>(maxLength: 100, nullable: true),
                    SenderName = table.Column<string>(maxLength: 100, nullable: true),
                    SenderKey = table.Column<string>(maxLength: 100, nullable: false),
                    DeliveryTime = table.Column<DateTime>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    ApplicationId = table.Column<Guid>(nullable: false),
                    NotificationId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailQueue", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailDeliveryLog");

            migrationBuilder.DropTable(
                name: "EmailQueue");
        }
    }
}
