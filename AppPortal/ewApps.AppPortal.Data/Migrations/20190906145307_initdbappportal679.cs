using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.AppPortal.Data.Migrations
{
    public partial class initdbappportal679 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PubBusinessSubsPlanId",
                schema: "ap",
                table: "PubBusinessSubsPlanAppService",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PubBusinessSubsPlanId",
                schema: "ap",
                table: "PubBusinessSubsPlanAppService");
        }
    }
}
