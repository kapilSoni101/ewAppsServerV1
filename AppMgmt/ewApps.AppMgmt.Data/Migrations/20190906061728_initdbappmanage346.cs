using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.AppMgmt.Data.Migrations
{
    public partial class initdbappmanage346 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlertFrequency",
                schema: "am",
                table: "TenantSubscription");

            migrationBuilder.DropColumn(
                name: "GracePeriodInDays",
                schema: "am",
                table: "TenantSubscription");

            migrationBuilder.DropColumn(
                name: "State",
                schema: "am",
                table: "TenantSubscription");

            migrationBuilder.RenameColumn(
                name: "UserLicenses",
                schema: "am",
                table: "TenantSubscription",
                newName: "Term");

            migrationBuilder.RenameColumn(
                name: "PlanSchedule",
                schema: "am",
                table: "TenantSubscription",
                newName: "BusinessUserCount");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                schema: "am",
                table: "TenantSubscription",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<decimal>(
                name: "PriceInDollar",
                schema: "am",
                table: "TenantSubscription",
                type: "decimal(18,5)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "Term",
                schema: "am",
                table: "SubscriptionPlan",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.CreateTable(
                name: "SubscriptionPlanService",
                schema: "am",
                columns: table => new
                {
                    TenantId = table.Column<Guid>(nullable: false),
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    SubscriptionPlanId = table.Column<Guid>(nullable: false),
                    AppServiceId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlanService", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPlanServiceAttribute",
                schema: "am",
                columns: table => new
                {
                    TenantId = table.Column<Guid>(nullable: false),
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    SubscriptionPlanId = table.Column<Guid>(nullable: false),
                    SubscriptionPlanServiceId = table.Column<Guid>(nullable: false),
                    AppServiceAttributeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlanServiceAttribute", x => x.ID);
                });

            migrationBuilder.UpdateData(
                schema: "am",
                table: "SubscriptionPlan",
                keyColumn: "ID",
                keyValue: new Guid("8d5e65ca-1b62-4bc3-b0f0-9b3703aabebf"),
                column: "Term",
                value: 15);

            migrationBuilder.UpdateData(
                schema: "am",
                table: "SubscriptionPlan",
                keyColumn: "ID",
                keyValue: new Guid("b37eb8b9-11bf-402e-9048-f9d0a43a426b"),
                column: "Term",
                value: 15);

            migrationBuilder.UpdateData(
                schema: "am",
                table: "SubscriptionPlan",
                keyColumn: "ID",
                keyValue: new Guid("d390a264-f0db-49ce-9415-daee1ed4b445"),
                column: "Term",
                value: 15);

            migrationBuilder.UpdateData(
                schema: "am",
                table: "TenantSubscription",
                keyColumn: "ID",
                keyValue: new Guid("f524047f-1389-412f-904d-0492858c0194"),
                columns: new[] { "BusinessUserCount", "PriceInDollar", "Term" },
                values: new object[] { 0, 0m, 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriptionPlanService",
                schema: "am");

            migrationBuilder.DropTable(
                name: "SubscriptionPlanServiceAttribute",
                schema: "am");

            migrationBuilder.RenameColumn(
                name: "Term",
                schema: "am",
                table: "TenantSubscription",
                newName: "UserLicenses");

            migrationBuilder.RenameColumn(
                name: "BusinessUserCount",
                schema: "am",
                table: "TenantSubscription",
                newName: "PlanSchedule");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                schema: "am",
                table: "TenantSubscription",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<double>(
                name: "PriceInDollar",
                schema: "am",
                table: "TenantSubscription",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,5)");

            migrationBuilder.AddColumn<int>(
                name: "AlertFrequency",
                schema: "am",
                table: "TenantSubscription",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GracePeriodInDays",
                schema: "am",
                table: "TenantSubscription",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "State",
                schema: "am",
                table: "TenantSubscription",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AlterColumn<float>(
                name: "Term",
                schema: "am",
                table: "SubscriptionPlan",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.UpdateData(
                schema: "am",
                table: "SubscriptionPlan",
                keyColumn: "ID",
                keyValue: new Guid("8d5e65ca-1b62-4bc3-b0f0-9b3703aabebf"),
                column: "Term",
                value: 15f);

            migrationBuilder.UpdateData(
                schema: "am",
                table: "SubscriptionPlan",
                keyColumn: "ID",
                keyValue: new Guid("b37eb8b9-11bf-402e-9048-f9d0a43a426b"),
                column: "Term",
                value: 15f);

            migrationBuilder.UpdateData(
                schema: "am",
                table: "SubscriptionPlan",
                keyColumn: "ID",
                keyValue: new Guid("d390a264-f0db-49ce-9415-daee1ed4b445"),
                column: "Term",
                value: 15f);

            migrationBuilder.UpdateData(
                schema: "am",
                table: "TenantSubscription",
                keyColumn: "ID",
                keyValue: new Guid("f524047f-1389-412f-904d-0492858c0194"),
                columns: new[] { "AlertFrequency", "GracePeriodInDays", "PlanSchedule", "PriceInDollar", "State", "UserLicenses" },
                values: new object[] { 4, 58, 1, 0.0, 1, 15 });
        }
    }
}
