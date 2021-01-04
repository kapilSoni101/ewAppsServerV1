using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.BusinessEntity.Data.Migrations
{
    public partial class initasnentity124 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BAASN",
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
                    ERPCustomerKey = table.Column<string>(maxLength: 20, nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    CustomerName = table.Column<string>(maxLength: 100, nullable: false),
                    ERPASNKey = table.Column<string>(maxLength: 100, nullable: false),
                    DeliveryNo = table.Column<string>(maxLength: 100, nullable: true),
                    ShipDate = table.Column<DateTime>(nullable: false),
                    ExpectedDate = table.Column<DateTime>(maxLength: 100, nullable: false),
                    TrackingNo = table.Column<string>(maxLength: 100, nullable: true),
                    ShipmentType = table.Column<int>(nullable: false),
                    ShipmentTypeText = table.Column<string>(maxLength: 100, nullable: true),
                    ShipmentPlan = table.Column<string>(maxLength: 100, nullable: true),
                    PackagingSlipNo = table.Column<string>(maxLength: 100, nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal (18,5)", nullable: true),
                    Discount = table.Column<decimal>(type: "decimal (18,5)", nullable: true),
                    Freight = table.Column<decimal>(type: "decimal (18,5)", nullable: true),
                    Tax = table.Column<decimal>(type: "decimal (18,5)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BAASN", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BAASNItem",
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
                    ERPASNKey = table.Column<string>(maxLength: 50, nullable: false),
                    ERPConnectorKey = table.Column<string>(maxLength: 50, nullable: false),
                    ASNId = table.Column<Guid>(nullable: false),
                    ERPItemKey = table.Column<string>(maxLength: 20, nullable: false),
                    ItemID = table.Column<Guid>(nullable: false),
                    ItemName = table.Column<string>(maxLength: 100, nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal (18,5)", nullable: false),
                    QuantityUnit = table.Column<string>(maxLength: 20, nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal (18,5)", nullable: false),
                    UnitPriceFC = table.Column<decimal>(type: "decimal (18,5)", nullable: false),
                    Unit = table.Column<string>(maxLength: 20, nullable: true),
                    DiscountPercent = table.Column<decimal>(type: "decimal (18,5)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal (18,5)", nullable: false),
                    TaxCode = table.Column<string>(maxLength: 20, nullable: true),
                    TaxPercent = table.Column<decimal>(type: "decimal (18,5)", nullable: false),
                    TotalLC = table.Column<decimal>(type: "decimal (18,5)", nullable: false),
                    TotalLCFC = table.Column<decimal>(type: "decimal (18,5)", nullable: false),
                    BlanketAgreementNo = table.Column<string>(maxLength: 100, nullable: true),
                    Freight = table.Column<decimal>(type: "decimal (18,9)", nullable: false),
                    FreightFC = table.Column<decimal>(type: "decimal (18,9)", nullable: false),
                    SerialOrBatchNo = table.Column<string>(maxLength: 100, nullable: true),
                    GLAmount = table.Column<decimal>(type: "decimal (18,9)", nullable: true),
                    Whse = table.Column<string>(nullable: true),
                    ShipFromAddress = table.Column<string>(nullable: true),
                    ShipToAddress = table.Column<string>(nullable: true),
                    BillToAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BAASNItem", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BAASN",
                schema: "be");

            migrationBuilder.DropTable(
                name: "BAASNItem",
                schema: "be");
        }
    }
}
