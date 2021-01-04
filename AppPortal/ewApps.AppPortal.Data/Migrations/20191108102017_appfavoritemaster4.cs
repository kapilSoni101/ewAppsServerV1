using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.AppPortal.Data.Migrations
{
    public partial class appfavoritemaster4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "ap",
                table: "Favorite",
                columns: new[] { "ID", "AppId", "CreatedBy", "CreatedOn", "Deleted", "MenuKey", "PortalKey", "System", "TenantId", "TenantUserId", "UpdatedBy", "UpdatedOn", "Url" },
                values: new object[] { new Guid("438a6e60-4b05-4896-8344-f7f660b3b629"), new Guid("67d09a6f-ce95-498c-bf69-33c7d38f9041"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, " Invite Business", "PubPortal", true, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), "payment/history" });

            migrationBuilder.InsertData(
                schema: "ap",
                table: "Favorite",
                columns: new[] { "ID", "AppId", "CreatedBy", "CreatedOn", "Deleted", "MenuKey", "PortalKey", "System", "TenantId", "TenantUserId", "UpdatedBy", "UpdatedOn", "Url" },
                values: new object[] { new Guid("e406a4db-030d-4864-908e-08332bb6ab98"), new Guid("67d09a6f-ce95-498c-bf69-33c7d38f9041"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "Open Tickets", "PubPortal", true, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), "payment/history" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "ap",
                table: "Favorite",
                keyColumn: "ID",
                keyValue: new Guid("438a6e60-4b05-4896-8344-f7f660b3b629"));

            migrationBuilder.DeleteData(
                schema: "ap",
                table: "Favorite",
                keyColumn: "ID",
                keyValue: new Guid("e406a4db-030d-4864-908e-08332bb6ab98"));
        }
    }
}
