using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.AppPortal.Data.Migrations
{
    public partial class initapppor6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "ap",
                table: "Role",
                keyColumn: "ID",
                keyValue: new Guid("09116a01-2ed7-42a9-962c-c07999f40340"));

            migrationBuilder.RenameColumn(
                name: "Iden",
                schema: "ap",
                table: "Customer",
                newName: "IdentityNumber");

            migrationBuilder.InsertData(
                schema: "ap",
                table: "Role",
                columns: new[] { "ID", "Active", "AppId", "CreatedBy", "CreatedOn", "Deleted", "PermissionBitMask", "RoleKey", "RoleName", "UpdatedBy", "UpdatedOn", "UserType" },
                values: new object[] { new Guid("2fb520ad-6655-44eb-ba5f-c4abe5af01da"), true, new Guid("2b031b59-5276-589c-9d75-2a7ae1c799c8"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, 63L, "admin", "Admin", new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 4 });

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "RoleLinking",
                keyColumn: "ID",
                keyValue: new Guid("0b03bbe7-b3f7-4978-9411-2b9cb8976579"),
                columns: new[] { "CreatedBy", "UpdatedBy" },
                values: new object[] { new Guid("4278d1fc-2bac-4c2c-be0c-25955d87733e"), new Guid("0808036c-16ef-4fe4-89be-641285b9028a") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "ap",
                table: "Role",
                keyColumn: "ID",
                keyValue: new Guid("2fb520ad-6655-44eb-ba5f-c4abe5af01da"));

            migrationBuilder.RenameColumn(
                name: "IdentityNumber",
                schema: "ap",
                table: "Customer",
                newName: "Iden");

            migrationBuilder.InsertData(
                schema: "ap",
                table: "Role",
                columns: new[] { "ID", "Active", "AppId", "CreatedBy", "CreatedOn", "Deleted", "PermissionBitMask", "RoleKey", "RoleName", "UpdatedBy", "UpdatedOn", "UserType" },
                values: new object[] { new Guid("09116a01-2ed7-42a9-962c-c07999f40340"), true, new Guid("2b031b59-5276-589c-9d75-2a7ae1c799c8"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, 63L, "admin", "Admin", new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 4 });

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "RoleLinking",
                keyColumn: "ID",
                keyValue: new Guid("0b03bbe7-b3f7-4978-9411-2b9cb8976579"),
                columns: new[] { "CreatedBy", "UpdatedBy" },
                values: new object[] { new Guid("65fa80f3-4a76-43df-ae92-8ea37c09ad5f"), new Guid("a60304b8-145e-4a4d-a251-5bef00099601") });
        }
    }
}
