using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.Core.DM.Migrations
{
    public partial class initdbdmservice33 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "core");

            migrationBuilder.RenameTable(
                name: "EntityThumbnail",
                newName: "EntityThumbnail",
                newSchema: "core");

            migrationBuilder.RenameTable(
                name: "DMThumbnail",
                newName: "DMThumbnail",
                newSchema: "core");

            migrationBuilder.RenameTable(
                name: "DMFolder",
                newName: "DMFolder",
                newSchema: "core");

            migrationBuilder.RenameTable(
                name: "DMFileStorage",
                newName: "DMFileStorage",
                newSchema: "core");

            migrationBuilder.RenameTable(
                name: "DMDocumentVersion",
                newName: "DMDocumentVersion",
                newSchema: "core");

            migrationBuilder.RenameTable(
                name: "DMDocumentUser",
                newName: "DMDocumentUser",
                newSchema: "core");

            migrationBuilder.RenameTable(
                name: "DMDocumentFolderLinking",
                newName: "DMDocumentFolderLinking",
                newSchema: "core");

            migrationBuilder.RenameTable(
                name: "DMDocument",
                newName: "DMDocument",
                newSchema: "core");

            migrationBuilder.AlterColumn<bool>(
                name: "ParentDeleted",
                schema: "core",
                table: "DMFolder",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "Deleted",
                schema: "core",
                table: "DMFolder",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "ParentDeleted",
                schema: "core",
                table: "DMDocument",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "Deleted",
                schema: "core",
                table: "DMDocument",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "EntityThumbnail",
                schema: "core",
                newName: "EntityThumbnail");

            migrationBuilder.RenameTable(
                name: "DMThumbnail",
                schema: "core",
                newName: "DMThumbnail");

            migrationBuilder.RenameTable(
                name: "DMFolder",
                schema: "core",
                newName: "DMFolder");

            migrationBuilder.RenameTable(
                name: "DMFileStorage",
                schema: "core",
                newName: "DMFileStorage");

            migrationBuilder.RenameTable(
                name: "DMDocumentVersion",
                schema: "core",
                newName: "DMDocumentVersion");

            migrationBuilder.RenameTable(
                name: "DMDocumentUser",
                schema: "core",
                newName: "DMDocumentUser");

            migrationBuilder.RenameTable(
                name: "DMDocumentFolderLinking",
                schema: "core",
                newName: "DMDocumentFolderLinking");

            migrationBuilder.RenameTable(
                name: "DMDocument",
                schema: "core",
                newName: "DMDocument");

            migrationBuilder.AlterColumn<bool>(
                name: "ParentDeleted",
                table: "DMFolder",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "Deleted",
                table: "DMFolder",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "ParentDeleted",
                table: "DMDocument",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "Deleted",
                table: "DMDocument",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: false);
        }
    }
}
