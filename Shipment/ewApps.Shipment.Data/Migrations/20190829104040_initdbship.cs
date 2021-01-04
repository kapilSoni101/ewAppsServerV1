using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.Shipment.Data.Migrations
{
    public partial class initdbship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ship");

            migrationBuilder.CreateTable(
                name: "CarrierPackageDetail",
                schema: "ship",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    CarrierCode = table.Column<string>(maxLength: 200, nullable: false),
                    CarrierPackageCode = table.Column<string>(maxLength: 200, nullable: false),
                    PackageMasterId = table.Column<Guid>(nullable: false),
                    ContainerType = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrierPackageDetail", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CarrierPackageLinking",
                schema: "ship",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    CarrierCode = table.Column<string>(maxLength: 100, nullable: false),
                    CarrierPackageCode = table.Column<string>(maxLength: 100, nullable: false),
                    ContainerType = table.Column<string>(maxLength: 250, nullable: true),
                    PackageMasterId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrierPackageLinking", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FavouriteShipmentPkgSetting",
                schema: "ship",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    BusinessId = table.Column<Guid>(nullable: false),
                    PackageId = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    ItemIds = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteShipmentPkgSetting", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PackageMaster",
                schema: "ship",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    IdentityNumber = table.Column<string>(maxLength: 100, nullable: true),
                    PkgName = table.Column<string>(maxLength: 200, nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    WeightUnit = table.Column<string>(maxLength: 100, nullable: false),
                    Height = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    HeightUnit = table.Column<string>(maxLength: 100, nullable: false),
                    Width = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    WidthUnit = table.Column<string>(maxLength: 100, nullable: false),
                    Length = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    LengthUnit = table.Column<string>(maxLength: 100, nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageMaster", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "ship",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
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
                schema: "ship",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    TenantUserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    AppId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleLinking", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrderPkg",
                schema: "ship",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    SalesOrderId = table.Column<Guid>(nullable: false),
                    PackageId = table.Column<Guid>(nullable: false),
                    TotalItems = table.Column<int>(nullable: false),
                    PkgName = table.Column<string>(maxLength: 100, nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    WeightUnit = table.Column<string>(maxLength: 100, nullable: false),
                    Height = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    HeightUnit = table.Column<string>(maxLength: 100, nullable: false),
                    Width = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    WidthUnit = table.Column<string>(maxLength: 100, nullable: false),
                    Length = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    LengthUnit = table.Column<string>(maxLength: 100, nullable: false),
                    CarrierPackageCode = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderPkg", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrderPkgItem",
                schema: "ship",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    SalesOrderPackageId = table.Column<Guid>(nullable: false),
                    SalesOrderId = table.Column<Guid>(nullable: false),
                    PackageId = table.Column<Guid>(nullable: false),
                    SalesOrderItemId = table.Column<Guid>(nullable: false),
                    AddedQuantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderPkgItem", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Shipment",
                schema: "ship",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    ShipmentRefId = table.Column<string>(maxLength: 100, nullable: false),
                    ShipmentType = table.Column<int>(nullable: false),
                    SalesOrderId = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    PostingOn = table.Column<DateTime>(nullable: false),
                    DeliveryDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal (18,5)", nullable: false),
                    Description = table.Column<string>(maxLength: 4000, nullable: true),
                    TaxRate = table.Column<decimal>(type: "decimal (18,5)", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal (18,5)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal (18,5)", nullable: false),
                    Freight = table.Column<decimal>(type: "decimal (18,5)", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal (18,5)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal (18,5)", nullable: false),
                    Status = table.Column<int>(nullable: false),
                    StatusText = table.Column<string>(maxLength: 20, nullable: false),
                    CarrierId = table.Column<Guid>(nullable: true),
                    CarrierPlanId = table.Column<string>(maxLength: 100, nullable: true),
                    CarrierPickupDate = table.Column<DateTime>(nullable: true),
                    CarrierTransitDays = table.Column<string>(maxLength: 100, nullable: true),
                    CarrierExpectedDeliveryDate = table.Column<DateTime>(nullable: true),
                    CarrierFreight = table.Column<decimal>(type: "decimal (18,5)", nullable: true),
                    BillingAddressId = table.Column<Guid>(nullable: false),
                    ShippingAddressId = table.Column<Guid>(nullable: false),
                    TrackingNumber = table.Column<string>(maxLength: 100, nullable: true),
                    CarrierAccNo = table.Column<string>(maxLength: 100, nullable: true),
                    FromAddressId = table.Column<Guid>(nullable: false),
                    ShipperAccountNo = table.Column<string>(maxLength: 100, nullable: true),
                    ShipperAccountKey = table.Column<string>(maxLength: 100, nullable: true),
                    UseCustomerCarrierAccNo = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipment", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ShipmentItem",
                schema: "ship",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    ShipmentId = table.Column<Guid>(nullable: false),
                    SalesOrderItemId = table.Column<Guid>(nullable: false),
                    ItemId = table.Column<Guid>(nullable: false),
                    ItemQuantity = table.Column<int>(nullable: false),
                    ItemUnitPrice = table.Column<decimal>(type: "decimal (18,5)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal (18,5)", nullable: false),
                    Tax = table.Column<bool>(nullable: false),
                    ItemTotatPrice = table.Column<decimal>(type: "decimal (18,5)", nullable: false),
                    ItemName = table.Column<string>(maxLength: 100, nullable: false),
                    ItemCode = table.Column<string>(maxLength: 20, nullable: false),
                    Weight = table.Column<decimal>(type: "decimal (18,5)", nullable: false),
                    WeightUnit = table.Column<string>(maxLength: 20, nullable: false),
                    Height = table.Column<decimal>(type: "decimal (18,5)", nullable: false),
                    Width = table.Column<decimal>(type: "decimal (18,5)", nullable: false),
                    Length = table.Column<decimal>(type: "decimal (18,5)", nullable: false),
                    SizeUnit = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentItem", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ShipmentPkgItem",
                schema: "ship",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    ShipmentId = table.Column<Guid>(nullable: false),
                    PackageId = table.Column<Guid>(nullable: false),
                    ShipmentItemId = table.Column<Guid>(nullable: false),
                    AddedQuantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentPkgItem", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "VerifiedAddress",
                schema: "ship",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    CarrierId = table.Column<Guid>(nullable: false),
                    AddressId = table.Column<Guid>(nullable: false),
                    VarifiedOn = table.Column<DateTime>(nullable: false),
                    VerifiedBy = table.Column<Guid>(nullable: false),
                    Verified = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerifiedAddress", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarrierPackageDetail",
                schema: "ship");

            migrationBuilder.DropTable(
                name: "CarrierPackageLinking",
                schema: "ship");

            migrationBuilder.DropTable(
                name: "FavouriteShipmentPkgSetting",
                schema: "ship");

            migrationBuilder.DropTable(
                name: "PackageMaster",
                schema: "ship");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "ship");

            migrationBuilder.DropTable(
                name: "RoleLinking",
                schema: "ship");

            migrationBuilder.DropTable(
                name: "SalesOrderPkg",
                schema: "ship");

            migrationBuilder.DropTable(
                name: "SalesOrderPkgItem",
                schema: "ship");

            migrationBuilder.DropTable(
                name: "Shipment",
                schema: "ship");

            migrationBuilder.DropTable(
                name: "ShipmentItem",
                schema: "ship");

            migrationBuilder.DropTable(
                name: "ShipmentPkgItem",
                schema: "ship");

            migrationBuilder.DropTable(
                name: "VerifiedAddress",
                schema: "ship");
        }
    }
}
