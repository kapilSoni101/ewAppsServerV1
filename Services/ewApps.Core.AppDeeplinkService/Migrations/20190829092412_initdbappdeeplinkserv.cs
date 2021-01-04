using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.Core.AppDeeplinkService.Migrations
{
    public partial class initdbappdeeplinkserv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppDeeplink",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    NumberId = table.Column<long>(nullable: false),
                    ShortUrlKey = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: false),
                    MaxUseCount = table.Column<int>(nullable: false),
                    UserAccessCount = table.Column<int>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: true),
                    Password = table.Column<string>(maxLength: 20, nullable: true),
                    ActionData = table.Column<string>(maxLength: 100, nullable: true),
                    ActionName = table.Column<string>(maxLength: 100, nullable: true),
                    ActionEndpointUrl = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDeeplink", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AppDeeplinkAccessLog",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    AppDeeplinkId = table.Column<Guid>(nullable: false),
                    AccessTimestamp = table.Column<DateTime>(nullable: false),
                    AccessUrl = table.Column<string>(maxLength: 100, nullable: true),
                    AccessGranted = table.Column<bool>(nullable: false),
                    AccessNotGrantedReason = table.Column<string>(maxLength: 100, nullable: true),
                    AccessorIPAddress = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDeeplinkAccessLog", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppDeeplink");

            migrationBuilder.DropTable(
                name: "AppDeeplinkAccessLog");
        }
    }
}
