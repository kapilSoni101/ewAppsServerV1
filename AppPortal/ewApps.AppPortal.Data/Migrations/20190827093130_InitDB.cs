using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.AppPortal.Data.Migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ap");

            migrationBuilder.CreateTable(
                name: "Business",
                schema: "ap",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    ContactPersonEmail = table.Column<string>(maxLength: 100, nullable: true),
                    ContactPersonName = table.Column<string>(maxLength: 100, nullable: true),
                    ContactPersonDesignation = table.Column<string>(maxLength: 100, nullable: true),
                    ContactPersonPhone = table.Column<string>(maxLength: 100, nullable: true),
                    CurrencyCode = table.Column<int>(nullable: false),
                    GroupValue = table.Column<string>(maxLength: 100, nullable: false),
                    GroupSeperator = table.Column<string>(maxLength: 20, nullable: false),
                    DecimalSeperator = table.Column<string>(maxLength: 20, nullable: false),
                    DecimalPrecision = table.Column<int>(nullable: false),
                    CanUpdateCurrency = table.Column<bool>(nullable: false),
                    Website = table.Column<string>(maxLength: 100, nullable: true),
                    Language = table.Column<string>(maxLength: 100, nullable: true),
                    TimeZone = table.Column<string>(maxLength: 100, nullable: true),
                    DateTimeFormat = table.Column<string>(nullable: true),
                    IdentityNumber = table.Column<string>(maxLength: 100, nullable: true),
                    LogoThumbnailId = table.Column<Guid>(nullable: false),
                    PrintLabelLayout = table.Column<string>(maxLength: 100, nullable: true),
                    WeightUnit = table.Column<string>(maxLength: 100, nullable: true),
                    SizeUnit = table.Column<string>(maxLength: 100, nullable: true),
                    ThemeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Business", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BusinessAddress",
                schema: "ap",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    BusinessId = table.Column<Guid>(nullable: false),
                    Label = table.Column<string>(maxLength: 20, nullable: true),
                    AddressStreet1 = table.Column<string>(maxLength: 100, nullable: true),
                    AddressStreet2 = table.Column<string>(maxLength: 100, nullable: true),
                    AddressStreet3 = table.Column<string>(maxLength: 100, nullable: true),
                    City = table.Column<string>(maxLength: 100, nullable: true),
                    Country = table.Column<string>(maxLength: 100, nullable: true),
                    State = table.Column<string>(maxLength: 100, nullable: true),
                    ZipCode = table.Column<string>(maxLength: 100, nullable: true),
                    FaxNumber = table.Column<string>(maxLength: 100, nullable: true),
                    Phone = table.Column<string>(maxLength: 100, nullable: true),
                    AddressType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessAddress", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                schema: "ap",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    BusinessPartnerTenantId = table.Column<string>(nullable: true),
                    CanUpdateCurrency = table.Column<bool>(nullable: false),
                    Configured = table.Column<bool>(nullable: false),
                    CurrencyCode = table.Column<int>(nullable: false),
                    Iden = table.Column<string>(nullable: true),
                    DateTimeFormat = table.Column<string>(nullable: true),
                    DecimalPrecision = table.Column<int>(nullable: false),
                    DecimalSeperator = table.Column<string>(nullable: true),
                    GroupSeperator = table.Column<string>(nullable: true),
                    GroupValue = table.Column<string>(nullable: true),
                    Language = table.Column<string>(nullable: true),
                    TimeZone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LevelTransitionHistory",
                schema: "ap",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    AppKey = table.Column<string>(nullable: false),
                    SourceLevel = table.Column<int>(nullable: false),
                    TargetLevel = table.Column<int>(nullable: false),
                    Status = table.Column<short>(nullable: false),
                    SupportId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelTransitionHistory", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Platform",
                schema: "ap",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    CurrencyCode = table.Column<int>(nullable: false),
                    GroupValue = table.Column<string>(maxLength: 20, nullable: false),
                    GroupSeperator = table.Column<string>(maxLength: 20, nullable: false),
                    DecimalSeperator = table.Column<string>(maxLength: 20, nullable: false),
                    DecimalPrecision = table.Column<int>(nullable: false),
                    CanUpdateCurrency = table.Column<bool>(nullable: false),
                    LogoThumbnailId = table.Column<Guid>(nullable: false),
                    Language = table.Column<string>(maxLength: 20, nullable: true),
                    TimeZone = table.Column<string>(maxLength: 20, nullable: true),
                    DateTimeFormat = table.Column<string>(maxLength: 100, nullable: true),
                    PoweredBy = table.Column<string>(maxLength: 100, nullable: false),
                    Copyright = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platform", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Portal",
                schema: "ap",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    UserType = table.Column<int>(nullable: false),
                    PortalKey = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portal", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PortalAppLinking",
                schema: "ap",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    AppId = table.Column<Guid>(nullable: false),
                    PortalId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortalAppLinking", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Publisher",
                schema: "ap",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    IdentityNumber = table.Column<string>(maxLength: 20, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    InactiveComment = table.Column<string>(maxLength: 4000, nullable: true),
                    ContactPersonName = table.Column<string>(maxLength: 100, nullable: true),
                    ContactPersonDesignation = table.Column<string>(maxLength: 100, nullable: true),
                    ContactPersonEmail = table.Column<string>(maxLength: 100, nullable: true),
                    ContactPersonPhone = table.Column<string>(maxLength: 20, nullable: true),
                    Website = table.Column<string>(maxLength: 100, nullable: true),
                    CurrencyCode = table.Column<int>(nullable: true),
                    GroupValue = table.Column<string>(maxLength: 20, nullable: true),
                    GroupSeperator = table.Column<string>(maxLength: 20, nullable: true),
                    DecimalSeperator = table.Column<string>(maxLength: 20, nullable: true),
                    DecimalPrecision = table.Column<int>(nullable: true),
                    CanUpdateCurrency = table.Column<bool>(nullable: true),
                    LogoThumbnailId = table.Column<Guid>(nullable: false),
                    Language = table.Column<string>(maxLength: 20, nullable: true),
                    TimeZone = table.Column<string>(maxLength: 20, nullable: true),
                    DateTimeFormat = table.Column<string>(maxLength: 20, nullable: true),
                    PoweredBy = table.Column<string>(maxLength: 100, nullable: true),
                    Copyright = table.Column<string>(maxLength: 100, nullable: true),
                    CustomizedLogoThumbnail = table.Column<bool>(nullable: false),
                    ApplyPoweredBy = table.Column<bool>(nullable: false),
                    CanUpdateCopyright = table.Column<bool>(nullable: false),
                    CustomizedCopyright = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publisher", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PublisherAddress",
                schema: "ap",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    PublisherId = table.Column<Guid>(nullable: false),
                    Label = table.Column<string>(maxLength: 20, nullable: true),
                    AddressStreet1 = table.Column<string>(maxLength: 100, nullable: true),
                    AddressStreet2 = table.Column<string>(maxLength: 100, nullable: true),
                    AddressStreet3 = table.Column<string>(maxLength: 100, nullable: true),
                    City = table.Column<string>(maxLength: 100, nullable: true),
                    Country = table.Column<string>(maxLength: 100, nullable: true),
                    State = table.Column<string>(maxLength: 100, nullable: true),
                    ZipCode = table.Column<string>(maxLength: 100, nullable: true),
                    FaxNumber = table.Column<string>(maxLength: 100, nullable: true),
                    Phone = table.Column<string>(maxLength: 100, nullable: true),
                    AddressType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublisherAddress", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PublisherAppSetting",
                schema: "ap",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ThumbnailId = table.Column<Guid>(nullable: true),
                    CopyrightsText = table.Column<string>(nullable: true),
                    AppId = table.Column<Guid>(nullable: false),
                    Customized = table.Column<bool>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ThemeId = table.Column<Guid>(nullable: false),
                    InactiveComment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublisherAppSetting", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SupportComment",
                schema: "ap",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    CommentText = table.Column<string>(nullable: false),
                    CreatorLevel = table.Column<short>(nullable: false),
                    SupportId = table.Column<Guid>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportComment", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SupportGroup",
                schema: "ap",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    DisplayName = table.Column<string>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    AppKey = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportGroup", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SupportTicket",
                schema: "ap",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    IdentityNumber = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Priority = table.Column<short>(nullable: false),
                    Status = table.Column<short>(nullable: false),
                    GenerationLevel = table.Column<short>(nullable: false),
                    CurrentLevel = table.Column<short>(nullable: false),
                    AppKey = table.Column<string>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportTicket", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TenantUserAppPreference",
                schema: "ap",
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
                    AppId = table.Column<Guid>(nullable: false),
                    EmailPreference = table.Column<long>(nullable: false),
                    SMSPreference = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantUserAppPreference", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TokenInfo",
                schema: "ap",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    TokenData = table.Column<string>(maxLength: 4000, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    TokenType = table.Column<int>(nullable: false),
                    TenantUserId = table.Column<Guid>(nullable: false),
                    AppKey = table.Column<string>(maxLength: 20, nullable: true),
                    AppId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokenInfo", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Business",
                schema: "ap");

            migrationBuilder.DropTable(
                name: "BusinessAddress",
                schema: "ap");

            migrationBuilder.DropTable(
                name: "Customer",
                schema: "ap");

            migrationBuilder.DropTable(
                name: "LevelTransitionHistory",
                schema: "ap");

            migrationBuilder.DropTable(
                name: "Platform",
                schema: "ap");

            migrationBuilder.DropTable(
                name: "Portal",
                schema: "ap");

            migrationBuilder.DropTable(
                name: "PortalAppLinking",
                schema: "ap");

            migrationBuilder.DropTable(
                name: "Publisher",
                schema: "ap");

            migrationBuilder.DropTable(
                name: "PublisherAddress",
                schema: "ap");

            migrationBuilder.DropTable(
                name: "PublisherAppSetting",
                schema: "ap");

            migrationBuilder.DropTable(
                name: "SupportComment",
                schema: "ap");

            migrationBuilder.DropTable(
                name: "SupportGroup",
                schema: "ap");

            migrationBuilder.DropTable(
                name: "SupportTicket",
                schema: "ap");

            migrationBuilder.DropTable(
                name: "TenantUserAppPreference",
                schema: "ap");

            migrationBuilder.DropTable(
                name: "TokenInfo",
                schema: "ap");
        }
    }
}
