﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ewApps.Core.SMSService;

namespace ewApps.Core.SMSService.Migrations
{
    [DbContext(typeof(SMSDbContext))]
    [Migration("20190829091623_initwebhooksmsserv")]
    partial class initwebhooksmsserv
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ewApps.Core.SMSService.SMSDeliveryLog", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ActualDeliveryTime");

                    b.Property<Guid>("ApplicationId");

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<bool>("Deleted");

                    b.Property<int>("DeliverySubType");

                    b.Property<int>("DeliveryType");

                    b.Property<string>("MessagePart1")
                        .HasMaxLength(4000);

                    b.Property<string>("MessagePart2")
                        .IsRequired()
                        .HasMaxLength(4000);

                    b.Property<Guid>("NotificationId");

                    b.Property<string>("Recipient")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("ScheduledDeliveryTime");

                    b.HasKey("ID");

                    b.ToTable("SMSDeliveryLog");
                });

            modelBuilder.Entity("ewApps.Core.SMSService.SMSQueue", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ApplicationId");

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<bool>("Deleted");

                    b.Property<int>("DeliverySubType");

                    b.Property<DateTime>("DeliveryTime");

                    b.Property<int>("DeliveryType");

                    b.Property<string>("MessagePart1")
                        .HasMaxLength(4000);

                    b.Property<string>("MessagePart2")
                        .IsRequired()
                        .HasMaxLength(4000);

                    b.Property<Guid>("NotificationId");

                    b.Property<string>("Recipient")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("State");

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime>("UpdatedOn");

                    b.HasKey("ID");

                    b.ToTable("SMSQueue");
                });
#pragma warning restore 612, 618
        }
    }
}
