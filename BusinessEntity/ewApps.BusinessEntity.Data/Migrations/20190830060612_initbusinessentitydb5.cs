using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.BusinessEntity.Data.Migrations
{
    public partial class initbusinessentitydb5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "be",
                table: "Role",
                columns: new[] { "ID", "Active", "AppId", "CreatedBy", "CreatedOn", "Deleted", "PermissionBitMask", "RoleKey", "RoleName", "UpdatedBy", "UpdatedOn", "UserType" },
                values: new object[] { new Guid("da5421d7-dfc6-425a-93e9-a3272e740727"), true, new Guid("8c6fa8ce-6b94-428f-95de-5ed8859260d2"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, 511L, "admin", "Admin", new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 3 });

            migrationBuilder.InsertData(
                schema: "be",
                table: "Role",
                columns: new[] { "ID", "Active", "AppId", "CreatedBy", "CreatedOn", "Deleted", "PermissionBitMask", "RoleKey", "RoleName", "UpdatedBy", "UpdatedOn", "UserType" },
                values: new object[] { new Guid("8cecec11-2352-431d-86bc-ba78322649a7"), true, new Guid("8c6fa8ce-6b94-428f-95de-5ed8859260d2"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, 4095L, "admin", "Admin", new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 4 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "be",
                table: "Role",
                keyColumn: "ID",
                keyValue: new Guid("8cecec11-2352-431d-86bc-ba78322649a7"));

            migrationBuilder.DeleteData(
                schema: "be",
                table: "Role",
                keyColumn: "ID",
                keyValue: new Guid("da5421d7-dfc6-425a-93e9-a3272e740727"));
        }
    }
}
