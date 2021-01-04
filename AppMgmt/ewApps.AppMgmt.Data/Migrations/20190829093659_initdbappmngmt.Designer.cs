﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ewApps.AppMgmt.Data;

namespace ewApps.AppMgmt.Data.Migrations
{
    [DbContext(typeof(AppMgmtDbContext))]
    [Migration("20190829093659_initdbappmngmt")]
    partial class initdbappmngmt
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ewApps.AppMgmt.Entity.App", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("AppKey")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int>("AppScope");

                    b.Property<int>("AppSubscriptionMode");

                    b.Property<bool>("Constructed");

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<bool>("Deleted");

                    b.Property<string>("IdentityNumber")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("InactiveComment")
                        .HasMaxLength(4000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<Guid>("ThemeId");

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("ID");

                    b.ToTable("App","am");
                });

            modelBuilder.Entity("ewApps.AppMgmt.Entity.AppService", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<Guid>("AppId");

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("ServiceKey")
                        .HasMaxLength(20);

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("ID");

                    b.ToTable("AppService","am");
                });

            modelBuilder.Entity("ewApps.AppMgmt.Entity.AppServiceAccountDetail", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountJson");

                    b.Property<Guid>("AppId");

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<bool>("Deleted");

                    b.Property<Guid>("EntityId");

                    b.Property<int>("EntityType");

                    b.Property<Guid>("ServiceAttributeId");

                    b.Property<Guid>("ServiceId");

                    b.Property<Guid>("TenantId");

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("ID");

                    b.ToTable("AppServiceAccountDetail","am");
                });

            modelBuilder.Entity("ewApps.AppMgmt.Entity.AppServiceAttribute", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<Guid>("AppServiceId");

                    b.Property<string>("AttributeKey")
                        .HasMaxLength(20);

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("ID");

                    b.ToTable("AppServiceAttribute","am");
                });

            modelBuilder.Entity("ewApps.AppMgmt.Entity.AppUserTypeLinking", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AppId");

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<bool>("Deleted");

                    b.Property<int>("PartnerType");

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.Property<int>("UserType");

                    b.HasKey("ID");

                    b.ToTable("AppUserTypeLinking","am");
                });

            modelBuilder.Entity("ewApps.AppMgmt.Entity.CustomerAppServiceLinking", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AppId");

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<Guid>("CustomerId");

                    b.Property<bool>("Deleted");

                    b.Property<Guid>("ServiceAttributeId");

                    b.Property<Guid>("ServiceId");

                    b.Property<Guid>("TenantId");

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("ID");

                    b.ToTable("CustomerAppServiceLinking","am");
                });

            modelBuilder.Entity("ewApps.AppMgmt.Entity.SubscriptionPlan", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<double>("AdditionalPerUserPrice");

                    b.Property<int>("AlertFrequency");

                    b.Property<bool>("AllowCustomization");

                    b.Property<Guid>("AppId");

                    b.Property<bool>("AutoRenewal");

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<bool>("Deleted");

                    b.Property<int>("FreeUserLicenseCount");

                    b.Property<int>("GracePeriodInDays");

                    b.Property<string>("IdentityNumber")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("NumberOfUsers");

                    b.Property<int>("PaymentCycle");

                    b.Property<string>("PlanName")
                        .HasMaxLength(100);

                    b.Property<int>("PlanSchedule");

                    b.Property<decimal>("PriceInDollar")
                        .HasConversion(new ValueConverter<decimal, decimal>(v => default(decimal), v => default(decimal), new ConverterMappingHints(precision: 38, scale: 17)))
                        .HasColumnType("decimal(18,5)");

                    b.Property<bool>("System");

                    b.Property<Guid>("TenantId");

                    b.Property<int>("Term");

                    b.Property<int>("TrialPeriodInDays");

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("ID");

                    b.ToTable("SubscriptionPlan","am");
                });

            modelBuilder.Entity("ewApps.AppMgmt.Entity.Tenant", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<bool>("Deleted");

                    b.Property<string>("IdentityNumber")
                        .IsRequired();

                    b.Property<Guid?>("InvitedBy");

                    b.Property<DateTime?>("InvitedOn");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("LogoUrl");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(4000);

                    b.Property<string>("SubDomainName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("TenantType");

                    b.Property<string>("TimeZone")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.Property<string>("VarId")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("joinedOn");

                    b.HasKey("ID");

                    b.ToTable("Tenant","am");
                });

            modelBuilder.Entity("ewApps.AppMgmt.Entity.TenantAppServiceLinking", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AppId");

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<bool>("Deleted");

                    b.Property<Guid>("ServiceAttributeId");

                    b.Property<Guid>("ServiceId");

                    b.Property<Guid>("TenantId");

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("ID");

                    b.ToTable("TenantAppServiceLinking","am");
                });

            modelBuilder.Entity("ewApps.AppMgmt.Entity.TenantLinking", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("BusinessPartnerTenantId");

                    b.Property<Guid?>("BusinessTenantId");

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<Guid?>("PlatformTenantId");

                    b.Property<Guid?>("PublisherTenantId");

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("ID");

                    b.ToTable("TenantLinking","am");
                });

            modelBuilder.Entity("ewApps.AppMgmt.Entity.TenantSubscription", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AlertFrequency");

                    b.Property<Guid>("AppId");

                    b.Property<bool>("AutoRenew");

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<bool>("CustomizeSubscription");

                    b.Property<bool>("Deleted");

                    b.Property<int>("GracePeriodInDays");

                    b.Property<string>("InactiveComment")
                        .HasMaxLength(4000);

                    b.Property<Guid>("LogoThumbnailId");

                    b.Property<int>("PaymentCycle");

                    b.Property<int>("PlanSchedule");

                    b.Property<double>("PriceInDollar");

                    b.Property<int>("State");

                    b.Property<int>("Status");

                    b.Property<Guid>("SubscriptionPlanId");

                    b.Property<DateTime>("SubscriptionStartDate");

                    b.Property<DateTime>("SubscriptionStartEnd");

                    b.Property<Guid>("SystemConfId");

                    b.Property<Guid>("TenantId");

                    b.Property<Guid>("ThemeId");

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.Property<int>("UserLicenses");

                    b.HasKey("ID");

                    b.ToTable("TenantSubscription","am");
                });

            modelBuilder.Entity("ewApps.AppMgmt.Entity.TenantUser", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .HasMaxLength(4000);

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("IdentityNumber")
                        .HasMaxLength(20);

                    b.Property<Guid>("IdentityUserId");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Phone")
                        .HasMaxLength(20);

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("ID");

                    b.ToTable("TenantUser","am");
                });

            modelBuilder.Entity("ewApps.AppMgmt.Entity.TenantUserAppLastAccessInfo", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AppId");

                    b.Property<string>("Browser")
                        .HasMaxLength(20);

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<string>("Language")
                        .HasMaxLength(20);

                    b.Property<DateTime>("LoginDateTime");

                    b.Property<string>("Region")
                        .HasMaxLength(20);

                    b.Property<Guid>("TenantId");

                    b.Property<Guid>("TenantUserId");

                    b.Property<string>("TimeZone")
                        .HasMaxLength(20);

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("ID");

                    b.ToTable("TenantUserAppLastAccessInfo","am");
                });

            modelBuilder.Entity("ewApps.AppMgmt.Entity.TenantUserAppLinking", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<Guid>("AppId");

                    b.Property<Guid?>("BusinessPartnerTenantId");

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<bool>("Deleted");

                    b.Property<Guid?>("InvitedBy");

                    b.Property<DateTime?>("InvitedOn");

                    b.Property<DateTime?>("JoinedDate");

                    b.Property<int>("Status");

                    b.Property<Guid>("TenantId");

                    b.Property<Guid>("TenantUserId");

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.Property<int>("UserType");

                    b.HasKey("ID");

                    b.ToTable("TenantUserAppLinking","am");
                });

            modelBuilder.Entity("ewApps.AppMgmt.Entity.Theme", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<bool>("Deleted");

                    b.Property<string>("PreviewImageUrl")
                        .IsRequired();

                    b.Property<string>("ThemeKey")
                        .IsRequired();

                    b.Property<string>("ThemeName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("ID");

                    b.ToTable("Theme","am");
                });

            modelBuilder.Entity("ewApps.AppMgmt.Entity.UserTenantLinking", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("BusinessPartnerTenantId");

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<bool>("Deleted");

                    b.Property<bool>("IsPrimary");

                    b.Property<int?>("PartnerType");

                    b.Property<Guid>("TenantId");

                    b.Property<Guid>("TenantUserId");

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.Property<int>("UserType");

                    b.HasKey("ID");

                    b.ToTable("UserTenantLinking","am");
                });
#pragma warning restore 612, 618
        }
    }
}
