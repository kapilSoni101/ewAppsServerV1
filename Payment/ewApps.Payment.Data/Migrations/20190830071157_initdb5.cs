using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.Payment.Data.Migrations
{
    public partial class initdb5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "pay",
                table: "Role",
                columns: new[] { "ID", "Active", "AppId", "CreatedBy", "CreatedOn", "Deleted", "PermissionBitMask", "RoleKey", "RoleName", "UpdatedBy", "UpdatedOn", "UserType" },
                values: new object[] { new Guid("6327a668-e7d7-451c-bf86-722f027d5d79"), true, new Guid("2b031b59-5276-589c-9d75-2a7ae1c799c8"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, 511L, "admin", "Admin", new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "pay",
                table: "Role",
                keyColumn: "ID",
                keyValue: new Guid("6327a668-e7d7-451c-bf86-722f027d5d79"));
        }
    }
}
