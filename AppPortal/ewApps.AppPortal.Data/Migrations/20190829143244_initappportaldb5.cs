using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.AppPortal.Data.Migrations
{
    public partial class initappportaldb5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "ap",
                table: "Role",
                columns: new[] { "ID", "Active", "AppId", "CreatedBy", "CreatedOn", "Deleted", "PermissionBitMask", "RoleKey", "RoleName", "UpdatedBy", "UpdatedOn", "UserType" },
                values: new object[,]
                {
                    { new Guid("df435b1a-7dbd-4e8f-b0f9-c07ba6e84f80"), true, new Guid("8ded3502-01e5-469a-909b-5424d50d00d6"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, 511L, "admin", "Admin", new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 1 },
                    { new Guid("fb8be172-2c7f-4f04-b40a-487fda92e323"), true, new Guid("67d09a6f-ce95-498c-bf69-33c7d38f9041"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, 4095L, "admin", "Admin", new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 2 },
                    { new Guid("958d4943-124b-4467-b345-395dcd37e2fe"), true, new Guid("f4952ef3-f1bd-4621-a5f9-290fd09bc81b"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, 3L, "admin", "Admin", new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 3 },
                    { new Guid("09116a01-2ed7-42a9-962c-c07999f40340"), true, new Guid("2b031b59-5276-589c-9d75-2a7ae1c799c8"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, 63L, "admin", "Admin", new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 4 }
                });

            migrationBuilder.InsertData(
                schema: "ap",
                table: "RoleLinking",
                columns: new[] { "ID", "AppId", "CreatedBy", "CreatedOn", "Deleted", "RoleId", "TenantId", "TenantUserId", "UpdatedBy", "UpdatedOn" },
                values: new object[] { new Guid("0b03bbe7-b3f7-4978-9411-2b9cb8976579"), new Guid("8ded3502-01e5-469a-909b-5424d50d00d6"), new Guid("65fa80f3-4a76-43df-ae92-8ea37c09ad5f"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, new Guid("df435b1a-7dbd-4e8f-b0f9-c07ba6e84f80"), new Guid("18571765-24b5-4c36-a957-416eaec38fda"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new Guid("a60304b8-145e-4a4d-a251-5bef00099601"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "ap",
                table: "Role",
                keyColumn: "ID",
                keyValue: new Guid("09116a01-2ed7-42a9-962c-c07999f40340"));

            migrationBuilder.DeleteData(
                schema: "ap",
                table: "Role",
                keyColumn: "ID",
                keyValue: new Guid("958d4943-124b-4467-b345-395dcd37e2fe"));

            migrationBuilder.DeleteData(
                schema: "ap",
                table: "Role",
                keyColumn: "ID",
                keyValue: new Guid("df435b1a-7dbd-4e8f-b0f9-c07ba6e84f80"));

            migrationBuilder.DeleteData(
                schema: "ap",
                table: "Role",
                keyColumn: "ID",
                keyValue: new Guid("fb8be172-2c7f-4f04-b40a-487fda92e323"));

            migrationBuilder.DeleteData(
                schema: "ap",
                table: "RoleLinking",
                keyColumn: "ID",
                keyValue: new Guid("0b03bbe7-b3f7-4978-9411-2b9cb8976579"));
        }
    }
}
