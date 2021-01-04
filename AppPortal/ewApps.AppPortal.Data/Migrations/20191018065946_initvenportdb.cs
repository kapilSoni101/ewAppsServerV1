using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.AppPortal.Data.Migrations
{
    public partial class initvenportdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "ap",
                table: "PortalAppLinking",
                keyColumn: "ID",
                keyValue: new Guid("e1fbd5df-69e7-48d3-92ee-40f7fd9d2c99"));

           

          

           

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "Portal",
                keyColumn: "ID",
                keyValue: new Guid("929632bc-0327-4b35-bbe6-cb7a2bfbc3bd"),
                column: "PortalKey",
                value: "VendPortal");

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "PortalAppLinking",
                keyColumn: "ID",
                keyValue: new Guid("a8e9a7c5-57d1-43a4-afd3-e70a8ce59608"),
                column: "AppId",
                value: new Guid("283259b7-952c-4f9b-9399-16a28ed08580"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "Portal",
                keyColumn: "ID",
                keyValue: new Guid("929632bc-0327-4b35-bbe6-cb7a2bfbc3bd"),
                column: "PortalKey",
                value: "VendorPortal");

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "PortalAppLinking",
                keyColumn: "ID",
                keyValue: new Guid("a8e9a7c5-57d1-43a4-afd3-e70a8ce59608"),
                column: "AppId",
                value: new Guid("6118a7ff-742b-25db-a9c1-8e252c39bb73"));

            migrationBuilder.InsertData(
                schema: "ap",
                table: "PortalAppLinking",
                columns: new[] { "ID", "AppId", "CreatedBy", "CreatedOn", "Deleted", "PortalId", "UpdatedBy", "UpdatedOn" },
                values: new object[] { new Guid("e1fbd5df-69e7-48d3-92ee-40f7fd9d2c99"), new Guid("2b031b59-5276-589c-9d75-2a7ae1c799c8"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, new Guid("929632bc-0327-4b35-bbe6-cb7a2bfbc3bd"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) });
        }
    }
}
