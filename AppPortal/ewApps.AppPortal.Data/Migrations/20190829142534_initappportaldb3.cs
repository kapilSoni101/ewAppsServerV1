using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.AppPortal.Data.Migrations
{
    public partial class initappportaldb3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "ap",
                table: "Portal",
                keyColumn: "ID",
                keyValue: new Guid("67d09a6f-ce95-498c-bf69-33c7d38f9041"));

            migrationBuilder.DeleteData(
                schema: "ap",
                table: "Portal",
                keyColumn: "ID",
                keyValue: new Guid("8ded3502-01e5-469a-909b-5424d50d00d6"));

            migrationBuilder.DeleteData(
                schema: "ap",
                table: "Portal",
                keyColumn: "ID",
                keyValue: new Guid("f4952ef3-f1bd-4621-a5f9-290fd09bc81b"));

            migrationBuilder.InsertData(
                schema: "ap",
                table: "Portal",
                columns: new[] { "ID", "CreatedBy", "CreatedOn", "Deleted", "Name", "PortalKey", "UpdatedBy", "UpdatedOn", "UserType" },
                values: new object[,]
                {
                    { new Guid("5a066e57-73c2-4fae-a7bb-0be3abdfb35c"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "Platform Portal", "PlatPortal", new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 1 },
                    { new Guid("0ac1701b-3d90-454c-80a9-7cf062109795"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "Publisher Portal", "PubPortal", new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 2 },
                    { new Guid("31127b13-5be1-48b5-aa85-93a28682ef20"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "Business Portal", "BizPortal", new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 3 }
                });

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "PortalAppLinking",
                keyColumn: "ID",
                keyValue: new Guid("0a826d78-14af-469f-8f78-9a7b068c574c"),
                columns: new[] { "AppId", "PortalId" },
                values: new object[] { new Guid("f4952ef3-f1bd-4621-a5f9-290fd09bc81b"), new Guid("31127b13-5be1-48b5-aa85-93a28682ef20") });

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "PortalAppLinking",
                keyColumn: "ID",
                keyValue: new Guid("7a128e85-c2e2-4650-85bd-cc433a4e532e"),
                column: "PortalId",
                value: new Guid("31127b13-5be1-48b5-aa85-93a28682ef20"));

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "PortalAppLinking",
                keyColumn: "ID",
                keyValue: new Guid("7c9a4607-9410-4e61-b4ab-f1d8df7305cc"),
                columns: new[] { "AppId", "PortalId" },
                values: new object[] { new Guid("67d09a6f-ce95-498c-bf69-33c7d38f9041"), new Guid("0ac1701b-3d90-454c-80a9-7cf062109795") });

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "PortalAppLinking",
                keyColumn: "ID",
                keyValue: new Guid("8bfbd8af-5f27-4eb9-a508-be87a84c7963"),
                columns: new[] { "AppId", "PortalId" },
                values: new object[] { new Guid("8ded3502-01e5-469a-909b-5424d50d00d6"), new Guid("5a066e57-73c2-4fae-a7bb-0be3abdfb35c") });

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "PortalAppLinking",
                keyColumn: "ID",
                keyValue: new Guid("a4e51eea-81dc-4345-b9b1-76262b5c4f0e"),
                column: "PortalId",
                value: new Guid("31127b13-5be1-48b5-aa85-93a28682ef20"));

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "PortalAppLinking",
                keyColumn: "ID",
                keyValue: new Guid("caf59a3e-fb1e-4fb9-8129-9383230477bb"),
                column: "PortalId",
                value: new Guid("31127b13-5be1-48b5-aa85-93a28682ef20"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "ap",
                table: "Portal",
                keyColumn: "ID",
                keyValue: new Guid("0ac1701b-3d90-454c-80a9-7cf062109795"));

            migrationBuilder.DeleteData(
                schema: "ap",
                table: "Portal",
                keyColumn: "ID",
                keyValue: new Guid("31127b13-5be1-48b5-aa85-93a28682ef20"));

            migrationBuilder.DeleteData(
                schema: "ap",
                table: "Portal",
                keyColumn: "ID",
                keyValue: new Guid("5a066e57-73c2-4fae-a7bb-0be3abdfb35c"));

            migrationBuilder.InsertData(
                schema: "ap",
                table: "Portal",
                columns: new[] { "ID", "CreatedBy", "CreatedOn", "Deleted", "Name", "PortalKey", "UpdatedBy", "UpdatedOn", "UserType" },
                values: new object[,]
                {
                    { new Guid("8ded3502-01e5-469a-909b-5424d50d00d6"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "Platform Portal", "PlatPortal", new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 1 },
                    { new Guid("67d09a6f-ce95-498c-bf69-33c7d38f9041"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "Publisher Portal", "PubPortal", new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 2 },
                    { new Guid("f4952ef3-f1bd-4621-a5f9-290fd09bc81b"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "Business Portal", "BizPortal", new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 3 }
                });

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "PortalAppLinking",
                keyColumn: "ID",
                keyValue: new Guid("0a826d78-14af-469f-8f78-9a7b068c574c"),
                columns: new[] { "AppId", "PortalId" },
                values: new object[] { new Guid("31127b13-5be1-48b5-aa85-93a28682ef20"), new Guid("f4952ef3-f1bd-4621-a5f9-290fd09bc81b") });

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "PortalAppLinking",
                keyColumn: "ID",
                keyValue: new Guid("7a128e85-c2e2-4650-85bd-cc433a4e532e"),
                column: "PortalId",
                value: new Guid("f4952ef3-f1bd-4621-a5f9-290fd09bc81b"));

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "PortalAppLinking",
                keyColumn: "ID",
                keyValue: new Guid("7c9a4607-9410-4e61-b4ab-f1d8df7305cc"),
                columns: new[] { "AppId", "PortalId" },
                values: new object[] { new Guid("0ac1701b-3d90-454c-80a9-7cf062109795"), new Guid("67d09a6f-ce95-498c-bf69-33c7d38f9041") });

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "PortalAppLinking",
                keyColumn: "ID",
                keyValue: new Guid("8bfbd8af-5f27-4eb9-a508-be87a84c7963"),
                columns: new[] { "AppId", "PortalId" },
                values: new object[] { new Guid("5a066e57-73c2-4fae-a7bb-0be3abdfb35c"), new Guid("8ded3502-01e5-469a-909b-5424d50d00d6") });

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "PortalAppLinking",
                keyColumn: "ID",
                keyValue: new Guid("a4e51eea-81dc-4345-b9b1-76262b5c4f0e"),
                column: "PortalId",
                value: new Guid("f4952ef3-f1bd-4621-a5f9-290fd09bc81b"));

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "PortalAppLinking",
                keyColumn: "ID",
                keyValue: new Guid("caf59a3e-fb1e-4fb9-8129-9383230477bb"),
                column: "PortalId",
                value: new Guid("f4952ef3-f1bd-4621-a5f9-290fd09bc81b"));
        }
    }
}
