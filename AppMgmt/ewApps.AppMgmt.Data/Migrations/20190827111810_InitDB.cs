using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.AppMgmt.Data.Migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "am");

            migrationBuilder.CreateTable(
                name: "AppService",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    AppId = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ServiceKey = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppService", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AppServiceAccountDetail",
                columns: table => new
                {
                    TenantId = table.Column<Guid>(nullable: false),
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    ServiceId = table.Column<Guid>(nullable: false),
                    ServiceAttributeId = table.Column<Guid>(nullable: false),
                    EntityId = table.Column<Guid>(nullable: false),
                    EntityType = table.Column<int>(nullable: false),
                    AccountJson = table.Column<string>(nullable: true),
                    AppId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppServiceAccountDetail", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AppServiceAttribute",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    AppServiceId = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    AttributeKey = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppServiceAttribute", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AppUserTypeLinking",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    UserType = table.Column<int>(nullable: false),
                    AppId = table.Column<Guid>(nullable: false),
                    PartnerType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserTypeLinking", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAppServiceLinking",
                columns: table => new
                {
                    TenantId = table.Column<Guid>(nullable: false),
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    ServiceId = table.Column<Guid>(nullable: false),
                    ServiceAttributeId = table.Column<Guid>(nullable: false),
                    AppId = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAppServiceLinking", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "App",
                schema: "am",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    IdentityNumber = table.Column<string>(maxLength: 100, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    ThemeId = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    AppKey = table.Column<string>(maxLength: 20, nullable: false),
                    InactiveComment = table.Column<string>(maxLength: 4000, nullable: true),
                    AppSubscriptionMode = table.Column<int>(nullable: false),
                    AppScope = table.Column<int>(nullable: false),
                    Constructed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_App", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPlan",
                schema: "am",
                columns: table => new
                {
                    TenantId = table.Column<Guid>(nullable: false),
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    IdentityNumber = table.Column<string>(maxLength: 100, nullable: false),
                    PlanName = table.Column<string>(maxLength: 100, nullable: true),
                    AppId = table.Column<Guid>(nullable: false),
                    Term = table.Column<int>(nullable: false),
                    FreeUserLicenseCount = table.Column<int>(nullable: false),
                    TrialPeriodInDays = table.Column<int>(nullable: false),
                    PlanSchedule = table.Column<int>(nullable: false),
                    PriceInDollar = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    GracePeriodInDays = table.Column<int>(nullable: false),
                    AdditionalPerUserPrice = table.Column<double>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    PaymentCycle = table.Column<int>(nullable: false),
                    NumberOfUsers = table.Column<int>(nullable: false),
                    AlertFrequency = table.Column<int>(nullable: false),
                    AllowCustomization = table.Column<bool>(nullable: false),
                    AutoRenewal = table.Column<bool>(nullable: false),
                    System = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlan", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tenant",
                schema: "am",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    IdentityNumber = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 4000, nullable: false),
                    VarId = table.Column<string>(maxLength: 100, nullable: true),
                    SubDomainName = table.Column<string>(maxLength: 50, nullable: false),
                    LogoUrl = table.Column<string>(nullable: true),
                    Language = table.Column<string>(maxLength: 100, nullable: false),
                    TimeZone = table.Column<string>(maxLength: 100, nullable: false),
                    Currency = table.Column<string>(maxLength: 100, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    TenantType = table.Column<int>(nullable: false),
                    InvitedOn = table.Column<DateTime>(nullable: true),
                    joinedOn = table.Column<DateTime>(nullable: true),
                    InvitedBy = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TenantAppServiceLinking",
                schema: "am",
                columns: table => new
                {
                    TenantId = table.Column<Guid>(nullable: false),
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    ServiceId = table.Column<Guid>(nullable: false),
                    ServiceAttributeId = table.Column<Guid>(nullable: false),
                    AppId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantAppServiceLinking", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TenantLinking",
                schema: "am",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    PlatformTenantId = table.Column<Guid>(nullable: true),
                    PublisherTenantId = table.Column<Guid>(nullable: true),
                    BusinessTenantId = table.Column<Guid>(nullable: true),
                    BusinessPartnerTenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantLinking", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TenantSubscription",
                schema: "am",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    AppId = table.Column<Guid>(nullable: false),
                    SystemConfId = table.Column<Guid>(nullable: false),
                    SubscriptionStartDate = table.Column<DateTime>(nullable: false),
                    SubscriptionStartEnd = table.Column<DateTime>(nullable: false),
                    UserLicenses = table.Column<int>(nullable: false),
                    SubscriptionPlanId = table.Column<Guid>(nullable: false),
                    PlanSchedule = table.Column<int>(nullable: false),
                    PriceInDollar = table.Column<double>(nullable: false),
                    GracePeriodInDays = table.Column<int>(nullable: false),
                    AutoRenew = table.Column<bool>(nullable: false),
                    AlertFrequency = table.Column<int>(nullable: false),
                    PaymentCycle = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    InactiveComment = table.Column<string>(maxLength: 4000, nullable: true),
                    CustomizeSubscription = table.Column<bool>(nullable: false),
                    LogoThumbnailId = table.Column<Guid>(nullable: false),
                    ThemeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantSubscription", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TenantUser",
                schema: "am",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    IdentityUserId = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    FullName = table.Column<string>(maxLength: 200, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Code = table.Column<string>(maxLength: 4000, nullable: true),
                    IdentityNumber = table.Column<string>(maxLength: 20, nullable: true),
                    Phone = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantUser", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TenantUserAppLastAccessInfo",
                schema: "am",
                columns: table => new
                {
                    TenantId = table.Column<Guid>(nullable: false),
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    LoginDateTime = table.Column<DateTime>(nullable: false),
                    TimeZone = table.Column<string>(maxLength: 20, nullable: true),
                    Region = table.Column<string>(maxLength: 20, nullable: true),
                    Browser = table.Column<string>(maxLength: 20, nullable: true),
                    Language = table.Column<string>(maxLength: 20, nullable: true),
                    AppId = table.Column<Guid>(nullable: false),
                    TenantUserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantUserAppLastAccessInfo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TenantUserAppLinking",
                schema: "am",
                columns: table => new
                {
                    TenantId = table.Column<Guid>(nullable: false),
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    AppId = table.Column<Guid>(nullable: false),
                    TenantUserId = table.Column<Guid>(nullable: false),
                    BusinessPartnerTenantId = table.Column<Guid>(nullable: true),
                    JoinedDate = table.Column<DateTime>(nullable: true),
                    InvitedOn = table.Column<DateTime>(nullable: true),
                    InvitedBy = table.Column<Guid>(nullable: true),
                    UserType = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantUserAppLinking", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Theme",
                schema: "am",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    ThemeName = table.Column<string>(maxLength: 100, nullable: false),
                    PreviewImageUrl = table.Column<string>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ThemeKey = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Theme", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserTenantLinking",
                schema: "am",
                columns: table => new
                {
                    TenantId = table.Column<Guid>(nullable: false),
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantUserId = table.Column<Guid>(nullable: false),
                    BusinessPartnerTenantId = table.Column<Guid>(nullable: true),
                    UserType = table.Column<int>(nullable: false),
                    PartnerType = table.Column<int>(nullable: true),
                    IsPrimary = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTenantLinking", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppService");

            migrationBuilder.DropTable(
                name: "AppServiceAccountDetail");

            migrationBuilder.DropTable(
                name: "AppServiceAttribute");

            migrationBuilder.DropTable(
                name: "AppUserTypeLinking");

            migrationBuilder.DropTable(
                name: "CustomerAppServiceLinking");

            migrationBuilder.DropTable(
                name: "App",
                schema: "am");

            migrationBuilder.DropTable(
                name: "SubscriptionPlan",
                schema: "am");

            migrationBuilder.DropTable(
                name: "Tenant",
                schema: "am");

            migrationBuilder.DropTable(
                name: "TenantAppServiceLinking",
                schema: "am");

            migrationBuilder.DropTable(
                name: "TenantLinking",
                schema: "am");

            migrationBuilder.DropTable(
                name: "TenantSubscription",
                schema: "am");

            migrationBuilder.DropTable(
                name: "TenantUser",
                schema: "am");

            migrationBuilder.DropTable(
                name: "TenantUserAppLastAccessInfo",
                schema: "am");

            migrationBuilder.DropTable(
                name: "TenantUserAppLinking",
                schema: "am");

            migrationBuilder.DropTable(
                name: "Theme",
                schema: "am");

            migrationBuilder.DropTable(
                name: "UserTenantLinking",
                schema: "am");
        }
    }
}
