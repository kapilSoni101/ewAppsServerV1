using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.BusinessEntity.Data.Migrations
{
    public partial class initbusinessentitydb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "ERPConnectorConfig",
                newName: "ERPConnectorConfig",
                newSchema: "be");

            migrationBuilder.RenameTable(
                name: "ERPConnector",
                newName: "ERPConnector",
                newSchema: "be");

            migrationBuilder.AlterColumn<string>(
                name: "ERPItemKey",
                schema: "be",
                table: "BAItemMaster",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "ERPConnectorKey",
                schema: "be",
                table: "BAItemMaster",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<int>(
                name: "StatusText",
                schema: "be",
                table: "BACustomer",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(int),
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                schema: "be",
                table: "BACustomer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ShippingType",
                schema: "be",
                table: "BACustomer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "be",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    RoleName = table.Column<string>(maxLength: 100, nullable: false),
                    RoleKey = table.Column<string>(maxLength: 100, nullable: false),
                    PermissionBitMask = table.Column<long>(nullable: false),
                    AppId = table.Column<Guid>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    UserType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RoleLinking",
                schema: "be",
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
                    RoleId = table.Column<Guid>(nullable: false),
                    AppId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleLinking", x => x.ID);
                });

            migrationBuilder.InsertData(
                schema: "be",
                table: "ERPConnector",
                columns: new[] { "ID", "Active", "ConnectorKey", "CreatedBy", "CreatedOn", "Deleted", "Name", "TenantId", "UpdatedBy", "UpdatedOn" },
                values: new object[] { new Guid("aaea4427-53f8-4ef8-b821-caff358cbd92"), true, null, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, "SAP", new Guid("18571765-24b5-4c36-a957-416eaec38fda"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Role",
                schema: "be");

            migrationBuilder.DropTable(
                name: "RoleLinking",
                schema: "be");

            migrationBuilder.DeleteData(
                schema: "be",
                table: "ERPConnector",
                keyColumn: "ID",
                keyValue: new Guid("aaea4427-53f8-4ef8-b821-caff358cbd92"));

            migrationBuilder.RenameTable(
                name: "ERPConnectorConfig",
                schema: "be",
                newName: "ERPConnectorConfig");

            migrationBuilder.RenameTable(
                name: "ERPConnector",
                schema: "be",
                newName: "ERPConnector");

            migrationBuilder.AlterColumn<string>(
                name: "ERPItemKey",
                schema: "be",
                table: "BAItemMaster",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "ERPConnectorKey",
                schema: "be",
                table: "BAItemMaster",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "StatusText",
                schema: "be",
                table: "BACustomer",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                schema: "be",
                table: "BACustomer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ShippingType",
                schema: "be",
                table: "BACustomer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
