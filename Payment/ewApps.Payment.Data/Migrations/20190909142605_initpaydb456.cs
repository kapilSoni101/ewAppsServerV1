using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.Payment.Data.Migrations
{
    public partial class initpaydb456 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TenantUserAppPreference",
                schema: "pay",
                columns: table => new
                {
                    CreatedBy = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    ID = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    TenantUserId = table.Column<Guid>(nullable: false),
                    AppId = table.Column<Guid>(nullable: false),
                    EmailPreference = table.Column<long>(nullable: false),
                    SMSPreference = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantUserAppPreference", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TenantUserAppPreference",
                schema: "pay");
        }
    }
}
