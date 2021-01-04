using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.BusinessEntity.Data.Migrations
{
    public partial class initdnbe667 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ERPDeliveryItemKey",
                schema: "be",
                table: "BADeliveryItem",
                newName: "ERPDeliveryKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ERPDeliveryKey",
                schema: "be",
                table: "BADeliveryItem",
                newName: "ERPDeliveryItemKey");
        }
    }
}
