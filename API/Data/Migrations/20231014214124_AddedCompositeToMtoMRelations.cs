using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedCompositeToMtoMRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceInventoryItems",
                table: "ServiceInventoryItems");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ServiceInventoryItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceInventoryItems",
                table: "ServiceInventoryItems",
                columns: new[] { "ServiceId", "InventoryItemId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceInventoryItems",
                table: "ServiceInventoryItems");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ServiceInventoryItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceInventoryItems",
                table: "ServiceInventoryItems",
                column: "Id");
        }
    }
}
