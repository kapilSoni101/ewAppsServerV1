﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ewApps.Core.Webhook.Server;

namespace ewApps.Core.Webhook.Server.Migrations
{
    [DbContext(typeof(WebhookDBContext))]
    [Migration("20190829091153_initwebhookserver")]
    partial class initwebhookserver
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ewApps.Core.Webhook.Server.WebhookEvent", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("EventName")
                        .HasMaxLength(100);

                    b.Property<string>("Payload")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("WebhookEventQueue");
                });

            modelBuilder.Entity("ewApps.Core.Webhook.Server.WebhookEventDeliveryLog", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DeliveryAttempts");

                    b.Property<string>("DeliveryStatus")
                        .HasMaxLength(20);

                    b.Property<string>("EventName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("EventQueueTime");

                    b.Property<DateTime>("LastDeliveryTime");

                    b.Property<string>("Payload")
                        .HasMaxLength(4000);

                    b.Property<string>("ServerName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("SubscriptionCallBack")
                        .HasMaxLength(100);

                    b.Property<string>("SubscriptionName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("WebhookEventDeliveryLog");
                });

            modelBuilder.Entity("ewApps.Core.Webhook.Server.WebhookServer", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ServerEvents")
                        .HasMaxLength(100);

                    b.Property<string>("ServerName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("WebhookServer");
                });

            modelBuilder.Entity("ewApps.Core.Webhook.Server.WebhookSubscription", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CallbackEndPoint")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<bool>("IsActive");

                    b.Property<string>("ServerName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("ServerShutDownCallBackEndPoint")
                        .HasMaxLength(100);

                    b.Property<string>("SubscribeEvents")
                        .HasMaxLength(100);

                    b.Property<string>("SubscriptionName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("WebhookSubscription");
                });
#pragma warning restore 612, 618
        }
    }
}
