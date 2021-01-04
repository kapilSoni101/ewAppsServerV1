using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.AppMgmt.Data.Migrations
{
    public partial class addportaldb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.UpdateData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("71d183b4-b17b-4458-92c9-a545ff775c13"),
                column: "AppKey",
                value: "vend");

            migrationBuilder.UpdateData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("f6ad4bf0-c302-4eb1-ab13-35ff9a832add"),
                column: "IdentityNumber",
                value: "APP100016");

            migrationBuilder.InsertData(
                schema: "am",
                table: "App",
                columns: new[] { "ID", "Active", "AppKey", "AppScope", "AppSubscriptionMode", "Constructed", "CreatedBy", "CreatedOn", "Deleted", "IdentityNumber", "InactiveComment", "Name", "ThemeId", "UpdatedBy", "UpdatedOn" },
                values: new object[] { new Guid("283259b7-952c-4f9b-9399-16a28ed08580"), true, "vendsetup", 1, 1, true, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "APP100015", "", "VendorSetUp App", new Guid("46e3cf6c-6cd6-7c12-ed0e-1e906dfe5560"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                schema: "am",
                table: "AppUserTypeLinking",
                columns: new[] { "ID", "AppId", "CreatedBy", "CreatedOn", "Deleted", "PartnerType", "UpdatedBy", "UpdatedOn", "UserType" },
                values: new object[] { new Guid("51ac7ff1-083b-4def-8ff4-ee89e2e98041"), new Guid("71d183b4-b17b-4458-92c9-a545ff775c13"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, 0, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 3 });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("283259b7-952c-4f9b-9399-16a28ed08580"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "AppUserTypeLinking",
                keyColumn: "ID",
                keyValue: new Guid("51ac7ff1-083b-4def-8ff4-ee89e2e98041"));

            

            migrationBuilder.UpdateData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("71d183b4-b17b-4458-92c9-a545ff775c13"),
                column: "AppKey",
                value: "vendor");

            migrationBuilder.UpdateData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("f6ad4bf0-c302-4eb1-ab13-35ff9a832add"),
                column: "IdentityNumber",
                value: "APP100015");
        }
    }
}
