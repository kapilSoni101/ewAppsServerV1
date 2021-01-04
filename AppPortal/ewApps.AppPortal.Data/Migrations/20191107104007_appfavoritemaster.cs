using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.AppPortal.Data.Migrations
{
    public partial class appfavoritemaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.InsertData(
                schema: "ap",
                table: "Favorite",
                columns: new[] { "ID", "AppId", "CreatedBy", "CreatedOn", "Deleted", "MenuKey", "PortalKey", "System", "TenantId", "TenantUserId", "UpdatedBy", "UpdatedOn", "Url" },
                values: new object[,]
                {
                    { new Guid("eeec0c47-9c5e-4bde-914b-39a3e3a387ee"), new Guid("2b031b59-5276-589c-9d75-2a7ae1c799c8"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "Transaction History", "pay", true, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), "transaction/ history" },
                    { new Guid("c810c5d9-a559-4242-89a7-7283adb4ca7a"), new Guid("2b031b59-5276-589c-9d75-2a7ae1c799c8"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "Receive Payment", "pay", true, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), "arinvoices" },
                    { new Guid("8b5bb0f6-6e57-4529-93bc-9a8b45651b73"), new Guid("2b031b59-5276-589c-9d75-2a7ae1c799c8"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "Refund", "transaction/ refund", true, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), "arinvoices" },
                    { new Guid("083972f2-8eeb-4a99-92f1-6097a18cb094"), new Guid("2b031b59-5276-589c-9d75-2a7ae1c799c8"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "Void a Payment", "pay", true, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), "transaction/ void" },
                    { new Guid("4a5519b6-e2c9-42e2-9715-cae5af638b15"), new Guid("8c6fa8ce-6b94-428f-95de-5ed8859260d2"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "Transaction History", "pay", true, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), "transaction/ history" },
                    { new Guid("8120877e-3351-4f1b-924e-9934551f176e"), new Guid("8c6fa8ce-6b94-428f-95de-5ed8859260d2"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "Receive Payment", "pay", true, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), "arinvoices" },
                    { new Guid("c4285a44-32e9-4c4a-b186-843b6c31b4fb"), new Guid("8c6fa8ce-6b94-428f-95de-5ed8859260d2"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "Refund", "transaction/ refund", true, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), "arinvoices" },
                    { new Guid("4e5a3aa9-86bc-4dea-9acf-136678ef8cfe"), new Guid("8c6fa8ce-6b94-428f-95de-5ed8859260d2"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "Void a Payment", "pay", true, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), "transaction/ void" },
                    { new Guid("fcd4ccc7-eeba-49de-a1aa-83f1cd57a3e8"), new Guid("2b031b59-5276-589c-9d75-2a7ae1c799c8"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "Transaction History", "cust", true, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), "payment/ history" },
                    { new Guid("98f338c6-fada-4c44-8f09-298f4c6cc292"), new Guid("2b031b59-5276-589c-9d75-2a7ae1c799c8"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "Make a Payment", "cust", true, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), "payment" },
                    { new Guid("b2bd694f-004e-42c3-bad8-3478c37bcec4"), new Guid("8c6fa8ce-6b94-428f-95de-5ed8859260d2"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "Create Order", "cust", true, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), "Orders" },
                    { new Guid("c75c4053-8b86-445b-819f-0fbe2028b74b"), new Guid("8c6fa8ce-6b94-428f-95de-5ed8859260d2"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "Receive Payment", "cust", true, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), "apayment" },
                    { new Guid("9d74d0f6-d07c-4a90-884f-92e454c78349"), new Guid("8c6fa8ce-6b94-428f-95de-5ed8859260d2"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "Transaction History", "cust", true, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), "payment/ history" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
