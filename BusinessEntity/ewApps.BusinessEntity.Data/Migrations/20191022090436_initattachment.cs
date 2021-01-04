using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.BusinessEntity.Data.Migrations
{
    public partial class initattachment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrencyCode",
                schema: "be",
                table: "BACustomer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ContractId",
                schema: "be",
                table: "BAContractAttachment",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ERPConnectorKey",
                schema: "be",
                table: "BAContractAttachment",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ERPContractAttachmentKey",
                schema: "be",
                table: "BAContractAttachment",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ERPContractKey",
                schema: "be",
                table: "BAContractAttachment",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BAARinvoiceAttachment",
                schema: "be",
                columns: table => new
                {
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    ID = table.Column<Guid>(nullable: false),
                    ERPConnectorKey = table.Column<string>(maxLength: 100, nullable: true),
                    ERPARInvoiceAttachmentKey = table.Column<string>(maxLength: 100, nullable: true),
                    ARInvoiceId = table.Column<Guid>(nullable: false),
                    ERPARInvoiceKey = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    FreeText = table.Column<string>(maxLength: 100, nullable: true),
                    AttachmentDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BAARinvoiceAttachment", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BAASNAttachment",
                schema: "be",
                columns: table => new
                {
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    ID = table.Column<Guid>(nullable: false),
                    ERPConnectorKey = table.Column<string>(maxLength: 100, nullable: true),
                    ERPASNAttachmentKey = table.Column<string>(maxLength: 100, nullable: true),
                    ASNId = table.Column<Guid>(nullable: false),
                    ERPASNKey = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    FreeText = table.Column<string>(maxLength: 100, nullable: true),
                    AttachmentDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BAASNAttachment", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BADeliveryAttachment",
                schema: "be",
                columns: table => new
                {
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    ID = table.Column<Guid>(nullable: false),
                    ERPConnectorKey = table.Column<string>(maxLength: 100, nullable: true),
                    ERPDeliveryAttachmentKey = table.Column<string>(maxLength: 100, nullable: true),
                    DeliveryId = table.Column<Guid>(nullable: false),
                    ERPDeliveryKey = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    FreeText = table.Column<string>(maxLength: 100, nullable: true),
                    AttachmentDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BADeliveryAttachment", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BAItemMasterAttachment",
                schema: "be",
                columns: table => new
                {
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    ID = table.Column<Guid>(nullable: false),
                    ERPConnectorKey = table.Column<string>(maxLength: 100, nullable: true),
                    ERPItemAttachmentKey = table.Column<string>(maxLength: 100, nullable: true),
                    ItemId = table.Column<Guid>(nullable: false),
                    ERPItemKey = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    FreeText = table.Column<string>(maxLength: 100, nullable: true),
                    AttachmentDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BAItemMasterAttachment", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BASalesOrderAttachment",
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
                    ERPSalesOrderAttachmentKey = table.Column<string>(maxLength: 100, nullable: true),
                    SalesOrderId = table.Column<Guid>(nullable: false),
                    ERPSalesOrderKey = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    FreeText = table.Column<string>(maxLength: 100, nullable: true),
                    AttachmentDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BASalesOrderAttachment", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BASalesQuotationAttachment",
                schema: "be",
                columns: table => new
                {
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    ID = table.Column<Guid>(nullable: false),
                    ERPConnectorKey = table.Column<string>(maxLength: 100, nullable: true),
                    ERPSalesQuotationAttachmentKey = table.Column<string>(maxLength: 100, nullable: true),
                    SalesQuotationId = table.Column<Guid>(nullable: false),
                    ERPSalesQuotationKey = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    FreeText = table.Column<string>(maxLength: 100, nullable: true),
                    AttachmentDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BASalesQuotationAttachment", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BAARinvoiceAttachment",
                schema: "be");

            migrationBuilder.DropTable(
                name: "BAASNAttachment",
                schema: "be");

            migrationBuilder.DropTable(
                name: "BADeliveryAttachment",
                schema: "be");

            migrationBuilder.DropTable(
                name: "BAItemMasterAttachment",
                schema: "be");

            migrationBuilder.DropTable(
                name: "BASalesOrderAttachment",
                schema: "be");

            migrationBuilder.DropTable(
                name: "BASalesQuotationAttachment",
                schema: "be");

            migrationBuilder.DropColumn(
                name: "CurrencyCode",
                schema: "be",
                table: "BACustomer");

            migrationBuilder.DropColumn(
                name: "ContractId",
                schema: "be",
                table: "BAContractAttachment");

            migrationBuilder.DropColumn(
                name: "ERPConnectorKey",
                schema: "be",
                table: "BAContractAttachment");

            migrationBuilder.DropColumn(
                name: "ERPContractAttachmentKey",
                schema: "be",
                table: "BAContractAttachment");

            migrationBuilder.DropColumn(
                name: "ERPContractKey",
                schema: "be",
                table: "BAContractAttachment");
        }
    }
}
