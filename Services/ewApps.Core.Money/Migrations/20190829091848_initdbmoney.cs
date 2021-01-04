using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.Core.Money.Migrations
{
    public partial class initdbmoney : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentCurrency",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    DocumentAmount = table.Column<decimal>(type: "decimal (18,5)", nullable: false),
                    DocumentCurrencyCode = table.Column<int>(nullable: false),
                    AgentCurrencyCode = table.Column<int>(nullable: false),
                    ApproxConversionRate = table.Column<decimal>(type: "decimal (18,5)", nullable: false),
                    FinalConversionRate = table.Column<decimal>(type: "decimal (18,5)", nullable: false),
                    AgentAmount = table.Column<decimal>(type: "decimal (18,5)", nullable: false),
                    DocumentType = table.Column<string>(maxLength: 100, nullable: true),
                    DocumentId = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    SettlementDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentCurrency", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentCurrency");
        }
    }
}
