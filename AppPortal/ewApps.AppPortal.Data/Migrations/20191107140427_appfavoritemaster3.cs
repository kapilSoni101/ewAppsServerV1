using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.AppPortal.Data.Migrations
{
    public partial class appfavoritemaster3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "ap",
                table: "Favorite",
                keyColumn: "ID",
                keyValue: new Guid("083972f2-8eeb-4a99-92f1-6097a18cb094"),
                column: "PortalKey",
                value: "BizPortal");

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "Favorite",
                keyColumn: "ID",
                keyValue: new Guid("4a5519b6-e2c9-42e2-9715-cae5af638b15"),
                column: "PortalKey",
                value: "BizPortal");

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "Favorite",
                keyColumn: "ID",
                keyValue: new Guid("4e5a3aa9-86bc-4dea-9acf-136678ef8cfe"),
                column: "PortalKey",
                value: "BizPortal");

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "Favorite",
                keyColumn: "ID",
                keyValue: new Guid("8120877e-3351-4f1b-924e-9934551f176e"),
                column: "PortalKey",
                value: "BizPortal");

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "Favorite",
                keyColumn: "ID",
                keyValue: new Guid("8b5bb0f6-6e57-4529-93bc-9a8b45651b73"),
                column: "PortalKey",
                value: "BizPortal");

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "Favorite",
                keyColumn: "ID",
                keyValue: new Guid("98f338c6-fada-4c44-8f09-298f4c6cc292"),
                column: "PortalKey",
                value: "CustPortal");

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "Favorite",
                keyColumn: "ID",
                keyValue: new Guid("9d74d0f6-d07c-4a90-884f-92e454c78349"),
                column: "PortalKey",
                value: "CustPortal");

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "Favorite",
                keyColumn: "ID",
                keyValue: new Guid("b2bd694f-004e-42c3-bad8-3478c37bcec4"),
                column: "PortalKey",
                value: "CustPortal");

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "Favorite",
                keyColumn: "ID",
                keyValue: new Guid("c4285a44-32e9-4c4a-b186-843b6c31b4fb"),
                column: "PortalKey",
                value: "BizPortal");

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "Favorite",
                keyColumn: "ID",
                keyValue: new Guid("c75c4053-8b86-445b-819f-0fbe2028b74b"),
                column: "PortalKey",
                value: "CustPortal");

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "Favorite",
                keyColumn: "ID",
                keyValue: new Guid("c810c5d9-a559-4242-89a7-7283adb4ca7a"),
                column: "PortalKey",
                value: "BizPortal");

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "Favorite",
                keyColumn: "ID",
                keyValue: new Guid("eeec0c47-9c5e-4bde-914b-39a3e3a387ee"),
                column: "PortalKey",
                value: "BizPortal");

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "Favorite",
                keyColumn: "ID",
                keyValue: new Guid("fcd4ccc7-eeba-49de-a1aa-83f1cd57a3e8"),
                column: "PortalKey",
                value: "CustPortal");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "ap",
                table: "Favorite",
                keyColumn: "ID",
                keyValue: new Guid("083972f2-8eeb-4a99-92f1-6097a18cb094"),
                column: "PortalKey",
                value: "pay");

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "Favorite",
                keyColumn: "ID",
                keyValue: new Guid("4a5519b6-e2c9-42e2-9715-cae5af638b15"),
                column: "PortalKey",
                value: "cust");

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "Favorite",
                keyColumn: "ID",
                keyValue: new Guid("4e5a3aa9-86bc-4dea-9acf-136678ef8cfe"),
                column: "PortalKey",
                value: "cust");

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "Favorite",
                keyColumn: "ID",
                keyValue: new Guid("8120877e-3351-4f1b-924e-9934551f176e"),
                column: "PortalKey",
                value: "cust");

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "Favorite",
                keyColumn: "ID",
                keyValue: new Guid("8b5bb0f6-6e57-4529-93bc-9a8b45651b73"),
                column: "PortalKey",
                value: "transaction/ refund");

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "Favorite",
                keyColumn: "ID",
                keyValue: new Guid("98f338c6-fada-4c44-8f09-298f4c6cc292"),
                column: "PortalKey",
                value: "pay");

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "Favorite",
                keyColumn: "ID",
                keyValue: new Guid("9d74d0f6-d07c-4a90-884f-92e454c78349"),
                column: "PortalKey",
                value: "cust");

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "Favorite",
                keyColumn: "ID",
                keyValue: new Guid("b2bd694f-004e-42c3-bad8-3478c37bcec4"),
                column: "PortalKey",
                value: "cust");

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "Favorite",
                keyColumn: "ID",
                keyValue: new Guid("c4285a44-32e9-4c4a-b186-843b6c31b4fb"),
                column: "PortalKey",
                value: "cust");

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "Favorite",
                keyColumn: "ID",
                keyValue: new Guid("c75c4053-8b86-445b-819f-0fbe2028b74b"),
                column: "PortalKey",
                value: "cust");

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "Favorite",
                keyColumn: "ID",
                keyValue: new Guid("c810c5d9-a559-4242-89a7-7283adb4ca7a"),
                column: "PortalKey",
                value: "pay");

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "Favorite",
                keyColumn: "ID",
                keyValue: new Guid("eeec0c47-9c5e-4bde-914b-39a3e3a387ee"),
                column: "PortalKey",
                value: "pay");

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "Favorite",
                keyColumn: "ID",
                keyValue: new Guid("fcd4ccc7-eeba-49de-a1aa-83f1cd57a3e8"),
                column: "PortalKey",
                value: "pay");
        }
    }
}
