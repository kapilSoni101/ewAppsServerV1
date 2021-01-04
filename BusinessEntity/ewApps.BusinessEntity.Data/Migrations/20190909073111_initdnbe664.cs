using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.BusinessEntity.Data.Migrations
{
    public partial class initdnbe664 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ERPContractKey",
                schema: "be",
                table: "BADeliveryItem",
                newName: "ERPConnectorKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ERPConnectorKey",
                schema: "be",
                table: "BADeliveryItem",
                newName: "ERPContractKey");
        }
    }
}
