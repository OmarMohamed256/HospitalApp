using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedDosesToInvoiceMedicine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DosageAmount",
                table: "InvoiceMedicine",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Duration",
                table: "InvoiceMedicine",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Frequency",
                table: "InvoiceMedicine",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "InvoiceMedicine",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DosageAmount",
                table: "InvoiceMedicine");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "InvoiceMedicine");

            migrationBuilder.DropColumn(
                name: "Frequency",
                table: "InvoiceMedicine");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "InvoiceMedicine");
        }
    }
}
