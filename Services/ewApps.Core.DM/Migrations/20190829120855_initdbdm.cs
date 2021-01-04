using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.Core.DM.Migrations
{
    public partial class initdbdm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DMDocument",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: false),
                    OwnerEntityType = table.Column<int>(nullable: false),
                    OwnerEntityId = table.Column<Guid>(nullable: false),
                    FileName = table.Column<string>(maxLength: 200, nullable: false),
                    FileExtension = table.Column<string>(maxLength: 50, nullable: false),
                    FileSizeinKB = table.Column<float>(nullable: false),
                    CurrentVersionNumber = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 4000, nullable: true),
                    FileStorageId = table.Column<Guid>(nullable: false),
                    StorageType = table.Column<int>(nullable: false),
                    AppId = table.Column<Guid>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    ParentDeleted = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DMDocument", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DMDocumentFolderLinking",
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
                    DocumentId = table.Column<Guid>(nullable: false),
                    FolderId = table.Column<Guid>(nullable: false),
                    FolderType = table.Column<int>(nullable: false),
                    AppId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DMDocumentFolderLinking", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DMDocumentUser",
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
                    DocumentId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    LinkedEntityId = table.Column<Guid>(nullable: false),
                    LinkedEntityType = table.Column<int>(nullable: false),
                    LinkedEntityName = table.Column<string>(nullable: false),
                    LinkedGroupMemberId = table.Column<Guid>(nullable: true),
                    LinkedGroupMemberType = table.Column<int>(nullable: true),
                    LinkedGroupMemberName = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DMDocumentUser", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DMDocumentVersion",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    DocumentId = table.Column<Guid>(nullable: false),
                    PhysicalPath = table.Column<string>(maxLength: 200, nullable: false),
                    VersionNumber = table.Column<int>(nullable: false),
                    OwnerEntityType = table.Column<int>(nullable: false),
                    OwnerEntityId = table.Column<Guid>(nullable: false),
                    ThumbnailId = table.Column<Guid>(nullable: false),
                    FileName = table.Column<string>(maxLength: 200, nullable: false),
                    FileExtension = table.Column<string>(maxLength: 50, nullable: false),
                    FileSizeinKB = table.Column<float>(nullable: false),
                    FileStorageId = table.Column<Guid>(nullable: false),
                    StorageType = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DMDocumentVersion", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DMFileStorage",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    FileName = table.Column<string>(maxLength: 200, nullable: false),
                    Size = table.Column<float>(nullable: false),
                    FilePath = table.Column<string>(maxLength: 200, nullable: false),
                    StorageType = table.Column<int>(nullable: true),
                    MimeType = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DMFileStorage", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DMFolder",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: false),
                    FolderId = table.Column<Guid>(nullable: false),
                    TenantUserId = table.Column<Guid>(nullable: false),
                    FolderName = table.Column<string>(maxLength: 200, nullable: false),
                    ParentFolderId = table.Column<Guid>(nullable: true),
                    FolderType = table.Column<int>(nullable: false),
                    FileCount = table.Column<int>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    ParentDeleted = table.Column<bool>(nullable: false),
                    AppId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DMFolder", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DMThumbnail",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    DocumentId = table.Column<Guid>(nullable: false),
                    OwnerEntityType = table.Column<int>(nullable: false),
                    OwnerEntityId = table.Column<Guid>(nullable: false),
                    FileName = table.Column<string>(maxLength: 200, nullable: false),
                    FileExtension = table.Column<string>(maxLength: 50, nullable: true),
                    FileSizeinKB = table.Column<double>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    Width = table.Column<int>(nullable: false),
                    Duration = table.Column<double>(nullable: false),
                    MediaType = table.Column<int>(nullable: false),
                    DocumentFileName = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DMThumbnail", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EntityThumbnail",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    OwnerEntityType = table.Column<int>(nullable: false),
                    OwnerEntityId = table.Column<Guid>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    FileExtension = table.Column<string>(nullable: true),
                    FileSizeinKB = table.Column<double>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    Width = table.Column<int>(nullable: false),
                    Duration = table.Column<double>(nullable: false),
                    MediaType = table.Column<int>(nullable: false),
                    DocumentFileName = table.Column<string>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityThumbnail", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DMDocument");

            migrationBuilder.DropTable(
                name: "DMDocumentFolderLinking");

            migrationBuilder.DropTable(
                name: "DMDocumentUser");

            migrationBuilder.DropTable(
                name: "DMDocumentVersion");

            migrationBuilder.DropTable(
                name: "DMFileStorage");

            migrationBuilder.DropTable(
                name: "DMFolder");

            migrationBuilder.DropTable(
                name: "DMThumbnail");

            migrationBuilder.DropTable(
                name: "EntityThumbnail");
        }
    }
}
