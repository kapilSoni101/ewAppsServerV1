using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.AppPortal.Data.Migrations
{
    public partial class initdbasdx1234 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
           

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "PortalAppLinking",
                keyColumn: "ID",
                keyValue: new Guid("2657f722-c6dd-44b7-8c3b-432ecf797cdc"),
                column: "AppId",
                value: new Guid("3252c1cf-c74a-4d0d-b0ce-a6271aefc0a2"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "PortalAppLinking",
                keyColumn: "ID",
                keyValue: new Guid("2657f722-c6dd-44b7-8c3b-432ecf797cdc"),
                column: "AppId",
                value: new Guid("6118a7ff-742b-25db-a9c1-8e252c39bb73"));
        }
    }
}
