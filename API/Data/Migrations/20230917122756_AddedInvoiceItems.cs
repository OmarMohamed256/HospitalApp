using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedInvoiceItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DisposablesPercentage",
                table: "Services",
                newName: "DisposablesPrice");

            migrationBuilder.AddColumn<decimal>(
                name: "ServicePrice",
                table: "InvoiceDoctorServices",
                type: "decimal(18,3)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServicePrice",
                table: "InvoiceDoctorServices");

            migrationBuilder.RenameColumn(
                name: "DisposablesPrice",
                table: "Services",
                newName: "DisposablesPercentage");
        }
    }
}
