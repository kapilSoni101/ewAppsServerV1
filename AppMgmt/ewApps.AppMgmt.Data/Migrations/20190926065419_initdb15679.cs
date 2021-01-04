using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.AppMgmt.Data.Migrations
{
    public partial class initdb15679 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder) {
           

            migrationBuilder.InsertData(
                schema: "am",
                table: "App",
                columns: new[] { "ID", "Active", "AppKey", "AppScope", "AppSubscriptionMode", "Constructed", "CreatedBy", "CreatedOn", "Deleted", "IdentityNumber", "InactiveComment", "Name", "ThemeId", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { new Guid("2b031b59-5276-589c-9d75-2a7ae1c799c8"), true, "Pay", 1, 2, true, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "APP100001", "", "iSmartPayment", new Guid("46e3cf6c-6cd6-7c12-ed0e-1e906dfe5560"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("f6ad4bf0-c302-4eb1-ab13-35ff9a832add"), true, "BankPay", 2, 2, true, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "APP100015", "", "BankPayment", new Guid("46e3cf6c-6cd6-7c12-ed0e-1e906dfe5560"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("8ded3502-01e5-469a-909b-5424d50d00d6"), true, "Plat", 1, 1, true, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "APP100014", "", "Platform App", new Guid("46e3cf6c-6cd6-7c12-ed0e-1e906dfe5560"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("67d09a6f-ce95-498c-bf69-33c7d38f9041"), true, "Pub", 1, 1, true, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "APP100013", "", "Publisher App", new Guid("46e3cf6c-6cd6-7c12-ed0e-1e906dfe5560"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("3252c1cf-c74a-4d0d-b0ce-a6271aefc0a2"), true, "custsetup", 1, 1, true, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "APP100012", "", "CustomerSetUp App", new Guid("46e3cf6c-6cd6-7c12-ed0e-1e906dfe5560"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("71d183b4-b17b-4458-92c9-a545ff775c13"), true, "Vendor", 1, 2, false, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "APP100010", "", "Vendor App", new Guid("46e3cf6c-6cd6-7c12-ed0e-1e906dfe5560"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("8c6fa8ce-6b94-428f-95de-5ed8859260d2"), true, "Cust", 1, 2, true, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "APP100009", "", "Customer App", new Guid("46e3cf6c-6cd6-7c12-ed0e-1e906dfe5560"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("f4952ef3-f1bd-4621-a5f9-290fd09bc81b"), true, "Biz", 1, 1, true, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "APP100011", "", "BusinessSetUp App", new Guid("46e3cf6c-6cd6-7c12-ed0e-1e906dfe5560"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("4b643cfc-26d4-41f4-abda-a656682ceb50"), true, "Fixed", 1, 2, false, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "APP100007", "", "Fixed Assets", new Guid("46e3cf6c-6cd6-7c12-ed0e-1e906dfe5560"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("a33315c5-fd8b-45cd-99df-fbbeb88597f6"), true, "Pos", 1, 2, false, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "APP100006", "", "POS", new Guid("46e3cf6c-6cd6-7c12-ed0e-1e906dfe5560"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("e4ecf0e7-1bc7-49d4-a95d-1bdb1b77715e"), true, "Report", 1, 2, false, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "APP100005", "", "Report and Dashboard", new Guid("46e3cf6c-6cd6-7c12-ed0e-1e906dfe5560"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("f629c9e6-93d8-4e6b-8050-26fd89e9af5c"), true, "Dsd", 1, 2, false, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "APP100004", "", "DSD", new Guid("46e3cf6c-6cd6-7c12-ed0e-1e906dfe5560"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("e5080257-c602-42cd-aedb-30b33757c382"), true, "Doc", 1, 2, false, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "APP100003", "", "Document", new Guid("46e3cf6c-6cd6-7c12-ed0e-1e906dfe5560"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("6118a7ff-742b-25db-a9c1-8e252c39bb73"), true, "Ship", 1, 2, true, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "APP100002", "", "iFastShipment", new Guid("46e3cf6c-6cd6-7c12-ed0e-1e906dfe5560"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("74d886db-ee46-41c2-acbe-9fdc7bd182fa"), true, "Crm", 1, 2, false, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "APP100008", "", "CRM", new Guid("46e3cf6c-6cd6-7c12-ed0e-1e906dfe5560"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) }
                });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("2b031b59-5276-589c-9d75-2a7ae1c799c8"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("3252c1cf-c74a-4d0d-b0ce-a6271aefc0a2"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("4b643cfc-26d4-41f4-abda-a656682ceb50"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("6118a7ff-742b-25db-a9c1-8e252c39bb73"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("67d09a6f-ce95-498c-bf69-33c7d38f9041"));

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
                keyValue: new Guid("8ded3502-01e5-469a-909b-5424d50d00d6"));

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
                keyValue: new Guid("f4952ef3-f1bd-4621-a5f9-290fd09bc81b"));

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

        }
    }
}
