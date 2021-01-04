using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.Core.SMSService.Migrations
{
    public partial class initwebhooksmsserv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SMSDeliveryLog",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    DeliveryType = table.Column<int>(nullable: false),
                    DeliverySubType = table.Column<int>(nullable: false),
                    Recipient = table.Column<string>(maxLength: 100, nullable: false),
                    MessagePart1 = table.Column<string>(maxLength: 4000, nullable: true),
                    MessagePart2 = table.Column<string>(maxLength: 4000, nullable: false),
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
                    table.PrimaryKey("PK_SMSDeliveryLog", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SMSQueue",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    DeliveryType = table.Column<int>(nullable: false),
                    DeliverySubType = table.Column<int>(nullable: false),
                    Recipient = table.Column<string>(maxLength: 100, nullable: false),
                    MessagePart1 = table.Column<string>(maxLength: 4000, nullable: true),
                    MessagePart2 = table.Column<string>(maxLength: 4000, nullable: false),
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
                    table.PrimaryKey("PK_SMSQueue", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SMSDeliveryLog");

            migrationBuilder.DropTable(
                name: "SMSQueue");
        }
    }
}
