using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.AppPortal.Data.Migrations
{
    public partial class initdbappportal678 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PublisherAppService",
                schema: "ap");

           
            
            

            migrationBuilder.CreateTable(
                name: "PubBusinessSubsPlan",
                schema: "ap",
                columns: table => new
                {
                    TenantId = table.Column<Guid>(nullable: false),
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    IdentityNumber = table.Column<string>(maxLength: 100, nullable: false),
                    PlanName = table.Column<string>(maxLength: 100, nullable: false),
                    AppId = table.Column<Guid>(nullable: false),
                    SubscriptionPlanId = table.Column<Guid>(nullable: false),
                    Term = table.Column<int>(nullable: false),
                    TermUnit = table.Column<int>(nullable: false),
                    PriceInDollar = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    PaymentCycle = table.Column<int>(nullable: false),
                    BusinessUserCount = table.Column<int>(nullable: true),
                    CustomerUserCount = table.Column<int>(nullable: true),
                    TransactionCount = table.Column<int>(nullable: false),
                    AllowUnlimitedTransaction = table.Column<bool>(nullable: false),
                    OtherFeatures = table.Column<string>(maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PubBusinessSubsPlan", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PubBusinessSubsPlanAppService",
                schema: "ap",
                columns: table => new
                {
                    TenantId = table.Column<Guid>(nullable: false),
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    AppId = table.Column<Guid>(nullable: false),
                    AppServiceId = table.Column<Guid>(nullable: false),
                    AppServiceAttributeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PubBusinessSubsPlanAppService", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PubBusinessSubsPlan",
                schema: "ap");

            migrationBuilder.DropTable(
                name: "PubBusinessSubsPlanAppService",
                schema: "ap");

          
            

            migrationBuilder.CreateTable(
                name: "PublisherAppService",
                schema: "ap",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    AppId = table.Column<Guid>(nullable: false),
                    AppServiceAttributeId = table.Column<Guid>(nullable: false),
                    AppServiceId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublisherAppService", x => x.ID);
                });
        }
    }
}
