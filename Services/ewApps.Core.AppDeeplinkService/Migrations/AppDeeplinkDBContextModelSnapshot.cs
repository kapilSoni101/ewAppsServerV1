﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ewApps.Core.AppDeeplinkService;

namespace ewApps.Core.AppDeeplinkService.Migrations
{
    [DbContext(typeof(AppDeeplinkDBContext))]
    partial class AppDeeplinkDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ewApps.Core.AppDeeplinkService.AppDeeplink", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActionData")
                        .HasMaxLength(100);

                    b.Property<string>("ActionEndpointUrl")
                        .HasMaxLength(100);

                    b.Property<string>("ActionName")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("ExpirationDate");

                    b.Property<int>("MaxUseCount");

                    b.Property<long>("NumberId");

                    b.Property<string>("Password")
                        .HasMaxLength(20);

                    b.Property<string>("ShortUrlKey");

                    b.Property<Guid>("TenantId");

                    b.Property<int>("UserAccessCount");

                    b.Property<Guid?>("UserId");

                    b.HasKey("ID");

                    b.ToTable("AppDeeplink","core");
                });

            modelBuilder.Entity("ewApps.Core.AppDeeplinkService.AppDeeplinkAccessLog", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AccessGranted");

                    b.Property<string>("AccessNotGrantedReason")
                        .HasMaxLength(100);

                    b.Property<DateTime>("AccessTimestamp");

                    b.Property<string>("AccessUrl")
                        .HasMaxLength(100);

                    b.Property<string>("AccessorIPAddress")
                        .HasMaxLength(100);

                    b.Property<Guid>("AppDeeplinkId");

                    b.HasKey("ID");

                    b.ToTable("AppDeeplinkAccessLog","core");
                });
#pragma warning restore 612, 618
        }
    }
}