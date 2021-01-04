using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.AppMgmt.Data.Migrations
{
    public partial class initdbappmngmt1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "State",
                schema: "am",
                table: "TenantSubscription",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<bool>(
                name: "CustomizeSubscription",
                schema: "am",
                table: "TenantSubscription",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "AutoRenew",
                schema: "am",
                table: "TenantSubscription",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                schema: "am",
                table: "Tenant",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool));

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "am",
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
                schema: "am",
                columns: table => new
                {
                    TenantId = table.Column<Guid>(nullable: false),
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantUserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    AppId = table.Column<Guid>(nullable: false)
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
                    { new Guid("2b031b59-5276-589c-9d75-2a7ae1c799c8"), true, "Pay", 1, 2, true, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "APP100001", "", "iSmartPayment", new Guid("46e3cf6c-6cd6-7c12-ed0e-1e906dfe5560"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("f6ad4bf0-c302-4eb1-ab13-35ff9a832add"), true, "BankPay", 2, 2, true, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "APP100014", "", "BankPayment", new Guid("46e3cf6c-6cd6-7c12-ed0e-1e906dfe5560"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("5a066e57-73c2-4fae-a7bb-0be3abdfb35c"), true, "Plat", 1, 1, true, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "APP100013", "", "Platform App", new Guid("46e3cf6c-6cd6-7c12-ed0e-1e906dfe5560"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("0ac1701b-3d90-454c-80a9-7cf062109795"), true, "Pub", 1, 1, true, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "APP100012", "", "Publisher App", new Guid("46e3cf6c-6cd6-7c12-ed0e-1e906dfe5560"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("31127b13-5be1-48b5-aa85-93a28682ef20"), true, "Biz", 1, 1, true, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "APP100011", "", "BusinessSetUp App", new Guid("46e3cf6c-6cd6-7c12-ed0e-1e906dfe5560"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("8c6fa8ce-6b94-428f-95de-5ed8859260d2"), true, "Cust", 1, 2, true, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "APP100009", "", "Customer App", new Guid("46e3cf6c-6cd6-7c12-ed0e-1e906dfe5560"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("74d886db-ee46-41c2-acbe-9fdc7bd182fa"), true, "Crm", 1, 2, false, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "APP100008", "", "CRM", new Guid("46e3cf6c-6cd6-7c12-ed0e-1e906dfe5560"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("71d183b4-b17b-4458-92c9-a545ff775c13"), true, "Vendor", 1, 2, false, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "APP100010", "", "Vendor App", new Guid("46e3cf6c-6cd6-7c12-ed0e-1e906dfe5560"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("a33315c5-fd8b-45cd-99df-fbbeb88597f6"), true, "Pos", 1, 2, false, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "APP100006", "", "POS", new Guid("46e3cf6c-6cd6-7c12-ed0e-1e906dfe5560"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("e4ecf0e7-1bc7-49d4-a95d-1bdb1b77715e"), true, "Report", 1, 2, false, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "APP100005", "", "Report and Dashboard", new Guid("46e3cf6c-6cd6-7c12-ed0e-1e906dfe5560"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("f629c9e6-93d8-4e6b-8050-26fd89e9af5c"), true, "Dsd", 1, 2, false, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "APP100004", "", "DSD", new Guid("46e3cf6c-6cd6-7c12-ed0e-1e906dfe5560"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("e5080257-c602-42cd-aedb-30b33757c382"), true, "Doc", 1, 2, false, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "APP100003", "", "Document", new Guid("46e3cf6c-6cd6-7c12-ed0e-1e906dfe5560"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("6118a7ff-742b-25db-a9c1-8e252c39bb73"), true, "Ship", 1, 2, true, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "APP100002", "", "iFastShipment", new Guid("46e3cf6c-6cd6-7c12-ed0e-1e906dfe5560"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("4b643cfc-26d4-41f4-abda-a656682ceb50"), true, "Fixed", 1, 2, false, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "APP100007", "", "Fixed Assets", new Guid("46e3cf6c-6cd6-7c12-ed0e-1e906dfe5560"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                schema: "am",
                table: "AppUserTypeLinking",
                columns: new[] { "ID", "AppId", "CreatedBy", "CreatedOn", "Deleted", "PartnerType", "UpdatedBy", "UpdatedOn", "UserType" },
                values: new object[,]
                {
                    { new Guid("c8ae2650-f7f5-44a4-ac52-851f01e8ed5d"), new Guid("2b031b59-5276-589c-9d75-2a7ae1c799c8"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, 0, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 4 },
                    { new Guid("58b5ab4b-3921-42f3-8d44-600398d7c68d"), new Guid("2b031b59-5276-589c-9d75-2a7ae1c799c8"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, 0, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 5 },
                    { new Guid("14a88b03-da38-422e-a58f-ed71e2243445"), new Guid("6118a7ff-742b-25db-a9c1-8e252c39bb73"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, 0, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 5 },
                    { new Guid("b9e705cd-d5a3-44a8-9672-e916871f8d38"), new Guid("71d183b4-b17b-4458-92c9-a545ff775c13"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, 0, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 5 },
                    { new Guid("67f40404-084c-4170-baca-9a6868f5b6c2"), new Guid("8c6fa8ce-6b94-428f-95de-5ed8859260d2"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, 0, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 4 },
                    { new Guid("809cc1eb-2234-46b7-9829-dd66edac9b60"), new Guid("6118a7ff-742b-25db-a9c1-8e252c39bb73"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, 0, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 4 },
                    { new Guid("87d0b552-8973-4f28-a383-532611c5db06"), new Guid("6118a7ff-742b-25db-a9c1-8e252c39bb73"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, 0, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 3 },
                    { new Guid("b21da138-837b-483a-bb95-0a7c19f02331"), new Guid("8c6fa8ce-6b94-428f-95de-5ed8859260d2"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, 0, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 3 },
                    { new Guid("f62a5cb5-36c9-43e7-8dfb-af92eb114712"), new Guid("31127b13-5be1-48b5-aa85-93a28682ef20"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, 0, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 3 },
                    { new Guid("6db9d8c9-2a8b-4b7c-8dba-cda7338d1424"), new Guid("0ac1701b-3d90-454c-80a9-7cf062109795"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, 0, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 2 },
                    { new Guid("9974f09e-ce64-460d-b1e9-7967dcc2ee00"), new Guid("5a066e57-73c2-4fae-a7bb-0be3abdfb35c"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, 0, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 1 },
                    { new Guid("b43d29e0-28ab-4c4b-8293-8ad90bc83f29"), new Guid("2b031b59-5276-589c-9d75-2a7ae1c799c8"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, 0, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 3 }
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

            migrationBuilder.InsertData(
                schema: "am",
                table: "SubscriptionPlan",
                columns: new[] { "ID", "Active", "AdditionalPerUserPrice", "AlertFrequency", "AllowCustomization", "AppId", "AutoRenewal", "CreatedBy", "CreatedOn", "Deleted", "FreeUserLicenseCount", "GracePeriodInDays", "IdentityNumber", "NumberOfUsers", "PaymentCycle", "PlanName", "PlanSchedule", "PriceInDollar", "System", "TenantId", "Term", "TrialPeriodInDays", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { new Guid("29f50252-433f-4de9-8a4b-7a2360b4ee44"), true, 0.0, 2, true, new Guid("2b031b59-5276-589c-9d75-2a7ae1c799c8"), true, new Guid("0d6dffa2-c0ff-420c-a457-78d4bda103a2"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, 1, 58, "SS#5", 1, 1, "Executive Plan", 1, 0m, false, new Guid("00000000-0000-0000-0000-000000000000"), 90, 30, new Guid("17dce19c-5058-4bc4-bb08-d2d57352ca5e"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("98276e8b-78eb-4b87-bfff-1d20d82b446f"), true, 0.0, 1, true, new Guid("2b031b59-5276-589c-9d75-2a7ae1c799c8"), true, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, 1, 58, "SS#4", 1, 1, "Trial Plan", 1, 0m, false, new Guid("00000000-0000-0000-0000-000000000000"), 15, 30, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("b37eb8b9-11bf-402e-9048-f9d0a43a426b"), true, 0.0, 1, true, new Guid("8ded3502-01e5-469a-909b-5424d50d00d6"), true, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, 1, 58, "SS#1", 1, 1, "Trial Plan", 1, 0m, false, new Guid("00000000-0000-0000-0000-000000000000"), 15, 30, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("d390a264-f0db-49ce-9415-daee1ed4b445"), true, 0.0, 1, true, new Guid("67d09a6f-ce95-498c-bf69-33c7d38f9041"), true, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, 1, 58, "SS#2", 1, 1, "Trial Plan", 1, 0m, false, new Guid("00000000-0000-0000-0000-000000000000"), 15, 30, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("8d5e65ca-1b62-4bc3-b0f0-9b3703aabebf"), true, 0.0, 1, true, new Guid("f4952ef3-f1bd-4621-a5f9-290fd09bc81b"), true, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, 1, 58, "SS#6", 1, 1, "Trial Plan", 1, 0m, false, new Guid("00000000-0000-0000-0000-000000000000"), 15, 30, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                schema: "am",
                table: "Tenant",
                columns: new[] { "ID", "Active", "CreatedBy", "CreatedOn", "Currency", "Deleted", "IdentityNumber", "InvitedBy", "InvitedOn", "Language", "LogoUrl", "Name", "SubDomainName", "TenantType", "TimeZone", "UpdatedBy", "UpdatedOn", "VarId", "joinedOn" },
                values: new object[] { new Guid("18571765-24b5-4c36-a957-416eaec38fda"), true, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), "$", false, "0", null, null, "en", "", "Platform", "69ABE04E-E9D7-499D-A3C1-2593ABEF8959", 1, "America/Los_Angeles", new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), "", null });

            migrationBuilder.InsertData(
                schema: "am",
                table: "TenantLinking",
                columns: new[] { "ID", "BusinessPartnerTenantId", "BusinessTenantId", "CreatedBy", "CreatedOn", "PlatformTenantId", "PublisherTenantId", "UpdatedBy", "UpdatedOn" },
                values: new object[] { new Guid("30b3a49f-ae51-4a67-b610-bfcb935ae77a"), null, null, new Guid("d899632c-a7ee-405b-aa71-0ad0779edfd2"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), new Guid("18571765-24b5-4c36-a957-416eaec38fda"), null, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                schema: "am",
                table: "TenantSubscription",
                columns: new[] { "ID", "AlertFrequency", "AppId", "AutoRenew", "CreatedBy", "CreatedOn", "CustomizeSubscription", "Deleted", "GracePeriodInDays", "InactiveComment", "LogoThumbnailId", "PaymentCycle", "PlanSchedule", "PriceInDollar", "State", "Status", "SubscriptionPlanId", "SubscriptionStartDate", "SubscriptionStartEnd", "SystemConfId", "TenantId", "ThemeId", "UpdatedBy", "UpdatedOn", "UserLicenses" },
                values: new object[] { new Guid("f524047f-1389-412f-904d-0492858c0194"), 4, new Guid("8ded3502-01e5-469a-909b-5424d50d00d6"), false, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, false, 58, "", new Guid("00000000-0000-0000-0000-000000000000"), 1, 1, 0.0, 1, 0, new Guid("b37eb8b9-11bf-402e-9048-f9d0a43a426b"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("18571765-24b5-4c36-a957-416eaec38fda"), new Guid("46e3cf6c-6cd6-7c12-ed0e-1e906dfe5560"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 15 });

            migrationBuilder.InsertData(
                schema: "am",
                table: "TenantUser",
                columns: new[] { "ID", "Code", "CreatedBy", "CreatedOn", "Deleted", "Email", "FirstName", "FullName", "IdentityNumber", "IdentityUserId", "LastName", "Phone", "UpdatedBy", "UpdatedOn" },
                values: new object[] { new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), null, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "hdudani@gmail.com", "Hari", "Hari Dudani", null, new Guid("d26eb1db-1c04-4a14-b922-e7cabbf1366f"), "Dudani", "+918965321451", new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                schema: "am",
                table: "TenantUserAppLinking",
                columns: new[] { "ID", "Active", "AppId", "BusinessPartnerTenantId", "CreatedBy", "CreatedOn", "Deleted", "InvitedBy", "InvitedOn", "JoinedDate", "Status", "TenantId", "TenantUserId", "UpdatedBy", "UpdatedOn", "UserType" },
                values: new object[] { new Guid("8260b697-e605-46e2-92e2-2f27935a8547"), true, new Guid("8ded3502-01e5-469a-909b-5424d50d00d6"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, null, new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 1, new Guid("18571765-24b5-4c36-a957-416eaec38fda"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 1 });

            migrationBuilder.InsertData(
                schema: "am",
                table: "Theme",
                columns: new[] { "ID", "Active", "CreatedBy", "CreatedOn", "Deleted", "PreviewImageUrl", "ThemeKey", "ThemeName", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { new Guid("2a438a5c-7ac6-41bd-66cb-9a771b60f9dd"), true, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "1", "light", "Light", new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("46e3cf6c-6cd6-7c12-ed0e-1e906dfe5560"), true, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "1", "business", "Business", new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("5270bc92-13fe-5299-0a24-752070424bc8"), true, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "1", "elegant", "Elegant", new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                schema: "am",
                table: "UserTenantLinking",
                columns: new[] { "ID", "BusinessPartnerTenantId", "CreatedBy", "CreatedOn", "Deleted", "IsPrimary", "PartnerType", "TenantId", "TenantUserId", "UpdatedBy", "UpdatedOn", "UserType" },
                values: new object[] { new Guid("601622f9-9e52-4dd7-acaa-b8d44087b486"), null, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, true, null, new Guid("18571765-24b5-4c36-a957-416eaec38fda"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                keyValue: new Guid("2b031b59-5276-589c-9d75-2a7ae1c799c8"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("31127b13-5be1-48b5-aa85-93a28682ef20"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("4b643cfc-26d4-41f4-abda-a656682ceb50"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("5a066e57-73c2-4fae-a7bb-0be3abdfb35c"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("6118a7ff-742b-25db-a9c1-8e252c39bb73"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("71d183b4-b17b-4458-92c9-a545ff775c13"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("74d886db-ee46-41c2-acbe-9fdc7bd182fa"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("8c6fa8ce-6b94-428f-95de-5ed8859260d2"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("a33315c5-fd8b-45cd-99df-fbbeb88597f6"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("e4ecf0e7-1bc7-49d4-a95d-1bdb1b77715e"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("e5080257-c602-42cd-aedb-30b33757c382"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("f629c9e6-93d8-4e6b-8050-26fd89e9af5c"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("f6ad4bf0-c302-4eb1-ab13-35ff9a832add"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "AppUserTypeLinking",
                keyColumn: "ID",
                keyValue: new Guid("14a88b03-da38-422e-a58f-ed71e2243445"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "AppUserTypeLinking",
                keyColumn: "ID",
                keyValue: new Guid("58b5ab4b-3921-42f3-8d44-600398d7c68d"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "AppUserTypeLinking",
                keyColumn: "ID",
                keyValue: new Guid("67f40404-084c-4170-baca-9a6868f5b6c2"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "AppUserTypeLinking",
                keyColumn: "ID",
                keyValue: new Guid("6db9d8c9-2a8b-4b7c-8dba-cda7338d1424"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "AppUserTypeLinking",
                keyColumn: "ID",
                keyValue: new Guid("809cc1eb-2234-46b7-9829-dd66edac9b60"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "AppUserTypeLinking",
                keyColumn: "ID",
                keyValue: new Guid("87d0b552-8973-4f28-a383-532611c5db06"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "AppUserTypeLinking",
                keyColumn: "ID",
                keyValue: new Guid("9974f09e-ce64-460d-b1e9-7967dcc2ee00"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "AppUserTypeLinking",
                keyColumn: "ID",
                keyValue: new Guid("b21da138-837b-483a-bb95-0a7c19f02331"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "AppUserTypeLinking",
                keyColumn: "ID",
                keyValue: new Guid("b43d29e0-28ab-4c4b-8293-8ad90bc83f29"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "AppUserTypeLinking",
                keyColumn: "ID",
                keyValue: new Guid("b9e705cd-d5a3-44a8-9672-e916871f8d38"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "AppUserTypeLinking",
                keyColumn: "ID",
                keyValue: new Guid("c8ae2650-f7f5-44a4-ac52-851f01e8ed5d"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "AppUserTypeLinking",
                keyColumn: "ID",
                keyValue: new Guid("f62a5cb5-36c9-43e7-8dfb-af92eb114712"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "SubscriptionPlan",
                keyColumn: "ID",
                keyValue: new Guid("29f50252-433f-4de9-8a4b-7a2360b4ee44"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "SubscriptionPlan",
                keyColumn: "ID",
                keyValue: new Guid("8d5e65ca-1b62-4bc3-b0f0-9b3703aabebf"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "SubscriptionPlan",
                keyColumn: "ID",
                keyValue: new Guid("98276e8b-78eb-4b87-bfff-1d20d82b446f"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "SubscriptionPlan",
                keyColumn: "ID",
                keyValue: new Guid("b37eb8b9-11bf-402e-9048-f9d0a43a426b"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "SubscriptionPlan",
                keyColumn: "ID",
                keyValue: new Guid("d390a264-f0db-49ce-9415-daee1ed4b445"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "Tenant",
                keyColumn: "ID",
                keyValue: new Guid("18571765-24b5-4c36-a957-416eaec38fda"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "TenantLinking",
                keyColumn: "ID",
                keyValue: new Guid("30b3a49f-ae51-4a67-b610-bfcb935ae77a"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "TenantSubscription",
                keyColumn: "ID",
                keyValue: new Guid("f524047f-1389-412f-904d-0492858c0194"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "TenantUser",
                keyColumn: "ID",
                keyValue: new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "TenantUserAppLinking",
                keyColumn: "ID",
                keyValue: new Guid("8260b697-e605-46e2-92e2-2f27935a8547"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "Theme",
                keyColumn: "ID",
                keyValue: new Guid("2a438a5c-7ac6-41bd-66cb-9a771b60f9dd"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "Theme",
                keyColumn: "ID",
                keyValue: new Guid("46e3cf6c-6cd6-7c12-ed0e-1e906dfe5560"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "Theme",
                keyColumn: "ID",
                keyValue: new Guid("5270bc92-13fe-5299-0a24-752070424bc8"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "UserTenantLinking",
                keyColumn: "ID",
                keyValue: new Guid("601622f9-9e52-4dd7-acaa-b8d44087b486"));

            migrationBuilder.AlterColumn<int>(
                name: "State",
                schema: "am",
                table: "TenantSubscription",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<bool>(
                name: "CustomizeSubscription",
                schema: "am",
                table: "TenantSubscription",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "AutoRenew",
                schema: "am",
                table: "TenantSubscription",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                schema: "am",
                table: "Tenant",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: true);
        }
    }
}
