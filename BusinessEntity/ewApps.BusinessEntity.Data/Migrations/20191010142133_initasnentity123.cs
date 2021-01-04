using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ewApps.BusinessEntity.Data.Migrations
{
    public partial class initasnentity123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CummlativeQuantity",
                schema: "be",
                table: "BAContractItem",
                newName: "CumulativeQuantity");

            migrationBuilder.RenameColumn(
                name: "CummlativeCommittedQuantity",
                schema: "be",
                table: "BAContractItem",
                newName: "CumulativeCommittedQuantity");

            migrationBuilder.RenameColumn(
                name: "CummlativeCommittedAmount",
                schema: "be",
                table: "BAContractItem",
                newName: "CumulativeCommittedAmount");

            migrationBuilder.RenameColumn(
                name: "CummlativeAmountLC",
                schema: "be",
                table: "BAContractItem",
                newName: "CumulativeAmountLC");

           

            migrationBuilder.AddColumn<string>(
                name: "ERPContractKey",
                schema: "be",
                table: "BAContractItem",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ERPItemKey",
                schema: "be",
                table: "BAContractItem",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ItemId",
                schema: "be",
                table: "BAContractItem",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ERPContractKey",
                schema: "be",
                table: "BAContract",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropColumn(
                name: "ERPContractKey",
                schema: "be",
                table: "BAContractItem");

            migrationBuilder.DropColumn(
                name: "ERPItemKey",
                schema: "be",
                table: "BAContractItem");

            migrationBuilder.DropColumn(
                name: "ItemId",
                schema: "be",
                table: "BAContractItem");

            migrationBuilder.DropColumn(
                name: "ERPContractKey",
                schema: "be",
                table: "BAContract");

            migrationBuilder.RenameColumn(
                name: "CumulativeQuantity",
                schema: "be",
                table: "BAContractItem",
                newName: "CummlativeQuantity");

            migrationBuilder.RenameColumn(
                name: "CumulativeCommittedQuantity",
                schema: "be",
                table: "BAContractItem",
                newName: "CummlativeCommittedQuantity");

            migrationBuilder.RenameColumn(
                name: "CumulativeCommittedAmount",
                schema: "be",
                table: "BAContractItem",
                newName: "CummlativeCommittedAmount");

            migrationBuilder.RenameColumn(
                name: "CumulativeAmountLC",
                schema: "be",
                table: "BAContractItem",
                newName: "CummlativeAmountLC");
        }
    }
}
