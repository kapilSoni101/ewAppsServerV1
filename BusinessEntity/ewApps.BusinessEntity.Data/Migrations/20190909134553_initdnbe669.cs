using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.BusinessEntity.Data.Migrations
{
    public partial class initdnbe669 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BAContractAttachment",
                schema: "be",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    FreeText = table.Column<string>(maxLength: 100, nullable: true),
                    AttachmentDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BAContractAttachment", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BAContractItem",
                schema: "be",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    ERPConnectorKey = table.Column<string>(maxLength: 20, nullable: false),
                    ContractId = table.Column<Guid>(nullable: false),
                    ItemNo = table.Column<int>(nullable: false),
                    ItemDescription = table.Column<string>(maxLength: 4000, nullable: true),
                    ItemGroup = table.Column<string>(maxLength: 20, nullable: true),
                    PlannedQuantity = table.Column<int>(nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    CummlativeCommittedQuantity = table.Column<int>(nullable: false),
                    CummlativeCommittedAmount = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    CummlativeQuantity = table.Column<int>(nullable: false),
                    CummlativeAmountLC = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    OpenQuantity = table.Column<int>(nullable: false),
                    OpenAmountLC = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    Project = table.Column<string>(maxLength: 20, nullable: true),
                    FreeText = table.Column<string>(maxLength: 100, nullable: true),
                    EndOfWarranty = table.Column<DateTime>(nullable: true),
                    UoMCode = table.Column<string>(maxLength: 100, nullable: true),
                    UoMName = table.Column<string>(maxLength: 100, nullable: true),
                    UoMGroup = table.Column<string>(maxLength: 20, nullable: true),
                    ItemsPerUnit = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    PortionofReturnsPerc = table.Column<string>(maxLength: 20, nullable: true),
                    ShippingType = table.Column<int>(nullable: false),
                    ShippingTypeText = table.Column<string>(maxLength: 20, nullable: true),
                    ItemRowStatus = table.Column<int>(nullable: false),
                    ItemRowStatusText = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BAContractItem", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SyncHistory",
                schema: "be",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    ERPConnectorKey = table.Column<string>(maxLength: 50, nullable: true),
                    ERPEntityKey = table.Column<string>(maxLength: 50, nullable: true),
                    ActionStartTime = table.Column<DateTime>(nullable: false),
                    ActionEndTime = table.Column<DateTime>(nullable: false),
                    StatusCode = table.Column<int>(nullable: false),
                    StatusMessage = table.Column<string>(nullable: true),
                    ReqFromSyncTime = table.Column<DateTime>(nullable: false),
                    ReqToSyncTime = table.Column<DateTime>(nullable: false),
                    NumItems = table.Column<int>(nullable: false),
                    SyncRequestData = table.Column<string>(maxLength: 100, nullable: true),
                    SyncReplyData = table.Column<string>(nullable: true),
                    ExecutionTimeInMS = table.Column<int>(nullable: false),
                    ResponseChunkSize = table.Column<int>(nullable: false),
                    TenantName = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyncHistory", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SyncTimeLog",
                schema: "be",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    ERPConnectorKey = table.Column<string>(maxLength: 100, nullable: true),
                    ERPEntityKey = table.Column<string>(maxLength: 100, nullable: true),
                    ReceiveFromTime = table.Column<DateTime>(nullable: false),
                    ReceiveToTime = table.Column<DateTime>(nullable: false),
                    SendSyncFromTime = table.Column<DateTime>(nullable: false),
                    SendSyncToTime = table.Column<DateTime>(nullable: false),
                    LastReceiveRowId = table.Column<string>(maxLength: 100, nullable: true),
                    LastSendRowId = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyncTimeLog", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BAContractAttachment",
                schema: "be");

            migrationBuilder.DropTable(
                name: "BAContractItem",
                schema: "be");

            migrationBuilder.DropTable(
                name: "SyncHistory",
                schema: "be");

            migrationBuilder.DropTable(
                name: "SyncTimeLog",
                schema: "be");
        }
    }
}
