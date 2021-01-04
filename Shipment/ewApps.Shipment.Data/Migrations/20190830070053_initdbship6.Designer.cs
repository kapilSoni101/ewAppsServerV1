﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ewApps.Shipment.Data;

namespace ewApps.Shipment.Data.Migrations
{
    [DbContext(typeof(ShipmentDbContext))]
    [Migration("20190830070053_initdbship6")]
    partial class initdbship6
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ewApps.Shipment.Entity.CarrierPackageDetail", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CarrierCode")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("CarrierPackageCode")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("ContainerType")
                        .HasMaxLength(200);

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<bool>("Deleted");

                    b.Property<Guid>("PackageMasterId");

                    b.Property<Guid>("TenantId");

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("ID");

                    b.ToTable("CarrierPackageDetail","ship");
                });

            modelBuilder.Entity("ewApps.Shipment.Entity.CarrierPackageLinking", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CarrierCode")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("CarrierPackageCode")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("ContainerType")
                        .HasMaxLength(250);

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<bool>("Deleted");

                    b.Property<Guid>("PackageMasterId");

                    b.Property<Guid>("TenantId");

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("ID");

                    b.ToTable("CarrierPackageLinking","ship");
                });

            modelBuilder.Entity("ewApps.Shipment.Entity.FavouriteShipmentPkgSetting", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("BusinessId");

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<Guid>("CustomerId");

                    b.Property<bool>("Deleted");

                    b.Property<string>("ItemIds");

                    b.Property<Guid>("PackageId");

                    b.Property<Guid>("TenantId");

                    b.Property<int>("Type");

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("ID");

                    b.ToTable("FavouriteShipmentPkgSetting","ship");
                });

            modelBuilder.Entity("ewApps.Shipment.Entity.PackageMaster", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<bool>("Deleted");

                    b.Property<decimal>("Height")
                        .HasColumnType("decimal(18,5)");

                    b.Property<string>("HeightUnit")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("IdentityNumber")
                        .HasMaxLength(100);

                    b.Property<decimal>("Length")
                        .HasColumnType("decimal(18,5)");

                    b.Property<string>("LengthUnit")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("PkgName")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<bool>("Status");

                    b.Property<Guid>("TenantId");

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(18,5)");

                    b.Property<string>("WeightUnit")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<decimal>("Width")
                        .HasColumnType("decimal(18,5)");

                    b.Property<string>("WidthUnit")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("PackageMaster","ship");
                });

            modelBuilder.Entity("ewApps.Shipment.Entity.Role", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<Guid?>("AppId");

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<bool>("Deleted");

                    b.Property<long>("PermissionBitMask");

                    b.Property<string>("RoleKey")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.Property<int>("UserType");

                    b.HasKey("ID");

                    b.ToTable("Role","ship");

                    b.HasData(
                        new { ID = new Guid("427ed55a-e054-4090-a18c-5900e074d624"), Active = true, AppId = new Guid("6118a7ff-742b-25db-a9c1-8e252c39bb73"), CreatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), CreatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), Deleted = false, PermissionBitMask = 511L, RoleKey = "admin", RoleName = "Admin", UpdatedBy = new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), UpdatedOn = new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), UserType = 3 }
                    );
                });

            modelBuilder.Entity("ewApps.Shipment.Entity.RoleLinking", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AppId");

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<bool>("Deleted");

                    b.Property<Guid>("RoleId");

                    b.Property<Guid>("TenantId");

                    b.Property<Guid>("TenantUserId");

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("ID");

                    b.ToTable("RoleLinking","ship");
                });

            modelBuilder.Entity("ewApps.Shipment.Entity.SalesOrderPkg", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CarrierPackageCode")
                        .HasMaxLength(100);

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<bool>("Deleted");

                    b.Property<decimal>("Height")
                        .HasColumnType("decimal(18, 5)");

                    b.Property<string>("HeightUnit")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<decimal>("Length")
                        .HasColumnType("decimal(18, 5)");

                    b.Property<string>("LengthUnit")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<Guid>("PackageId");

                    b.Property<string>("PkgName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<Guid>("SalesOrderId");

                    b.Property<Guid>("TenantId");

                    b.Property<int>("TotalItems");

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(18, 5)");

                    b.Property<string>("WeightUnit")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<decimal>("Width")
                        .HasColumnType("decimal(18, 5)");

                    b.Property<string>("WidthUnit")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("SalesOrderPkg","ship");
                });

            modelBuilder.Entity("ewApps.Shipment.Entity.SalesOrderPkgItem", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AddedQuantity");

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<bool>("Deleted");

                    b.Property<Guid>("PackageId");

                    b.Property<Guid>("SalesOrderId");

                    b.Property<Guid>("SalesOrderItemId");

                    b.Property<Guid>("SalesOrderPackageId");

                    b.Property<Guid>("TenantId");

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("ID");

                    b.ToTable("SalesOrderPkgItem","ship");
                });

            modelBuilder.Entity("ewApps.Shipment.Entity.Shipment", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal (18,5)");

                    b.Property<Guid>("BillingAddressId");

                    b.Property<string>("CarrierAccNo")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("CarrierExpectedDeliveryDate");

                    b.Property<decimal?>("CarrierFreight")
                        .HasColumnType("decimal (18,5)");

                    b.Property<Guid?>("CarrierId");

                    b.Property<DateTime?>("CarrierPickupDate");

                    b.Property<string>("CarrierPlanId")
                        .HasMaxLength(100);

                    b.Property<string>("CarrierTransitDays")
                        .HasMaxLength(100);

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<Guid>("CustomerId");

                    b.Property<bool>("Deleted");

                    b.Property<DateTime>("DeliveryDate");

                    b.Property<string>("Description")
                        .HasMaxLength(4000);

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal (18,5)");

                    b.Property<decimal>("Freight")
                        .HasColumnType("decimal (18,5)");

                    b.Property<Guid>("FromAddressId");

                    b.Property<DateTime>("PostingOn");

                    b.Property<Guid>("SalesOrderId");

                    b.Property<string>("ShipmentRefId")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("ShipmentType");

                    b.Property<string>("ShipperAccountKey")
                        .HasMaxLength(100);

                    b.Property<string>("ShipperAccountNo")
                        .HasMaxLength(100);

                    b.Property<Guid>("ShippingAddressId");

                    b.Property<int>("Status");

                    b.Property<string>("StatusText")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<decimal>("SubTotal")
                        .HasColumnType("decimal (18,5)");

                    b.Property<decimal>("Tax")
                        .HasColumnType("decimal (18,5)");

                    b.Property<decimal>("TaxRate")
                        .HasColumnType("decimal (18,5)");

                    b.Property<Guid>("TenantId");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal (18,5)");

                    b.Property<string>("TrackingNumber")
                        .HasMaxLength(100);

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.Property<bool?>("UseCustomerCarrierAccNo");

                    b.HasKey("ID");

                    b.ToTable("Shipment","ship");
                });

            modelBuilder.Entity("ewApps.Shipment.Entity.ShipmentItem", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<bool>("Deleted");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal (18,5)");

                    b.Property<decimal>("Height")
                        .HasColumnType("decimal (18,5)");

                    b.Property<string>("ItemCode")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<Guid>("ItemId");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("ItemQuantity");

                    b.Property<decimal>("ItemTotatPrice")
                        .HasColumnType("decimal (18,5)");

                    b.Property<decimal>("ItemUnitPrice")
                        .HasColumnType("decimal (18,5)");

                    b.Property<decimal>("Length")
                        .HasColumnType("decimal (18,5)");

                    b.Property<Guid>("SalesOrderItemId");

                    b.Property<Guid>("ShipmentId");

                    b.Property<string>("SizeUnit")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<bool>("Tax");

                    b.Property<Guid>("TenantId");

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal (18,5)");

                    b.Property<string>("WeightUnit")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<decimal>("Width")
                        .HasColumnType("decimal (18,5)");

                    b.HasKey("ID");

                    b.ToTable("ShipmentItem","ship");
                });

            modelBuilder.Entity("ewApps.Shipment.Entity.ShipmentPkgItem", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AddedQuantity");

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<bool>("Deleted");

                    b.Property<Guid>("PackageId");

                    b.Property<Guid>("ShipmentId");

                    b.Property<Guid>("ShipmentItemId");

                    b.Property<Guid>("TenantId");

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("ID");

                    b.ToTable("ShipmentPkgItem","ship");
                });

            modelBuilder.Entity("ewApps.Shipment.Entity.VerifiedAddress", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AddressId");

                    b.Property<Guid>("CarrierId");

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Description")
                        .HasMaxLength(4000);

                    b.Property<Guid>("TenantId");

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.Property<DateTime>("VarifiedOn");

                    b.Property<bool>("Verified");

                    b.Property<Guid>("VerifiedBy");

                    b.HasKey("ID");

                    b.ToTable("VerifiedAddress","ship");
                });
#pragma warning restore 612, 618
        }
    }
}
