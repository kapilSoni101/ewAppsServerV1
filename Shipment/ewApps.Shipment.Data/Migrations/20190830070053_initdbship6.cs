using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.Shipment.Data.Migrations
{
    public partial class initdbship6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "ship",
                table: "Role",
                columns: new[] { "ID", "Active", "AppId", "CreatedBy", "CreatedOn", "Deleted", "PermissionBitMask", "RoleKey", "RoleName", "UpdatedBy", "UpdatedOn", "UserType" },
                values: new object[] { new Guid("427ed55a-e054-4090-a18c-5900e074d624"), true, new Guid("6118a7ff-742b-25db-a9c1-8e252c39bb73"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, 511L, "admin", "Admin", new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "ship",
                table: "Role",
                keyColumn: "ID",
                keyValue: new Guid("427ed55a-e054-4090-a18c-5900e074d624"));
        }
    }
}
