using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.AppPortal.Data.Migrations
{
    public partial class initappportaldb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                schema: "ap",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    RoleName = table.Column<string>(maxLength: 100, nullable: false),
                    RoleKey = table.Column<string>(maxLength: 100, nullable: false),
                    PermissionBitMask = table.Column<long>(nullable: false),
                    AppId = table.Column<Guid>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    UserType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RoleLinking",
                schema: "ap",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    TenantUserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    AppId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleLinking", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Support",
                schema: "ap",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    SupportId = table.Column<Guid>(nullable: false),
                    AppId = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    TicketNo = table.Column<string>(nullable: true),
                    Portaltype = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    PhoneNo = table.Column<string>(nullable: true),
                    IssueDesc = table.Column<string>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Support", x => x.ID);
                });

            migrationBuilder.InsertData(
                schema: "ap",
                table: "Portal",
                columns: new[] { "ID", "CreatedBy", "CreatedOn", "Deleted", "Name", "PortalKey", "UpdatedBy", "UpdatedOn", "UserType" },
                values: new object[,]
                {
                    { new Guid("8ded3502-01e5-469a-909b-5424d50d00d6"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "Platform Portal", "PlatPortal", new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 1 },
                    { new Guid("67d09a6f-ce95-498c-bf69-33c7d38f9041"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "Publisher Portal", "PubPortal", new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 2 },
                    { new Guid("f4952ef3-f1bd-4621-a5f9-290fd09bc81b"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "Business Portal", "BizPortal", new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 3 },
                    { new Guid("0919810e-536c-42f5-a130-1cb62605508d"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "Customer Portal", "CustPortal", new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 4 },
                    { new Guid("929632bc-0327-4b35-bbe6-cb7a2bfbc3bd"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "Vendor Portal", "VendorPortal", new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 5 },
                    { new Guid("0fe68284-fbc8-472a-8efc-b7914c89a5e1"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "Employee Portal", "EmployeePortal", new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 6 }
                });

            migrationBuilder.InsertData(
                schema: "ap",
                table: "PortalAppLinking",
                columns: new[] { "ID", "AppId", "CreatedBy", "CreatedOn", "Deleted", "PortalId", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { new Guid("a8e9a7c5-57d1-43a4-afd3-e70a8ce59608"), new Guid("6118a7ff-742b-25db-a9c1-8e252c39bb73"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, new Guid("929632bc-0327-4b35-bbe6-cb7a2bfbc3bd"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("5d404aad-7235-41f1-b760-bd414ed9e3fa"), new Guid("71d183b4-b17b-4458-92c9-a545ff775c13"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, new Guid("929632bc-0327-4b35-bbe6-cb7a2bfbc3bd"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("2657f722-c6dd-44b7-8c3b-432ecf797cdc"), new Guid("6118a7ff-742b-25db-a9c1-8e252c39bb73"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, new Guid("0919810e-536c-42f5-a130-1cb62605508d"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("4af1430a-468f-4e5b-9786-9e542dc9a14a"), new Guid("2b031b59-5276-589c-9d75-2a7ae1c799c8"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, new Guid("0919810e-536c-42f5-a130-1cb62605508d"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("803ea4b6-f380-4d9f-97b0-82faa212f48b"), new Guid("8c6fa8ce-6b94-428f-95de-5ed8859260d2"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, new Guid("0919810e-536c-42f5-a130-1cb62605508d"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("7a128e85-c2e2-4650-85bd-cc433a4e532e"), new Guid("8c6fa8ce-6b94-428f-95de-5ed8859260d2"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, new Guid("f4952ef3-f1bd-4621-a5f9-290fd09bc81b"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("caf59a3e-fb1e-4fb9-8129-9383230477bb"), new Guid("6118a7ff-742b-25db-a9c1-8e252c39bb73"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, new Guid("f4952ef3-f1bd-4621-a5f9-290fd09bc81b"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("e1fbd5df-69e7-48d3-92ee-40f7fd9d2c99"), new Guid("2b031b59-5276-589c-9d75-2a7ae1c799c8"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, new Guid("929632bc-0327-4b35-bbe6-cb7a2bfbc3bd"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("0a826d78-14af-469f-8f78-9a7b068c574c"), new Guid("31127b13-5be1-48b5-aa85-93a28682ef20"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, new Guid("f4952ef3-f1bd-4621-a5f9-290fd09bc81b"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("7c9a4607-9410-4e61-b4ab-f1d8df7305cc"), new Guid("0ac1701b-3d90-454c-80a9-7cf062109795"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, new Guid("67d09a6f-ce95-498c-bf69-33c7d38f9041"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("8bfbd8af-5f27-4eb9-a508-be87a84c7963"), new Guid("5a066e57-73c2-4fae-a7bb-0be3abdfb35c"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, new Guid("8ded3502-01e5-469a-909b-5424d50d00d6"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("a4e51eea-81dc-4345-b9b1-76262b5c4f0e"), new Guid("2b031b59-5276-589c-9d75-2a7ae1c799c8"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, new Guid("f4952ef3-f1bd-4621-a5f9-290fd09bc81b"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                schema: "ap",
                table: "TenantUserAppPreference",
                columns: new[] { "ID", "AppId", "CreatedBy", "CreatedOn", "Deleted", "EmailPreference", "SMSPreference", "TenantId", "TenantUserId", "UpdatedBy", "UpdatedOn" },
                values: new object[] { new Guid("c31433d7-107d-4fc0-b30d-baaf22112635"), new Guid("8ded3502-01e5-469a-909b-5424d50d00d6"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, 31L, 0L, new Guid("18571765-24b5-4c36-a957-416eaec38fda"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Role",
                schema: "ap");

            migrationBuilder.DropTable(
                name: "RoleLinking",
                schema: "ap");

            migrationBuilder.DropTable(
                name: "Support",
                schema: "ap");

            migrationBuilder.DeleteData(
                schema: "ap",
                table: "Portal",
                keyColumn: "ID",
                keyValue: new Guid("0919810e-536c-42f5-a130-1cb62605508d"));

            migrationBuilder.DeleteData(
                schema: "ap",
                table: "Portal",
                keyColumn: "ID",
                keyValue: new Guid("0fe68284-fbc8-472a-8efc-b7914c89a5e1"));

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
                keyValue: new Guid("929632bc-0327-4b35-bbe6-cb7a2bfbc3bd"));

            migrationBuilder.DeleteData(
                schema: "ap",
                table: "Portal",
                keyColumn: "ID",
                keyValue: new Guid("f4952ef3-f1bd-4621-a5f9-290fd09bc81b"));

            migrationBuilder.DeleteData(
                schema: "ap",
                table: "PortalAppLinking",
                keyColumn: "ID",
                keyValue: new Guid("0a826d78-14af-469f-8f78-9a7b068c574c"));

            migrationBuilder.DeleteData(
                schema: "ap",
                table: "PortalAppLinking",
                keyColumn: "ID",
                keyValue: new Guid("2657f722-c6dd-44b7-8c3b-432ecf797cdc"));

            migrationBuilder.DeleteData(
                schema: "ap",
                table: "PortalAppLinking",
                keyColumn: "ID",
                keyValue: new Guid("4af1430a-468f-4e5b-9786-9e542dc9a14a"));

            migrationBuilder.DeleteData(
                schema: "ap",
                table: "PortalAppLinking",
                keyColumn: "ID",
                keyValue: new Guid("5d404aad-7235-41f1-b760-bd414ed9e3fa"));

            migrationBuilder.DeleteData(
                schema: "ap",
                table: "PortalAppLinking",
                keyColumn: "ID",
                keyValue: new Guid("7a128e85-c2e2-4650-85bd-cc433a4e532e"));

            migrationBuilder.DeleteData(
                schema: "ap",
                table: "PortalAppLinking",
                keyColumn: "ID",
                keyValue: new Guid("7c9a4607-9410-4e61-b4ab-f1d8df7305cc"));

            migrationBuilder.DeleteData(
                schema: "ap",
                table: "PortalAppLinking",
                keyColumn: "ID",
                keyValue: new Guid("803ea4b6-f380-4d9f-97b0-82faa212f48b"));

            migrationBuilder.DeleteData(
                schema: "ap",
                table: "PortalAppLinking",
                keyColumn: "ID",
                keyValue: new Guid("8bfbd8af-5f27-4eb9-a508-be87a84c7963"));

            migrationBuilder.DeleteData(
                schema: "ap",
                table: "PortalAppLinking",
                keyColumn: "ID",
                keyValue: new Guid("a4e51eea-81dc-4345-b9b1-76262b5c4f0e"));

            migrationBuilder.DeleteData(
                schema: "ap",
                table: "PortalAppLinking",
                keyColumn: "ID",
                keyValue: new Guid("a8e9a7c5-57d1-43a4-afd3-e70a8ce59608"));

            migrationBuilder.DeleteData(
                schema: "ap",
                table: "PortalAppLinking",
                keyColumn: "ID",
                keyValue: new Guid("caf59a3e-fb1e-4fb9-8129-9383230477bb"));

            migrationBuilder.DeleteData(
                schema: "ap",
                table: "PortalAppLinking",
                keyColumn: "ID",
                keyValue: new Guid("e1fbd5df-69e7-48d3-92ee-40f7fd9d2c99"));

            migrationBuilder.DeleteData(
                schema: "ap",
                table: "TenantUserAppPreference",
                keyColumn: "ID",
                keyValue: new Guid("c31433d7-107d-4fc0-b30d-baaf22112635"));
        }
    }
}
