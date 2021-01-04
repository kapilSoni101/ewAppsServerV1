using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.BusinessEntity.Data.Migrations
{
    public partial class initasnentity127 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PlannedQuantity",
                schema: "be",
                table: "BAContractItem",
                type: "decimal(18, 5)",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PlannedQuantity",
                schema: "be",
                table: "BAContractItem",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 5)");
        }
    }
}
