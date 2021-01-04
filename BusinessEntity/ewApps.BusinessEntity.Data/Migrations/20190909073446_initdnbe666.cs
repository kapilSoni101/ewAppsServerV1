using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.BusinessEntity.Data.Migrations
{
    public partial class initdnbe666 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ERPDeliveryItemKey",
                schema: "be",
                table: "BADeliveryItem",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "ERPConnectorKey",
                schema: "be",
                table: "BADeliveryItem",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ERPDeliveryItemKey",
                schema: "be",
                table: "BADeliveryItem",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "ERPConnectorKey",
                schema: "be",
                table: "BADeliveryItem",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);
        }
    }
}
