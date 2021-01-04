using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.AppMgmt.Data.Migrations
{
    public partial class initdb15680 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("2b031b59-5276-589c-9d75-2a7ae1c799c8"),
                column: "AppKey",
                value: "pay");

            migrationBuilder.UpdateData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("4b643cfc-26d4-41f4-abda-a656682ceb50"),
                column: "AppKey",
                value: "fixed");

            migrationBuilder.UpdateData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("6118a7ff-742b-25db-a9c1-8e252c39bb73"),
                column: "AppKey",
                value: "ship");

            migrationBuilder.UpdateData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("67d09a6f-ce95-498c-bf69-33c7d38f9041"),
                column: "AppKey",
                value: "pub");

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
                keyValue: new Guid("74d886db-ee46-41c2-acbe-9fdc7bd182fa"),
                column: "AppKey",
                value: "crm");

            migrationBuilder.UpdateData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("8c6fa8ce-6b94-428f-95de-5ed8859260d2"),
                column: "AppKey",
                value: "cust");

            migrationBuilder.UpdateData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("8ded3502-01e5-469a-909b-5424d50d00d6"),
                column: "AppKey",
                value: "plat");

            migrationBuilder.UpdateData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("a33315c5-fd8b-45cd-99df-fbbeb88597f6"),
                column: "AppKey",
                value: "pos");

            migrationBuilder.UpdateData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("e4ecf0e7-1bc7-49d4-a95d-1bdb1b77715e"),
                column: "AppKey",
                value: "report");

            migrationBuilder.UpdateData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("e5080257-c602-42cd-aedb-30b33757c382"),
                column: "AppKey",
                value: "doc");

            migrationBuilder.UpdateData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("f4952ef3-f1bd-4621-a5f9-290fd09bc81b"),
                column: "AppKey",
                value: "biz");

            migrationBuilder.UpdateData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("f629c9e6-93d8-4e6b-8050-26fd89e9af5c"),
                column: "AppKey",
                value: "dsd");

            migrationBuilder.UpdateData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("f6ad4bf0-c302-4eb1-ab13-35ff9a832add"),
                column: "AppKey",
                value: "bankpay");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("2b031b59-5276-589c-9d75-2a7ae1c799c8"),
                column: "AppKey",
                value: "Pay");

            migrationBuilder.UpdateData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("4b643cfc-26d4-41f4-abda-a656682ceb50"),
                column: "AppKey",
                value: "Fixed");

            migrationBuilder.UpdateData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("6118a7ff-742b-25db-a9c1-8e252c39bb73"),
                column: "AppKey",
                value: "Ship");

            migrationBuilder.UpdateData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("67d09a6f-ce95-498c-bf69-33c7d38f9041"),
                column: "AppKey",
                value: "Pub");

            migrationBuilder.UpdateData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("71d183b4-b17b-4458-92c9-a545ff775c13"),
                column: "AppKey",
                value: "Vendor");

            migrationBuilder.UpdateData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("74d886db-ee46-41c2-acbe-9fdc7bd182fa"),
                column: "AppKey",
                value: "Crm");

            migrationBuilder.UpdateData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("8c6fa8ce-6b94-428f-95de-5ed8859260d2"),
                column: "AppKey",
                value: "Cust");

            migrationBuilder.UpdateData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("8ded3502-01e5-469a-909b-5424d50d00d6"),
                column: "AppKey",
                value: "Plat");

            migrationBuilder.UpdateData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("a33315c5-fd8b-45cd-99df-fbbeb88597f6"),
                column: "AppKey",
                value: "Pos");

            migrationBuilder.UpdateData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("e4ecf0e7-1bc7-49d4-a95d-1bdb1b77715e"),
                column: "AppKey",
                value: "Report");

            migrationBuilder.UpdateData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("e5080257-c602-42cd-aedb-30b33757c382"),
                column: "AppKey",
                value: "Doc");

            migrationBuilder.UpdateData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("f4952ef3-f1bd-4621-a5f9-290fd09bc81b"),
                column: "AppKey",
                value: "Biz");

            migrationBuilder.UpdateData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("f629c9e6-93d8-4e6b-8050-26fd89e9af5c"),
                column: "AppKey",
                value: "Dsd");

            migrationBuilder.UpdateData(
                schema: "am",
                table: "App",
                keyColumn: "ID",
                keyValue: new Guid("f6ad4bf0-c302-4eb1-ab13-35ff9a832add"),
                column: "AppKey",
                value: "BankPay");
        }
    }
}
