using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.Core.UniqueIdentityGeneratorService.Migrations
{
    public partial class initdbidentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UniqueIdentityGenerator",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    EntityType = table.Column<int>(nullable: false),
                    PrefixString = table.Column<string>(maxLength: 100, nullable: false),
                    IdentityNumber = table.Column<string>(maxLength: 100, nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UniqueIdentityGenerator", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UniqueIdentityGenerator");
        }
    }
}
