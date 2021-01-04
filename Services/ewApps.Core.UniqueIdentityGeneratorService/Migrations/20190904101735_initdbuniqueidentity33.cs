using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.Core.UniqueIdentityGeneratorService.Migrations
{
    public partial class initdbuniqueidentity33 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "core");

            migrationBuilder.RenameTable(
                name: "UniqueIdentityGenerator",
                newName: "UniqueIdentityGenerator",
                newSchema: "core");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "UniqueIdentityGenerator",
                schema: "core",
                newName: "UniqueIdentityGenerator");
        }
    }
}
