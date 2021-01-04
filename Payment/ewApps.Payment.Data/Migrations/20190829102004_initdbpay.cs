using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.Payment.Data.Migrations
{
    public partial class initdbpay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "pay");

            migrationBuilder.CreateTable(
                name: "Payment",
                schema: "pay",
                columns: table => new
                {
                    CreatedBy = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    ID = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    BusinessId = table.Column<Guid>(nullable: false),
                    PartnerId = table.Column<Guid>(nullable: false),
                    IdentityNumber = table.Column<string>(maxLength: 100, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    AmountFC = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    Description = table.Column<string>(maxLength: 4000, nullable: true),
                    Type = table.Column<string>(maxLength: 100, nullable: true),
                    CheckNumber = table.Column<string>(maxLength: 100, nullable: true),
                    CheckImageFront = table.Column<string>(maxLength: 100, nullable: true),
                    CheckImageBack = table.Column<string>(maxLength: 100, nullable: true),
                    CustomerName = table.Column<string>(maxLength: 100, nullable: true),
                    CustomerAccountNumber = table.Column<string>(maxLength: 100, nullable: true),
                    CustomerRoutingNumber = table.Column<string>(maxLength: 100, nullable: true),
                    CustomerAccountType = table.Column<int>(nullable: false),
                    OriginationDate = table.Column<DateTime>(nullable: false),
                    LastStatusUpdateDate = table.Column<DateTime>(nullable: false),
                    LastTransactionStatusId = table.Column<Guid>(nullable: false),
                    Status = table.Column<string>(maxLength: 100, nullable: true),
                    Reason = table.Column<string>(maxLength: 100, nullable: true),
                    AppServiceId = table.Column<Guid>(nullable: false),
                    AppServiceAttributeId = table.Column<Guid>(nullable: false),
                    ReturnCode = table.Column<string>(maxLength: 20, nullable: true),
                    ReturnString = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PaymentInvoiceLinking",
                schema: "pay",
                columns: table => new
                {
                    CreatedBy = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    ID = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    PaymentId = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    AmountFC = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    InvoiceId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentInvoiceLinking", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PaymentLog",
                schema: "pay",
                columns: table => new
                {
                    CreatedBy = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    ID = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ClientIP = table.Column<string>(nullable: true),
                    ClientBrowser = table.Column<string>(nullable: true),
                    ClientOS = table.Column<string>(nullable: true),
                    PaymentId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentLog", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RecurringPayment",
                schema: "pay",
                columns: table => new
                {
                    CreatedBy = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ID = table.Column<Guid>(nullable: false),
                    RecurringPeriod = table.Column<int>(nullable: false),
                    RecurringTerms = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    RemainingTermCount = table.Column<int>(nullable: false),
                    NextPaymentdate = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<string>(maxLength: 100, nullable: true),
                    TermAmount = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    Status = table.Column<int>(nullable: false),
                    InvoiceTax = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    CustomerAccountId = table.Column<Guid>(nullable: false),
                    Payload = table.Column<string>(maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurringPayment", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RecurringPaymentLog",
                schema: "pay",
                columns: table => new
                {
                    CreatedBy = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ID = table.Column<Guid>(nullable: false),
                    RecurringPaymentId = table.Column<Guid>(nullable: false),
                    ScheduledDate = table.Column<DateTime>(nullable: false),
                    ProcessingDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurringPaymentLog", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "pay",
                columns: table => new
                {
                    CreatedBy = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    ID = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    RoleName = table.Column<string>(maxLength: 100, nullable: false),
                    RoleKey = table.Column<string>(maxLength: 100, nullable: false),
                    PermissionBitMask = table.Column<long>(nullable: false),
                    AppId = table.Column<Guid>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    UserType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RoleLinking",
                schema: "pay",
                columns: table => new
                {
                    CreatedBy = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    ID = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    TenantUserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    AppId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleLinking", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payment",
                schema: "pay");

            migrationBuilder.DropTable(
                name: "PaymentInvoiceLinking",
                schema: "pay");

            migrationBuilder.DropTable(
                name: "PaymentLog",
                schema: "pay");

            migrationBuilder.DropTable(
                name: "RecurringPayment",
                schema: "pay");

            migrationBuilder.DropTable(
                name: "RecurringPaymentLog",
                schema: "pay");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "pay");

            migrationBuilder.DropTable(
                name: "RoleLinking",
                schema: "pay");
        }
    }
}
