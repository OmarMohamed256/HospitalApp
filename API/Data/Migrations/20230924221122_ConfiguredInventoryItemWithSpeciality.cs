using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ConfiguredInventoryItemWithSpeciality : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItems_Specialities_ServiceSpecialityId",
                table: "InventoryItems");

            migrationBuilder.DropIndex(
                name: "IX_InventoryItems_ServiceSpecialityId",
                table: "InventoryItems");

            migrationBuilder.DropColumn(
                name: "ServiceSpecialityId",
                table: "InventoryItems");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_InventoryItemSpecialityId",
                table: "InventoryItems",
                column: "InventoryItemSpecialityId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryItems_Specialities_InventoryItemSpecialityId",
                table: "InventoryItems",
                column: "InventoryItemSpecialityId",
                principalTable: "Specialities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItems_Specialities_InventoryItemSpecialityId",
                table: "InventoryItems");

            migrationBuilder.DropIndex(
                name: "IX_InventoryItems_InventoryItemSpecialityId",
                table: "InventoryItems");

            migrationBuilder.AddColumn<int>(
                name: "ServiceSpecialityId",
                table: "InventoryItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_ServiceSpecialityId",
                table: "InventoryItems",
                column: "ServiceSpecialityId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryItems_Specialities_ServiceSpecialityId",
                table: "InventoryItems",
                column: "ServiceSpecialityId",
                principalTable: "Specialities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
