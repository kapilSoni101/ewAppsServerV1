using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.BusinessEntity.Data.Migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "be");

            migrationBuilder.CreateTable(
                name: "ERPConnector",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    ConnectorKey = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ERPConnector", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ERPConnectorConfig",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    SettingJson = table.Column<string>(maxLength: 4000, nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    ConnectorKey = table.Column<string>(maxLength: 20, nullable: true),
                    Status = table.Column<string>(maxLength: 100, nullable: true),
                    Message = table.Column<string>(maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ERPConnectorConfig", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BAARInvoice",
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
                    ERPARInvoiceKey = table.Column<string>(maxLength: 100, nullable: true),
                    ERPConnectorKey = table.Column<string>(maxLength: 100, nullable: true),
                    ERPCustomerKey = table.Column<string>(maxLength: 100, nullable: true),
                    CustomerId = table.Column<Guid>(nullable: false),
                    CustomerName = table.Column<string>(maxLength: 100, nullable: true),
                    ContactPerson = table.Column<string>(maxLength: 100, nullable: true),
                    CustomerRefNo = table.Column<string>(maxLength: 100, nullable: true),
                    LocalCurrency = table.Column<string>(maxLength: 100, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    StatusText = table.Column<string>(maxLength: 100, nullable: true),
                    PostingDate = table.Column<DateTime>(nullable: false),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    SalesEmployee = table.Column<string>(maxLength: 100, nullable: true),
                    Owner = table.Column<string>(maxLength: 100, nullable: true),
                    TotalBeforeDiscount = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    TotalBeforeDiscountFC = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    DiscountFC = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    Freight = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    FreightFC = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    TaxFC = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    TotalPaymentDue = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    TotalPaymentDueFC = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    AppliedAmount = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    AppliedAmountFC = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    BalanceDue = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    BalanceDueFC = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    Remarks = table.Column<string>(maxLength: 4000, nullable: true),
                    ShipFromAddress = table.Column<string>(maxLength: 4000, nullable: true),
                    ShipFromAddressKey = table.Column<string>(maxLength: 100, nullable: true),
                    ShipToAddress = table.Column<string>(maxLength: 4000, nullable: true),
                    ERPShipToAddressKey = table.Column<string>(maxLength: 100, nullable: true),
                    ERPBillToAddressKey = table.Column<string>(maxLength: 100, nullable: true),
                    BillToAddress = table.Column<string>(maxLength: 4000, nullable: true),
                    ShippingType = table.Column<int>(nullable: false),
                    ShippingTypeText = table.Column<string>(maxLength: 100, nullable: true),
                    TrackingNo = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BAARInvoice", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BAARInvoiceItem",
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
                    ERPARInvoiceItemKey = table.Column<string>(maxLength: 100, nullable: true),
                    ARInvoiceID = table.Column<Guid>(nullable: false),
                    ERPARInvoiceKey = table.Column<string>(maxLength: 100, nullable: true),
                    ERPItemKey = table.Column<string>(maxLength: 100, nullable: true),
                    LotNo = table.Column<string>(maxLength: 20, nullable: true),
                    SerialOrBatchNo = table.Column<string>(maxLength: 100, nullable: true),
                    ItemId = table.Column<Guid>(nullable: false),
                    ItemName = table.Column<string>(maxLength: 100, nullable: true),
                    ItemType = table.Column<string>(maxLength: 100, nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    QuantityUnit = table.Column<string>(maxLength: 100, nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    UnitPriceFC = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    Unit = table.Column<string>(maxLength: 100, nullable: true),
                    DiscountPercent = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    DiscountAmountFC = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    TaxCode = table.Column<string>(maxLength: 100, nullable: true),
                    TaxPercent = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    TotalLC = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    TotalLCFC = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    ERPShipToAddressKey = table.Column<string>(maxLength: 100, nullable: true),
                    ShipFromAddress = table.Column<string>(maxLength: 4000, nullable: true),
                    ShipFromAddressKey = table.Column<string>(maxLength: 100, nullable: true),
                    ShipToAddress = table.Column<string>(maxLength: 4000, nullable: true),
                    ERPBillToAddressKey = table.Column<string>(maxLength: 100, nullable: true),
                    BillToAddress = table.Column<string>(maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BAARInvoiceItem", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BAContract",
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
                    ContactPerson = table.Column<string>(maxLength: 100, nullable: true),
                    CustomerRefNo = table.Column<string>(maxLength: 100, nullable: true),
                    BPCurrency = table.Column<string>(maxLength: 20, nullable: true),
                    TelephoneNo = table.Column<string>(maxLength: 20, nullable: true),
                    Email = table.Column<string>(maxLength: 20, nullable: true),
                    DocumentNo = table.Column<int>(nullable: false),
                    AgreementMethod = table.Column<string>(maxLength: 20, nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    BPProject = table.Column<string>(maxLength: 100, nullable: true),
                    TerminationDate = table.Column<DateTime>(nullable: true),
                    SigningDate = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(maxLength: 4000, nullable: true),
                    AgreementType = table.Column<string>(maxLength: 20, nullable: true),
                    PaymentTerms = table.Column<string>(maxLength: 100, nullable: true),
                    PaymentMethod = table.Column<string>(maxLength: 20, nullable: true),
                    ShippingType = table.Column<int>(nullable: false),
                    ShippingTypeText = table.Column<string>(maxLength: 20, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    StatusText = table.Column<int>(maxLength: 20, nullable: false),
                    Remarks = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BAContract", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BACustomer",
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
                    CustomerName = table.Column<string>(maxLength: 100, nullable: false),
                    BusinessPartnerTenantId = table.Column<Guid>(nullable: false),
                    Group = table.Column<string>(maxLength: 20, nullable: true),
                    Currency = table.Column<string>(maxLength: 20, nullable: true),
                    FederalTaxID = table.Column<string>(maxLength: 100, nullable: true),
                    AddressLine1 = table.Column<string>(maxLength: 100, nullable: true),
                    AddressLine2 = table.Column<string>(maxLength: 100, nullable: true),
                    Street = table.Column<string>(maxLength: 20, nullable: true),
                    StreetNo = table.Column<string>(maxLength: 20, nullable: true),
                    City = table.Column<string>(maxLength: 20, nullable: true),
                    ZipCode = table.Column<string>(maxLength: 20, nullable: true),
                    State = table.Column<string>(maxLength: 20, nullable: true),
                    Country = table.Column<string>(maxLength: 20, nullable: true),
                    Tel1 = table.Column<string>(maxLength: 20, nullable: true),
                    Tel2 = table.Column<string>(maxLength: 20, nullable: true),
                    MobilePhone = table.Column<string>(maxLength: 20, nullable: true),
                    Fax = table.Column<string>(maxLength: 20, nullable: true),
                    Email = table.Column<string>(maxLength: 20, nullable: true),
                    Website = table.Column<string>(maxLength: 20, nullable: true),
                    ShippingType = table.Column<int>(nullable: false),
                    ShippingTypeText = table.Column<string>(maxLength: 20, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    StatusText = table.Column<int>(maxLength: 20, nullable: false),
                    Remarks = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BACustomer", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BACustomerAddress",
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
                    Label = table.Column<string>(maxLength: 20, nullable: true),
                    ObjectType = table.Column<int>(nullable: false),
                    ObjectTypeText = table.Column<string>(maxLength: 20, nullable: true),
                    AddressName = table.Column<string>(maxLength: 100, nullable: true),
                    Line1 = table.Column<string>(maxLength: 100, nullable: true),
                    Line2 = table.Column<string>(maxLength: 100, nullable: true),
                    Line3 = table.Column<string>(maxLength: 100, nullable: true),
                    Street = table.Column<string>(maxLength: 20, nullable: true),
                    StreetNo = table.Column<string>(maxLength: 20, nullable: true),
                    City = table.Column<string>(maxLength: 20, nullable: true),
                    ZipCode = table.Column<string>(maxLength: 20, nullable: true),
                    State = table.Column<string>(maxLength: 20, nullable: true),
                    Country = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BACustomerAddress", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BACustomerContact",
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
                    ERPContactKey = table.Column<string>(maxLength: 20, nullable: false),
                    ERPConnectorKey = table.Column<string>(maxLength: 20, nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    ERPCustomerKey = table.Column<string>(maxLength: 20, nullable: false),
                    Default = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: true),
                    LastName = table.Column<string>(maxLength: 100, nullable: true),
                    Title = table.Column<string>(maxLength: 20, nullable: true),
                    Position = table.Column<string>(maxLength: 100, nullable: true),
                    Address = table.Column<string>(maxLength: 4000, nullable: true),
                    Telephone = table.Column<string>(maxLength: 20, nullable: true),
                    Email = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BACustomerContact", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BACustomerPaymentDetail",
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
                    CustomerId = table.Column<Guid>(nullable: false),
                    ERPCustomerKey = table.Column<string>(maxLength: 20, nullable: false),
                    CreditCardType = table.Column<int>(nullable: false),
                    CreditCardTypeText = table.Column<string>(maxLength: 20, nullable: true),
                    CreditCardNo = table.Column<string>(maxLength: 20, nullable: true),
                    ExpirationDate = table.Column<DateTime>(nullable: true),
                    IDNumber = table.Column<int>(nullable: false),
                    BankCountry = table.Column<string>(maxLength: 20, nullable: true),
                    BankName = table.Column<string>(maxLength: 100, nullable: true),
                    BankCode = table.Column<string>(maxLength: 20, nullable: true),
                    Account = table.Column<string>(maxLength: 20, nullable: true),
                    BICSWIFTCode = table.Column<string>(maxLength: 20, nullable: true),
                    BankAccountName = table.Column<string>(maxLength: 100, nullable: true),
                    Branch = table.Column<string>(maxLength: 100, nullable: true),
                    Default = table.Column<bool>(nullable: false),
                    ABARouteNumber = table.Column<string>(maxLength: 20, nullable: true),
                    AccountType = table.Column<int>(nullable: false),
                    AccountTypeText = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BACustomerPaymentDetail", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BADelivery",
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
                    ERPDeliveryKey = table.Column<string>(maxLength: 20, nullable: false),
                    ERPConnectorKey = table.Column<string>(maxLength: 20, nullable: false),
                    CustomerID = table.Column<string>(maxLength: 100, nullable: false),
                    CustomerName = table.Column<string>(maxLength: 100, nullable: false),
                    ContactPerson = table.Column<string>(maxLength: 100, nullable: false),
                    CustomerRefNo = table.Column<string>(maxLength: 100, nullable: true),
                    LocalCurrency = table.Column<string>(maxLength: 3, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    StatusText = table.Column<string>(maxLength: 20, nullable: false),
                    PostingDate = table.Column<DateTime>(nullable: true),
                    DeliveryDate = table.Column<DateTime>(nullable: true),
                    DocumentDate = table.Column<DateTime>(nullable: true),
                    SalesEmployee = table.Column<string>(maxLength: 100, nullable: true),
                    Owner = table.Column<string>(maxLength: 100, nullable: true),
                    TotalBeforeDiscount = table.Column<decimal>(type: "decimal (18,5)", nullable: true),
                    TotalBeforeDiscountFC = table.Column<decimal>(type: "decimal (18,5)", nullable: true),
                    Discount = table.Column<decimal>(type: "decimal (18,5)", nullable: true),
                    DiscountFC = table.Column<decimal>(type: "decimal (18,5)", nullable: true),
                    Freight = table.Column<decimal>(type: "decimal (18,5)", nullable: true),
                    FreightFC = table.Column<decimal>(type: "decimal (18,5)", nullable: true),
                    Tax = table.Column<decimal>(type: "decimal (18,5)", nullable: false),
                    TaxFC = table.Column<decimal>(type: "decimal (18,5)", nullable: true),
                    TotalPaymentDue = table.Column<decimal>(type: "decimal (18,5)", nullable: true),
                    TotalPaymentDueFC = table.Column<decimal>(type: "decimal (18,5)", nullable: true),
                    Remarks = table.Column<string>(maxLength: 4000, nullable: true),
                    ShipToAddress = table.Column<string>(maxLength: 4000, nullable: true),
                    ERPBillToAddressKey = table.Column<string>(maxLength: 20, nullable: true),
                    BillToAddress = table.Column<string>(maxLength: 4000, nullable: true),
                    ShippingType = table.Column<int>(nullable: false),
                    ShippingTypeText = table.Column<string>(maxLength: 20, nullable: false),
                    TrackingNo = table.Column<string>(maxLength: 100, nullable: true),
                    StampNo = table.Column<string>(maxLength: 100, nullable: true),
                    PickAndPackRemarks = table.Column<string>(maxLength: 4000, nullable: true),
                    ERPShipToAddressKey = table.Column<string>(maxLength: 20, nullable: true),
                    ShipFromAddress = table.Column<string>(maxLength: 4000, nullable: true),
                    ShipFromAddressKey = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BADelivery", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BADeliveryItem",
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
                    ERPDeliveryItemKey = table.Column<string>(maxLength: 20, nullable: false),
                    ERPContractKey = table.Column<string>(maxLength: 20, nullable: false),
                    DeliveryID = table.Column<Guid>(nullable: false),
                    ERPItemKey = table.Column<string>(maxLength: 20, nullable: false),
                    LotNo = table.Column<string>(maxLength: 20, nullable: false),
                    ItemID = table.Column<Guid>(nullable: false),
                    ItemName = table.Column<string>(maxLength: 100, nullable: false),
                    ItemType = table.Column<int>(nullable: false),
                    ItemTypeText = table.Column<string>(maxLength: 20, nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal (18,5)", nullable: false),
                    UnitPriceFC = table.Column<decimal>(type: "decimal (18,5)", nullable: false),
                    DiscountPercent = table.Column<decimal>(type: "decimal (18,5)", nullable: false),
                    TaxCode = table.Column<string>(maxLength: 20, nullable: true),
                    TaxPercent = table.Column<decimal>(type: "decimal (18,5)", nullable: false),
                    TotalLC = table.Column<decimal>(type: "decimal (18,5)", nullable: false),
                    TotalLCFC = table.Column<decimal>(type: "decimal (18,5)", nullable: false),
                    BlanketAgreementNo = table.Column<string>(maxLength: 100, nullable: false),
                    Freight = table.Column<decimal>(type: "decimal (18,9)", nullable: false),
                    FreightFC = table.Column<decimal>(type: "decimal (18,9)", nullable: false),
                    SerialOrBatchNo = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BADeliveryItem", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BAItemMaster",
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
                    ERPItemKey = table.Column<string>(maxLength: 20, nullable: false),
                    ItemType = table.Column<string>(maxLength: 20, nullable: true),
                    ItemName = table.Column<string>(maxLength: 100, nullable: true),
                    BarCode = table.Column<string>(maxLength: 20, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    PriceFC = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    PriceUnit = table.Column<int>(nullable: false),
                    PriceUniText = table.Column<string>(maxLength: 20, nullable: true),
                    ShippingType = table.Column<int>(nullable: false),
                    ShippingTypeText = table.Column<string>(maxLength: 20, nullable: true),
                    ManagedItem = table.Column<string>(maxLength: 100, nullable: true),
                    Active = table.Column<string>(maxLength: 20, nullable: true),
                    PurchaseLength = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    PurchaseLengthUnit = table.Column<int>(nullable: false),
                    PurchaseLengthUnitText = table.Column<string>(maxLength: 20, nullable: true),
                    PurchaseWidth = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    PurchaseWidthUnit = table.Column<int>(nullable: false),
                    PurchaseWidthUnitText = table.Column<string>(maxLength: 20, nullable: true),
                    PurchaseHeight = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    PurchaseHeightUnit = table.Column<int>(nullable: false),
                    PurchaseHeightUnitText = table.Column<string>(maxLength: 20, nullable: true),
                    PurchaseVolume = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    PurchaseVolumeUnit = table.Column<int>(nullable: false),
                    PurchaseVolumeUnitText = table.Column<string>(maxLength: 20, nullable: true),
                    PurchaseWeight = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    PurchaseWeightUnit = table.Column<int>(nullable: false),
                    PurchaseWeightUnitText = table.Column<string>(maxLength: 20, nullable: true),
                    SalesLength = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    SalesLengthUnit = table.Column<int>(nullable: false),
                    SalesLengthUnitText = table.Column<string>(maxLength: 20, nullable: true),
                    SalesWidth = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    SalesWidthUnit = table.Column<int>(nullable: false),
                    SalesWidthUnitText = table.Column<string>(maxLength: 20, nullable: true),
                    SalesHeight = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    SalesHeightUnit = table.Column<int>(nullable: false),
                    SalesHeightUnitText = table.Column<string>(maxLength: 20, nullable: true),
                    SalesVolume = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    SalesVolumeUnit = table.Column<int>(nullable: false),
                    SalesVolumeUnitText = table.Column<string>(maxLength: 20, nullable: true),
                    SalesWeight = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    SalesWeightUnit = table.Column<int>(nullable: false),
                    SalesWeightUnitText = table.Column<string>(maxLength: 20, nullable: true),
                    Remarks = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BAItemMaster", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BAPurchaseInquiry",
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
                    ERPCustomerKey = table.Column<string>(maxLength: 100, nullable: true),
                    CustomerName = table.Column<string>(maxLength: 100, nullable: true),
                    ContactPerson = table.Column<string>(maxLength: 100, nullable: true),
                    CustomerRefNo = table.Column<string>(maxLength: 100, nullable: true),
                    Status = table.Column<string>(nullable: false),
                    StatusText = table.Column<string>(nullable: true),
                    PostingDate = table.Column<DateTime>(nullable: false),
                    ValidUntil = table.Column<DateTime>(nullable: false),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    Remarks = table.Column<string>(maxLength: 4000, nullable: true),
                    ERPShipToAddressKey = table.Column<string>(maxLength: 100, nullable: true),
                    ShipToAddress = table.Column<string>(maxLength: 4000, nullable: true),
                    ERPPayToAddressKey = table.Column<string>(maxLength: 100, nullable: true),
                    PayToAddress = table.Column<string>(maxLength: 4000, nullable: true),
                    ShippingType = table.Column<int>(nullable: false),
                    ShippingText = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BAPurchaseInquiry", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BAPurchaseInquiryItem",
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
                    ERPPurchaseEnquiryItemKey = table.Column<string>(maxLength: 20, nullable: false),
                    PurchaseEnquiryID = table.Column<string>(maxLength: 20, nullable: false),
                    ItemID = table.Column<Guid>(nullable: false),
                    ItemName = table.Column<string>(maxLength: 100, nullable: false),
                    QuantityUnit = table.Column<string>(maxLength: 20, nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal (18,5)", nullable: false),
                    Unit = table.Column<decimal>(type: "decimal (18,5)", nullable: false),
                    TotalLC = table.Column<decimal>(type: "decimal (18,5)", nullable: false),
                    PriceSource = table.Column<string>(maxLength: 100, nullable: false),
                    ERPShipToAddressKey = table.Column<string>(maxLength: 20, nullable: false),
                    ShipToAddress = table.Column<string>(maxLength: 4000, nullable: false),
                    ERPPayToAddressKey = table.Column<string>(maxLength: 20, nullable: false),
                    PayToAddress = table.Column<string>(maxLength: 4000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BAPurchaseInquiryItem", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BAPurchaseOrder",
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
                    ERPSalesOrderKey = table.Column<string>(maxLength: 100, nullable: true),
                    ERPCustomerKey = table.Column<string>(maxLength: 100, nullable: true),
                    CustomerID = table.Column<Guid>(nullable: false),
                    CustomerName = table.Column<string>(maxLength: 100, nullable: true),
                    ContactPerson = table.Column<string>(maxLength: 100, nullable: true),
                    CustomerRefNo = table.Column<string>(maxLength: 100, nullable: true),
                    LocalCurrency = table.Column<string>(maxLength: 100, nullable: true),
                    Status = table.Column<string>(nullable: false),
                    StatusText = table.Column<string>(nullable: true),
                    PostingDate = table.Column<DateTime>(nullable: false),
                    DeliveryDate = table.Column<DateTime>(nullable: false),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    PickAndPackRemarks = table.Column<string>(maxLength: 4000, nullable: true),
                    SalesEmployee = table.Column<string>(maxLength: 100, nullable: true),
                    Owner = table.Column<string>(maxLength: 100, nullable: true),
                    TotalBeforeDiscount = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    Freight = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    TotalPaymentDue = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    Remarks = table.Column<string>(maxLength: 4000, nullable: true),
                    ShipFromAddress = table.Column<string>(maxLength: 4000, nullable: true),
                    ShipFromAddressKey = table.Column<string>(maxLength: 100, nullable: true),
                    ERPShipToAddressKey = table.Column<string>(maxLength: 100, nullable: true),
                    ShipToAddress = table.Column<string>(maxLength: 4000, nullable: true),
                    ERPBillToAddressKey = table.Column<string>(maxLength: 100, nullable: true),
                    BillToAddress = table.Column<string>(maxLength: 4000, nullable: true),
                    ShippingType = table.Column<int>(nullable: false),
                    ShippingText = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BAPurchaseOrder", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BAPurchaseOrderItem",
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
                    ERPPurchaseOrderItemKey = table.Column<string>(maxLength: 20, nullable: true),
                    ERPConnectorKey = table.Column<string>(maxLength: 20, nullable: true),
                    PurchaseOrderId = table.Column<Guid>(nullable: false),
                    ERPPurchaseOrderKey = table.Column<string>(maxLength: 100, nullable: true),
                    ERPItemKey = table.Column<string>(maxLength: 100, nullable: true),
                    LotNo = table.Column<string>(maxLength: 20, nullable: true),
                    SerialOrBatchNo = table.Column<string>(nullable: true),
                    ItemID = table.Column<string>(maxLength: 100, nullable: true),
                    ItemName = table.Column<string>(maxLength: 100, nullable: true),
                    ItemType = table.Column<int>(nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    QuantityUnit = table.Column<string>(maxLength: 100, nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    Unit = table.Column<string>(maxLength: 100, nullable: true),
                    DiscountPercent = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    TaxCode = table.Column<string>(maxLength: 20, nullable: true),
                    TaxPercent = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    TotalLC = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    Whse = table.Column<string>(maxLength: 100, nullable: true),
                    BlanketAgreementNo = table.Column<string>(maxLength: 100, nullable: true),
                    ShipFromAddress = table.Column<string>(maxLength: 4000, nullable: true),
                    ERPShipToAddressKey = table.Column<string>(maxLength: 100, nullable: true),
                    ShipToAddress = table.Column<string>(maxLength: 4000, nullable: true),
                    ERPPayToAddressKey = table.Column<string>(maxLength: 100, nullable: true),
                    PayToAddress = table.Column<string>(maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BAPurchaseOrderItem", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BASalesOrder",
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
                    ERPSalesOrderKey = table.Column<string>(maxLength: 100, nullable: true),
                    ERPConnectorKey = table.Column<string>(maxLength: 100, nullable: true),
                    ERPCustomerKey = table.Column<string>(maxLength: 100, nullable: true),
                    CustomerID = table.Column<Guid>(nullable: false),
                    CustomerName = table.Column<string>(maxLength: 100, nullable: true),
                    ContactPerson = table.Column<string>(maxLength: 100, nullable: true),
                    CustomerRefNo = table.Column<string>(maxLength: 100, nullable: true),
                    LocalCurrency = table.Column<string>(maxLength: 100, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    StatusText = table.Column<string>(maxLength: 100, nullable: true),
                    PostingDate = table.Column<DateTime>(nullable: false),
                    DeliveryDate = table.Column<DateTime>(nullable: false),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    PickAndPackRemarks = table.Column<string>(maxLength: 4000, nullable: true),
                    SalesEmployee = table.Column<string>(maxLength: 100, nullable: true),
                    Owner = table.Column<string>(maxLength: 100, nullable: true),
                    TotalBeforeDiscount = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    TotalBeforeDiscountFC = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    DiscountFC = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    Freight = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    FreightFC = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    TaxFC = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    TotalPaymentDue = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    TotalPaymentDueFC = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    Remarks = table.Column<string>(maxLength: 40000, nullable: true),
                    ShipFromAddress = table.Column<string>(maxLength: 4000, nullable: true),
                    ShipFromAddressKey = table.Column<string>(maxLength: 100, nullable: true),
                    ERPShipToAddressKey = table.Column<string>(maxLength: 100, nullable: true),
                    ShipToAddress = table.Column<string>(maxLength: 4000, nullable: true),
                    ERPBillToAddressKey = table.Column<string>(maxLength: 100, nullable: true),
                    BillToAddress = table.Column<string>(maxLength: 4000, nullable: true),
                    ShippingType = table.Column<int>(nullable: false),
                    ShippingTypeText = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BASalesOrder", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BASalesOrderItem",
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
                    ERPSalesOrderItemKey = table.Column<string>(maxLength: 100, nullable: true),
                    ERPConnectorKey = table.Column<string>(maxLength: 100, nullable: true),
                    SalesOrderId = table.Column<Guid>(nullable: false),
                    ERPSalesOrderKey = table.Column<string>(nullable: true),
                    ERPItemKey = table.Column<string>(maxLength: 100, nullable: true),
                    LotNo = table.Column<string>(maxLength: 20, nullable: true),
                    ItemId = table.Column<Guid>(nullable: false),
                    SerialOrBatchNo = table.Column<string>(maxLength: 100, nullable: true),
                    ItemName = table.Column<string>(maxLength: 100, nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    QuantityUnit = table.Column<string>(maxLength: 100, nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    UnitPriceFC = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    Unit = table.Column<string>(maxLength: 100, nullable: true),
                    DiscountPercent = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    DiscountAmountFC = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    TaxCode = table.Column<string>(maxLength: 100, nullable: true),
                    TaxPercent = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    TotalLC = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    TotalLCFC = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    Whse = table.Column<string>(maxLength: 100, nullable: true),
                    BlanketAgreementNo = table.Column<string>(maxLength: 100, nullable: true),
                    ShipFromAddress = table.Column<string>(maxLength: 4000, nullable: true),
                    ShipFromAddressKey = table.Column<string>(maxLength: 100, nullable: true),
                    ShipToAddress = table.Column<string>(maxLength: 4000, nullable: true),
                    ERPShipToAddressKey = table.Column<string>(maxLength: 100, nullable: true),
                    BillToAddress = table.Column<string>(maxLength: 4000, nullable: true),
                    ERPBillToAddressKey = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BASalesOrderItem", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BASalesQuotation",
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
                    ERPSalesQuotationKey = table.Column<string>(maxLength: 100, nullable: true),
                    ERPConnectorKey = table.Column<string>(maxLength: 100, nullable: true),
                    ERPCustomerKey = table.Column<string>(maxLength: 100, nullable: true),
                    CustomerId = table.Column<Guid>(nullable: false),
                    CustomerName = table.Column<string>(maxLength: 100, nullable: true),
                    ContactPerson = table.Column<string>(maxLength: 100, nullable: true),
                    CustomerRefNo = table.Column<string>(maxLength: 100, nullable: true),
                    LocalCurrency = table.Column<string>(maxLength: 100, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    StatusText = table.Column<string>(maxLength: 100, nullable: true),
                    PostingDate = table.Column<DateTime>(nullable: false),
                    ValidUntil = table.Column<DateTime>(nullable: false),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    SalesEmployee = table.Column<string>(maxLength: 100, nullable: true),
                    Owner = table.Column<string>(maxLength: 100, nullable: true),
                    TotalBeforeDiscount = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    TotalBeforeDiscountFC = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    DiscountFC = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    Freight = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    FreightFC = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    TaxFC = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    TotalPaymentDue = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    TotalPaymentDueFC = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    Remarks = table.Column<string>(maxLength: 4000, nullable: true),
                    ERPShipToAddressKey = table.Column<string>(maxLength: 100, nullable: true),
                    ShipFromAddress = table.Column<string>(maxLength: 4000, nullable: true),
                    ShipFromAddressKey = table.Column<string>(maxLength: 100, nullable: true),
                    ShipToAddress = table.Column<string>(maxLength: 4000, nullable: true),
                    ERPBillToAddressKey = table.Column<string>(maxLength: 100, nullable: true),
                    BillToAddress = table.Column<string>(maxLength: 4000, nullable: true),
                    ShippingType = table.Column<int>(nullable: false),
                    ShippingTypeText = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BASalesQuotation", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BASalesQuotationItem",
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
                    ERPSalesQuotationItemKey = table.Column<string>(maxLength: 100, nullable: true),
                    ERPConnectorKey = table.Column<string>(maxLength: 100, nullable: true),
                    LotNo = table.Column<string>(maxLength: 20, nullable: true),
                    SalesQuotationId = table.Column<Guid>(nullable: false),
                    ERPSalesQuotationKey = table.Column<string>(maxLength: 100, nullable: true),
                    ERPItemKey = table.Column<string>(maxLength: 100, nullable: true),
                    ItemId = table.Column<Guid>(nullable: false),
                    SerialOrBatchNo = table.Column<string>(maxLength: 100, nullable: true),
                    ItemName = table.Column<string>(maxLength: 100, nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    QuantityUnit = table.Column<string>(maxLength: 100, nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    UnitPriceFC = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    Unit = table.Column<string>(maxLength: 100, nullable: true),
                    DiscountPercent = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    DiscountAmountFC = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    TaxCode = table.Column<string>(maxLength: 100, nullable: true),
                    TaxPercent = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    TotalLC = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    TotalLCFC = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    GLAccount = table.Column<string>(maxLength: 100, nullable: true),
                    BlanketAgreementNo = table.Column<string>(maxLength: 100, nullable: true),
                    ShipFromAddress = table.Column<string>(maxLength: 4000, nullable: true),
                    ShipFromAddressKey = table.Column<string>(maxLength: 100, nullable: true),
                    ERPShipToAddressKey = table.Column<string>(maxLength: 100, nullable: true),
                    ShipToAddress = table.Column<string>(maxLength: 4000, nullable: true),
                    ERPBillToAddressKey = table.Column<string>(maxLength: 100, nullable: true),
                    BillToAddress = table.Column<string>(maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BASalesQuotationItem", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ERPConnector");

            migrationBuilder.DropTable(
                name: "ERPConnectorConfig");

            migrationBuilder.DropTable(
                name: "BAARInvoice",
                schema: "be");

            migrationBuilder.DropTable(
                name: "BAARInvoiceItem",
                schema: "be");

            migrationBuilder.DropTable(
                name: "BAContract",
                schema: "be");

            migrationBuilder.DropTable(
                name: "BACustomer",
                schema: "be");

            migrationBuilder.DropTable(
                name: "BACustomerAddress",
                schema: "be");

            migrationBuilder.DropTable(
                name: "BACustomerContact",
                schema: "be");

            migrationBuilder.DropTable(
                name: "BACustomerPaymentDetail",
                schema: "be");

            migrationBuilder.DropTable(
                name: "BADelivery",
                schema: "be");

            migrationBuilder.DropTable(
                name: "BADeliveryItem",
                schema: "be");

            migrationBuilder.DropTable(
                name: "BAItemMaster",
                schema: "be");

            migrationBuilder.DropTable(
                name: "BAPurchaseInquiry",
                schema: "be");

            migrationBuilder.DropTable(
                name: "BAPurchaseInquiryItem",
                schema: "be");

            migrationBuilder.DropTable(
                name: "BAPurchaseOrder",
                schema: "be");

            migrationBuilder.DropTable(
                name: "BAPurchaseOrderItem",
                schema: "be");

            migrationBuilder.DropTable(
                name: "BASalesOrder",
                schema: "be");

            migrationBuilder.DropTable(
                name: "BASalesOrderItem",
                schema: "be");

            migrationBuilder.DropTable(
                name: "BASalesQuotation",
                schema: "be");

            migrationBuilder.DropTable(
                name: "BASalesQuotationItem",
                schema: "be");
        }
    }
}
