using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.BusinessEntity.Data.Migrations
{
    public partial class initdnbe659 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerID",
                schema: "be",
                table: "BADelivery",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "ERPCustomerKey",
                schema: "be",
                table: "BADelivery",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ERPCustomerKey",
                schema: "be",
                table: "BADelivery");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerID",
                schema: "be",
                table: "BADelivery",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(Guid),
                oldMaxLength: 100);
        }
    }
}
