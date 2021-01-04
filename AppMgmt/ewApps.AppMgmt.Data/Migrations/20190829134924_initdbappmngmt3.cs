using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.AppMgmt.Data.Migrations
{
    public partial class initdbappmngmt3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Role",
                schema: "am");

            migrationBuilder.DropTable(
                name: "RoleLinking",
                schema: "am");

            migrationBuilder.DeleteData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("0ac1701b-3d90-454c-80a9-7cf062109795"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("31127b13-5be1-48b5-aa85-93a28682ef20"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("5a066e57-73c2-4fae-a7bb-0be3abdfb35c"));

            migrationBuilder.InsertData(
                schema: "am",
                table: "App",
                columns: new[] { "ID", "Active", "AppKey", "AppScope", "AppSubscriptionMode", "Constructed", "CreatedBy", "CreatedOn", "Deleted", "IdentityNumber", "InactiveComment", "Name", "ThemeId", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { new Guid("f4952ef3-f1bd-4621-a5f9-290fd09bc81b"), true, "Biz", 1, 1, true, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "APP100011", "", "BusinessSetUp App", new Guid("46e3cf6c-6cd6-7c12-ed0e-1e906dfe5560"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("67d09a6f-ce95-498c-bf69-33c7d38f9041"), true, "Pub", 1, 1, true, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "APP100012", "", "Publisher App", new Guid("46e3cf6c-6cd6-7c12-ed0e-1e906dfe5560"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("8ded3502-01e5-469a-909b-5424d50d00d6"), true, "Plat", 1, 1, true, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "APP100013", "", "Platform App", new Guid("46e3cf6c-6cd6-7c12-ed0e-1e906dfe5560"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) }
                });

            migrationBuilder.UpdateData(
                schema: "am",
                table: "SubscriptionPlan",
                keyColumn: "ID",
                keyValue: new Guid("29f50252-433f-4de9-8a4b-7a2360b4ee44"),
                columns: new[] { "CreatedBy", "UpdatedBy" },
                values: new object[] { new Guid("dab7edb0-cd13-4089-a96e-c52d76db4ba8"), new Guid("49a36f27-c493-4f1e-94b7-6f9c660bc3ec") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("67d09a6f-ce95-498c-bf69-33c7d38f9041"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("8ded3502-01e5-469a-909b-5424d50d00d6"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("f4952ef3-f1bd-4621-a5f9-290fd09bc81b"));

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "am",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    AppId = table.Column<Guid>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    PermissionBitMask = table.Column<long>(nullable: false),
                    RoleKey = table.Column<string>(maxLength: 100, nullable: false),
                    RoleName = table.Column<string>(maxLength: 100, nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    UserType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RoleLinking",
                schema: "am",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    AppId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    TenantUserId = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleLinking", x => x.ID);
                });

            migrationBuilder.InsertData(
                schema: "am",
                table: "App",
                columns: new[] { "ID", "Active", "AppKey", "AppScope", "AppSubscriptionMode", "Constructed", "CreatedBy", "CreatedOn", "Deleted", "IdentityNumber", "InactiveComment", "Name", "ThemeId", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { new Guid("31127b13-5be1-48b5-aa85-93a28682ef20"), true, "Biz", 1, 1, true, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "APP100011", "", "BusinessSetUp App", new Guid("46e3cf6c-6cd6-7c12-ed0e-1e906dfe5560"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("0ac1701b-3d90-454c-80a9-7cf062109795"), true, "Pub", 1, 1, true, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "APP100012", "", "Publisher App", new Guid("46e3cf6c-6cd6-7c12-ed0e-1e906dfe5560"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("5a066e57-73c2-4fae-a7bb-0be3abdfb35c"), true, "Plat", 1, 1, true, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "APP100013", "", "Platform App", new Guid("46e3cf6c-6cd6-7c12-ed0e-1e906dfe5560"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                schema: "am",
                table: "Role",
                columns: new[] { "ID", "Active", "AppId", "CreatedBy", "CreatedOn", "Deleted", "PermissionBitMask", "RoleKey", "RoleName", "UpdatedBy", "UpdatedOn", "UserType" },
                values: new object[,]
                {
                    { new Guid("df435b1a-7dbd-4e8f-b0f9-c07ba6e84f80"), true, new Guid("8ded3502-01e5-469a-909b-5424d50d00d6"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, 511L, "admin", "Admin", new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 1 },
                    { new Guid("fb8be172-2c7f-4f04-b40a-487fda92e323"), true, new Guid("67d09a6f-ce95-498c-bf69-33c7d38f9041"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, 4095L, "admin", "Admin", new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 2 },
                    { new Guid("958d4943-124b-4467-b345-395dcd37e2fe"), true, new Guid("f4952ef3-f1bd-4621-a5f9-290fd09bc81b"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, 3L, "admin", "Admin", new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 3 },
                    { new Guid("999e9919-ab9e-4e69-bfb0-d6283efa4338"), true, new Guid("2b031b59-5276-589c-9d75-2a7ae1c799c8"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, 1023L, "admin", "Admin", new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 3 },
                    { new Guid("2f1d98f5-f200-4861-bd80-62dd0e6af24c"), true, new Guid("2b031b59-5276-589c-9d75-2a7ae1c799c8"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, 63L, "admin", "Admin", new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 4 }
                });

            migrationBuilder.InsertData(
                schema: "am",
                table: "RoleLinking",
                columns: new[] { "ID", "AppId", "CreatedBy", "CreatedOn", "Deleted", "RoleId", "TenantId", "TenantUserId", "UpdatedBy", "UpdatedOn" },
                values: new object[] { new Guid("0b03bbe7-b3f7-4978-9411-2b9cb8976579"), new Guid("8ded3502-01e5-469a-909b-5424d50d00d6"), new Guid("56b5fb38-838c-4086-9c6e-b5b3f571de0c"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, new Guid("df435b1a-7dbd-4e8f-b0f9-c07ba6e84f80"), new Guid("18571765-24b5-4c36-a957-416eaec38fda"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new Guid("e330041f-ed94-46bd-b553-a33984a7ead5"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                schema: "am",
                table: "SubscriptionPlan",
                keyColumn: "ID",
                keyValue: new Guid("29f50252-433f-4de9-8a4b-7a2360b4ee44"),
                columns: new[] { "CreatedBy", "UpdatedBy" },
                values: new object[] { new Guid("0d6dffa2-c0ff-420c-a457-78d4bda103a2"), new Guid("17dce19c-5058-4bc4-bb08-d2d57352ca5e") });
        }
    }
}
