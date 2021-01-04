using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.Core.ScheduledJobService.Migrations
{
    public partial class initdbschedulerjob33 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "core");

            migrationBuilder.CreateTable(
                name: "ScheduledJob",
                schema: "core",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    JobName = table.Column<string>(maxLength: 100, nullable: false),
                    ScheduledTime = table.Column<DateTime>(nullable: false),
                    SourceName = table.Column<string>(maxLength: 100, nullable: false),
                    SourceId = table.Column<Guid>(nullable: false),
                    SourceEventName = table.Column<string>(maxLength: 100, nullable: false),
                    SourceEventPayload = table.Column<string>(maxLength: 4000, nullable: true),
                    SourceCallback = table.Column<string>(maxLength: 100, nullable: true),
                    Processed = table.Column<bool>(nullable: false),
                    Status = table.Column<string>(maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledJob", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ScheduledJobLog",
                schema: "core",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ScheduledJobId = table.Column<Guid>(nullable: false),
                    ScheduledTime = table.Column<DateTime>(nullable: false),
                    WorkflowStep = table.Column<string>(maxLength: 100, nullable: true),
                    InProcessQueueTime = table.Column<DateTime>(nullable: false),
                    InProcessQueueStatus = table.Column<string>(maxLength: 100, nullable: true),
                    InProcessQueueReason = table.Column<string>(maxLength: 4000, nullable: true),
                    SourceCallbackTime = table.Column<DateTime>(nullable: true),
                    SourceCallbackStatus = table.Column<string>(maxLength: 100, nullable: true),
                    SourceCallbackReason = table.Column<string>(maxLength: 4000, nullable: false),
                    CompletionTime = table.Column<DateTime>(nullable: true),
                    CompletionStatus = table.Column<string>(maxLength: 100, nullable: true),
                    CompletionReason = table.Column<string>(maxLength: 4000, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledJobLog", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Scheduler",
                schema: "core",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    SchedulerName = table.Column<string>(maxLength: 100, nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    FrequencyType = table.Column<int>(nullable: false),
                    FrequencyValue = table.Column<int>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    LastExecutionTime = table.Column<DateTime>(nullable: true),
                    NextExecutionTime = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scheduler", x => x.ID);
                });

            migrationBuilder.InsertData(
                schema: "core",
                table: "Scheduler",
                columns: new[] { "ID", "Active", "EndDate", "FrequencyType", "FrequencyValue", "LastExecutionTime", "ModifiedOn", "NextExecutionTime", "SchedulerName" },
                values: new object[] { new Guid("72504d45-3128-45f2-aa68-078a3d5eb20f"), true, new DateTime(2029, 9, 4, 10, 2, 38, 62, DateTimeKind.Utc), 1, 1, null, new DateTime(2019, 9, 4, 10, 2, 38, 63, DateTimeKind.Utc), new DateTime(2019, 9, 4, 10, 2, 0, 0, DateTimeKind.Utc), "RecurringPaymentScheduler" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduledJob",
                schema: "core");

            migrationBuilder.DropTable(
                name: "ScheduledJobLog",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Scheduler",
                schema: "core");
        }
    }
}
