using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedTackingToSupplyOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "SupplyOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdated",
                table: "SupplyOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "SupplyOrders");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                table: "SupplyOrders");
        }
    }
}
