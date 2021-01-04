using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.BusinessEntity.Data.Migrations
{
    public partial class init54633 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.InsertData(
                schema: "be",
                table: "ERPConnector",
                columns: new[] { "ID", "Active", "ConnectorKey", "CreatedBy", "CreatedOn", "Deleted", "Name", "TenantId", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { new Guid("706200a5-bdaa-482e-ad17-8fc6fc39c8b3"), false, "acumatica", new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "Acumatica", new Guid("18571765-24b5-4c36-a957-416eaec38fda"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("7d43c277-2d8a-4e68-aace-6cc8af26bf9b"), false, "batchmaster", new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "BatchMaster", new Guid("18571765-24b5-4c36-a957-416eaec38fda"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("a140ddc9-2bbf-477c-9052-c285118c2326"), false, "optiproeRP", new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "OptiPro ERP", new Guid("18571765-24b5-4c36-a957-416eaec38fda"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "be",
                table: "ERPConnector",
                keyColumn: "ID",
                keyValue: new Guid("706200a5-bdaa-482e-ad17-8fc6fc39c8b3"));

            migrationBuilder.DeleteData(
                schema: "be",
                table: "ERPConnector",
                keyColumn: "ID",
                keyValue: new Guid("7d43c277-2d8a-4e68-aace-6cc8af26bf9b"));

            migrationBuilder.DeleteData(
                schema: "be",
                table: "ERPConnector",
                keyColumn: "ID",
                keyValue: new Guid("a140ddc9-2bbf-477c-9052-c285118c2326"));

           
        }
    }
}
