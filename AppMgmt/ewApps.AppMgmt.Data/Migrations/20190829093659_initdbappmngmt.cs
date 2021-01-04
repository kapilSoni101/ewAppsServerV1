using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.AppMgmt.Data.Migrations
{
    public partial class initdbappmngmt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "CustomerAppServiceLinking",
                newName: "CustomerAppServiceLinking",
                newSchema: "am");

            migrationBuilder.RenameTable(
                name: "AppUserTypeLinking",
                newName: "AppUserTypeLinking",
                newSchema: "am");

            migrationBuilder.RenameTable(
                name: "AppServiceAttribute",
                newName: "AppServiceAttribute",
                newSchema: "am");

            migrationBuilder.RenameTable(
                name: "AppServiceAccountDetail",
                newName: "AppServiceAccountDetail",
                newSchema: "am");

            migrationBuilder.RenameTable(
                name: "AppService",
                newName: "AppService",
                newSchema: "am");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "CustomerAppServiceLinking",
                schema: "am",
                newName: "CustomerAppServiceLinking");

            migrationBuilder.RenameTable(
                name: "AppUserTypeLinking",
                schema: "am",
                newName: "AppUserTypeLinking");

            migrationBuilder.RenameTable(
                name: "AppServiceAttribute",
                schema: "am",
                newName: "AppServiceAttribute");

            migrationBuilder.RenameTable(
                name: "AppServiceAccountDetail",
                schema: "am",
                newName: "AppServiceAccountDetail");

            migrationBuilder.RenameTable(
                name: "AppService",
                schema: "am",
                newName: "AppService");
        }
    }
}
