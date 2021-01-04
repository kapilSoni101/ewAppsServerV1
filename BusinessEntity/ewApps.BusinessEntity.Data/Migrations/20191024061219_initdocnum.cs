using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.BusinessEntity.Data.Migrations
{
    public partial class initdocnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ERPDocNum",
                schema: "be",
                table: "BASalesQuotation",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ERPDocNum",
                schema: "be",
                table: "BASalesOrder",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ERPDocNum",
                schema: "be",
                table: "BADelivery",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ERPDocNum",
                schema: "be",
                table: "BAContract",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ERPDocNum",
                schema: "be",
                table: "BAASN",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ERPDocNum",
                schema: "be",
                table: "BAARInvoice",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ERPDocNum",
                schema: "be",
                table: "BASalesQuotation");

            migrationBuilder.DropColumn(
                name: "ERPDocNum",
                schema: "be",
                table: "BASalesOrder");

            migrationBuilder.DropColumn(
                name: "ERPDocNum",
                schema: "be",
                table: "BADelivery");

            migrationBuilder.DropColumn(
                name: "ERPDocNum",
                schema: "be",
                table: "BAContract");

            migrationBuilder.DropColumn(
                name: "ERPDocNum",
                schema: "be",
                table: "BAASN");

            migrationBuilder.DropColumn(
                name: "ERPDocNum",
                schema: "be",
                table: "BAARInvoice");
        }
    }
}
