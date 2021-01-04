using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.Core.UserSessionService.Migrations
{
    public partial class initusersession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserSession",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 100, nullable: false),
                    TenantUserId = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    TenantName = table.Column<string>(maxLength: 100, nullable: false),
                    AppId = table.Column<Guid>(nullable: false),
                    IdentityToken = table.Column<string>(maxLength: 4000, nullable: true),
                    UserType = table.Column<int>(nullable: false),
                    Subdomain = table.Column<string>(maxLength: 100, nullable: true),
                    IdentityServerId = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSession", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSession");
        }
    }
}
