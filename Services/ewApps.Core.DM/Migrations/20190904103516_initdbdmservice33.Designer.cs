﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ewApps.Core.DMService;

namespace ewApps.Core.DM.Migrations
{
    [DbContext(typeof(DMDBContext))]
    [Migration("20190904103516_initdbdmservice33")]
    partial class initdbdmservice33
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ewApps.Core.DMService.DMDocument", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AppId");

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<int>("CurrentVersionNumber");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("Description")
                        .HasMaxLength(4000);

                    b.Property<string>("FileExtension")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<float>("FileSizeinKB");

                    b.Property<Guid>("FileStorageId");

                    b.Property<Guid>("OwnerEntityId");

                    b.Property<int>("OwnerEntityType");

                    b.Property<bool>("ParentDeleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<int>("StorageType");

                    b.Property<Guid>("TenantId");

                    b.Property<string>("Title")
                        .HasMaxLength(200);

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("ID");

                    b.ToTable("DMDocument","core");
                });

            modelBuilder.Entity("ewApps.Core.DMService.DMDocumentFolderLinking", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AppId");

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<bool>("Deleted");

                    b.Property<Guid>("DocumentId");

                    b.Property<Guid>("FolderId");

                    b.Property<int>("FolderType");

                    b.Property<Guid>("TenantId");

                    b.Property<Guid>("TenantUserId");

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("ID");

                    b.ToTable("DMDocumentFolderLinking","core");
                });

            modelBuilder.Entity("ewApps.Core.DMService.DMDocumentUser", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<bool>("Deleted");

                    b.Property<Guid>("DocumentId");

                    b.Property<Guid>("LinkedEntityId");

                    b.Property<string>("LinkedEntityName")
                        .IsRequired();

                    b.Property<int>("LinkedEntityType");

                    b.Property<Guid?>("LinkedGroupMemberId");

                    b.Property<string>("LinkedGroupMemberName")
                        .HasMaxLength(200);

                    b.Property<int?>("LinkedGroupMemberType");

                    b.Property<Guid>("RoleId");

                    b.Property<Guid>("TenantId");

                    b.Property<Guid>("TenantUserId");

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("ID");

                    b.ToTable("DMDocumentUser","core");
                });

            modelBuilder.Entity("ewApps.Core.DMService.DMDocumentVersion", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<bool>("Deleted");

                    b.Property<Guid>("DocumentId");

                    b.Property<string>("FileExtension")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<float>("FileSizeinKB");

                    b.Property<Guid>("FileStorageId");

                    b.Property<Guid>("OwnerEntityId");

                    b.Property<int>("OwnerEntityType");

                    b.Property<string>("PhysicalPath")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int>("StorageType");

                    b.Property<Guid>("TenantId");

                    b.Property<Guid>("ThumbnailId");

                    b.Property<string>("Title")
                        .HasMaxLength(200);

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.Property<int>("VersionNumber");

                    b.HasKey("ID");

                    b.ToTable("DMDocumentVersion","core");
                });

            modelBuilder.Entity("ewApps.Core.DMService.DMFileStorage", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<bool>("Deleted");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("MimeType")
                        .HasMaxLength(50);

                    b.Property<float>("Size");

                    b.Property<int?>("StorageType");

                    b.Property<Guid>("TenantId");

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("ID");

                    b.ToTable("DMFileStorage","core");
                });

            modelBuilder.Entity("ewApps.Core.DMService.DMFolder", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AppId");

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<int?>("FileCount");

                    b.Property<Guid>("FolderId");

                    b.Property<string>("FolderName")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int>("FolderType");

                    b.Property<bool>("ParentDeleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<Guid?>("ParentFolderId");

                    b.Property<Guid>("TenantId");

                    b.Property<Guid>("TenantUserId");

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("ID");

                    b.ToTable("DMFolder","core");
                });

            modelBuilder.Entity("ewApps.Core.DMService.DMThumbnail", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<bool>("Deleted");

                    b.Property<string>("DocumentFileName")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<Guid>("DocumentId");

                    b.Property<double>("Duration");

                    b.Property<string>("FileExtension")
                        .HasMaxLength(50);

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<double>("FileSizeinKB");

                    b.Property<int>("Height");

                    b.Property<int>("MediaType");

                    b.Property<Guid>("OwnerEntityId");

                    b.Property<int>("OwnerEntityType");

                    b.Property<Guid>("TenantId");

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.Property<int>("Width");

                    b.HasKey("ID");

                    b.ToTable("DMThumbnail","core");
                });

            modelBuilder.Entity("ewApps.Core.DMService.EntityThumbnail", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<bool>("Deleted");

                    b.Property<string>("DocumentFileName");

                    b.Property<double>("Duration");

                    b.Property<string>("FileExtension");

                    b.Property<string>("FileName");

                    b.Property<double>("FileSizeinKB");

                    b.Property<int>("Height");

                    b.Property<int>("MediaType");

                    b.Property<Guid>("OwnerEntityId");

                    b.Property<int>("OwnerEntityType");

                    b.Property<Guid>("TenantId");

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.Property<int>("Width");

                    b.HasKey("ID");

                    b.ToTable("EntityThumbnail","core");
                });
#pragma warning restore 612, 618
        }
    }
}