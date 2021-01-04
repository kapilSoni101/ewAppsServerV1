using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.AppMgmt.Data.Migrations
{
    public partial class initdbappmanage345 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "am",
                table: "SubscriptionPlan",
                keyColumn: "ID",
                keyValue: new Guid("29f50252-433f-4de9-8a4b-7a2360b4ee44"));

            migrationBuilder.DeleteData(
                schema: "am",
                table: "SubscriptionPlan",
                keyColumn: "ID",
                keyValue: new Guid("98276e8b-78eb-4b87-bfff-1d20d82b446f"));

            migrationBuilder.DropColumn(
                name: "AdditionalPerUserPrice",
                schema: "am",
                table: "SubscriptionPlan");

            migrationBuilder.DropColumn(
                name: "AlertFrequency",
                schema: "am",
                table: "SubscriptionPlan");

            migrationBuilder.DropColumn(
                name: "AllowCustomization",
                schema: "am",
                table: "SubscriptionPlan");

            migrationBuilder.DropColumn(
                name: "AutoRenewal",
                schema: "am",
                table: "SubscriptionPlan");

            migrationBuilder.DropColumn(
                name: "FreeUserLicenseCount",
                schema: "am",
                table: "SubscriptionPlan");

            migrationBuilder.DropColumn(
                name: "GracePeriodInDays",
                schema: "am",
                table: "SubscriptionPlan");

            migrationBuilder.DropColumn(
                name: "NumberOfUsers",
                schema: "am",
                table: "SubscriptionPlan");

            migrationBuilder.RenameColumn(
                name: "joinedOn",
                schema: "am",
                table: "Tenant",
                newName: "JoinedOn");

            migrationBuilder.RenameColumn(
                name: "TrialPeriodInDays",
                schema: "am",
                table: "SubscriptionPlan",
                newName: "TransactionCount");

            migrationBuilder.RenameColumn(
                name: "System",
                schema: "am",
                table: "SubscriptionPlan",
                newName: "AllowUnlimitedTransaction");

            migrationBuilder.RenameColumn(
                name: "PlanSchedule",
                schema: "am",
                table: "SubscriptionPlan",
                newName: "TermUnit");

            migrationBuilder.AlterColumn<float>(
                name: "Term",
                schema: "am",
                table: "SubscriptionPlan",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "PlanName",
                schema: "am",
                table: "SubscriptionPlan",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BusinessUserCount",
                schema: "am",
                table: "SubscriptionPlan",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerUserCount",
                schema: "am",
                table: "SubscriptionPlan",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                schema: "am",
                table: "SubscriptionPlan",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "OtherFeatures",
                schema: "am",
                table: "SubscriptionPlan",
                maxLength: 4000,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                schema: "am",
                table: "SubscriptionPlan",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "am",
                table: "SubscriptionPlan",
                keyColumn: "ID",
                keyValue: new Guid("8d5e65ca-1b62-4bc3-b0f0-9b3703aabebf"),
                columns: new[] { "AllowUnlimitedTransaction", "BusinessUserCount", "CustomerUserCount", "EndDate", "IdentityNumber", "OtherFeatures", "StartDate", "Term", "TransactionCount" },
                values: new object[] { true, 0, 0, new DateTime(2020, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), "SS#3", "", new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 15f, 0 });

            migrationBuilder.UpdateData(
                schema: "am",
                table: "SubscriptionPlan",
                keyColumn: "ID",
                keyValue: new Guid("b37eb8b9-11bf-402e-9048-f9d0a43a426b"),
                columns: new[] { "AllowUnlimitedTransaction", "BusinessUserCount", "CustomerUserCount", "EndDate", "OtherFeatures", "StartDate", "Term", "TransactionCount" },
                values: new object[] { true, 0, 0, new DateTime(2020, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), "", new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 15f, 0 });

            migrationBuilder.UpdateData(
                schema: "am",
                table: "SubscriptionPlan",
                keyColumn: "ID",
                keyValue: new Guid("d390a264-f0db-49ce-9415-daee1ed4b445"),
                columns: new[] { "AllowUnlimitedTransaction", "BusinessUserCount", "CustomerUserCount", "EndDate", "OtherFeatures", "StartDate", "Term", "TransactionCount" },
                values: new object[] { true, 0, 0, new DateTime(2020, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), "", new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), 15f, 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusinessUserCount",
                schema: "am",
                table: "SubscriptionPlan");

            migrationBuilder.DropColumn(
                name: "CustomerUserCount",
                schema: "am",
                table: "SubscriptionPlan");

            migrationBuilder.DropColumn(
                name: "EndDate",
                schema: "am",
                table: "SubscriptionPlan");

            migrationBuilder.DropColumn(
                name: "OtherFeatures",
                schema: "am",
                table: "SubscriptionPlan");

            migrationBuilder.DropColumn(
                name: "StartDate",
                schema: "am",
                table: "SubscriptionPlan");

            migrationBuilder.RenameColumn(
                name: "JoinedOn",
                schema: "am",
                table: "Tenant",
                newName: "joinedOn");

            migrationBuilder.RenameColumn(
                name: "TransactionCount",
                schema: "am",
                table: "SubscriptionPlan",
                newName: "TrialPeriodInDays");

            migrationBuilder.RenameColumn(
                name: "TermUnit",
                schema: "am",
                table: "SubscriptionPlan",
                newName: "PlanSchedule");

            migrationBuilder.RenameColumn(
                name: "AllowUnlimitedTransaction",
                schema: "am",
                table: "SubscriptionPlan",
                newName: "System");

            migrationBuilder.AlterColumn<int>(
                name: "Term",
                schema: "am",
                table: "SubscriptionPlan",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<string>(
                name: "PlanName",
                schema: "am",
                table: "SubscriptionPlan",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AddColumn<double>(
                name: "AdditionalPerUserPrice",
                schema: "am",
                table: "SubscriptionPlan",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "AlertFrequency",
                schema: "am",
                table: "SubscriptionPlan",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "AllowCustomization",
                schema: "am",
                table: "SubscriptionPlan",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AutoRenewal",
                schema: "am",
                table: "SubscriptionPlan",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "FreeUserLicenseCount",
                schema: "am",
                table: "SubscriptionPlan",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GracePeriodInDays",
                schema: "am",
                table: "SubscriptionPlan",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfUsers",
                schema: "am",
                table: "SubscriptionPlan",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                schema: "am",
                table: "SubscriptionPlan",
                keyColumn: "ID",
                keyValue: new Guid("8d5e65ca-1b62-4bc3-b0f0-9b3703aabebf"),
                columns: new[] { "AlertFrequency", "AllowCustomization", "AutoRenewal", "FreeUserLicenseCount", "GracePeriodInDays", "IdentityNumber", "NumberOfUsers", "System", "Term", "TrialPeriodInDays" },
                values: new object[] { 1, true, true, 1, 58, "SS#6", 1, false, 15, 30 });

            migrationBuilder.UpdateData(
                schema: "am",
                table: "SubscriptionPlan",
                keyColumn: "ID",
                keyValue: new Guid("b37eb8b9-11bf-402e-9048-f9d0a43a426b"),
                columns: new[] { "AlertFrequency", "AllowCustomization", "AutoRenewal", "FreeUserLicenseCount", "GracePeriodInDays", "NumberOfUsers", "System", "Term", "TrialPeriodInDays" },
                values: new object[] { 1, true, true, 1, 58, 1, false, 15, 30 });

            migrationBuilder.UpdateData(
                schema: "am",
                table: "SubscriptionPlan",
                keyColumn: "ID",
                keyValue: new Guid("d390a264-f0db-49ce-9415-daee1ed4b445"),
                columns: new[] { "AlertFrequency", "AllowCustomization", "AutoRenewal", "FreeUserLicenseCount", "GracePeriodInDays", "NumberOfUsers", "System", "Term", "TrialPeriodInDays" },
                values: new object[] { 1, true, true, 1, 58, 1, false, 15, 30 });

            migrationBuilder.InsertData(
                schema: "am",
                table: "SubscriptionPlan",
                columns: new[] { "ID", "Active", "AdditionalPerUserPrice", "AlertFrequency", "AllowCustomization", "AppId", "AutoRenewal", "CreatedBy", "CreatedOn", "Deleted", "FreeUserLicenseCount", "GracePeriodInDays", "IdentityNumber", "NumberOfUsers", "PaymentCycle", "PlanName", "PlanSchedule", "PriceInDollar", "System", "TenantId", "Term", "TrialPeriodInDays", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { new Guid("98276e8b-78eb-4b87-bfff-1d20d82b446f"), true, 0.0, 1, true, new Guid("2b031b59-5276-589c-9d75-2a7ae1c799c8"), true, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, 1, 58, "SS#4", 1, 1, "Trial Plan", 1, 0m, false, new Guid("00000000-0000-0000-0000-000000000000"), 15, 30, new Guid("54b675f2-afdf-400f-a4fe-335ca6adbc40"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) },
                    { new Guid("29f50252-433f-4de9-8a4b-7a2360b4ee44"), true, 0.0, 2, true, new Guid("2b031b59-5276-589c-9d75-2a7ae1c799c8"), true, new Guid("dab7edb0-cd13-4089-a96e-c52d76db4ba8"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc), false, 1, 58, "SS#5", 1, 1, "Executive Plan", 1, 0m, false, new Guid("00000000-0000-0000-0000-000000000000"), 90, 30, new Guid("49a36f27-c493-4f1e-94b7-6f9c660bc3ec"), new DateTime(2019, 2, 4, 9, 40, 36, 503, DateTimeKind.Utc) }
                });
        }
    }
}
