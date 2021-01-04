using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.AppPortal.Data.Migrations
{
    public partial class initapppor7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PublisherAppService",
                schema: "ap",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    AppId = table.Column<Guid>(nullable: false),
                    AppServiceId = table.Column<Guid>(nullable: false),
                    AppServiceAttributeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublisherAppService", x => x.ID);
                });

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "RoleLinking",
                keyColumn: "ID",
                keyValue: new Guid("0b03bbe7-b3f7-4978-9411-2b9cb8976579"),
                columns: new[] { "CreatedBy", "UpdatedBy" },
                values: new object[] { new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PublisherAppService",
                schema: "ap");

            migrationBuilder.UpdateData(
                schema: "ap",
                table: "RoleLinking",
                keyColumn: "ID",
                keyValue: new Guid("0b03bbe7-b3f7-4978-9411-2b9cb8976579"),
                columns: new[] { "CreatedBy", "UpdatedBy" },
                values: new object[] { new Guid("4278d1fc-2bac-4c2c-be0c-25955d87733e"), new Guid("0808036c-16ef-4fe4-89be-641285b9028a") });
        }
    }
}
