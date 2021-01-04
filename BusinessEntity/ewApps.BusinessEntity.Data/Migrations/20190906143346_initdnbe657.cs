using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.BusinessEntity.Data.Migrations
{
    public partial class initdnbe657 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                schema: "be",
                table: "BADeliveryItem",
                type: "decimal (18,5)",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Owner",
                schema: "be",
                table: "BAContract",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Owner",
                schema: "be",
                table: "BAContract");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                schema: "be",
                table: "BADeliveryItem",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal (18,5)");
        }
    }
}
