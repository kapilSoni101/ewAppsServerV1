﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ewApps.Core.Webhook.Subscriber;

namespace ewApps.Core.Webhook.Subscription.Migrations
{
    [DbContext(typeof(WebhookSubscriptionDBContext))]
    [Migration("20190829091358_initwebhooksubs")]
    partial class initwebhooksubs
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ewApps.Core.Webhook.Subscriber.WebhookEvent", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EventName")
                        .HasMaxLength(100);

                    b.Property<string>("Payload")
                        .HasMaxLength(4000);

                    b.Property<DateTime>("ReceivedDate");

                    b.Property<string>("ServerName")
                        .HasMaxLength(100);

                    b.Property<string>("SubscriptionName")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("WebhookClientEventQueue");
                });

            modelBuilder.Entity("ewApps.Core.Webhook.Subscriber.WebhookSubscription", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CallBackEndPoint")
                        .HasMaxLength(100);

                    b.Property<bool>("IsActive");

                    b.Property<string>("ServerHostEndPoint")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("ServerName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("ServerShutDownCallBackEndPoint")
                        .HasMaxLength(100);

                    b.Property<string>("SubscribedEvents")
                        .HasMaxLength(100);

                    b.Property<string>("SubscriptionName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("WebhookClientSubscription");
                });
#pragma warning restore 612, 618
        }
    }
}