using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenamedInvoiceItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDoctorServices_DoctorServices_DoctorServiceId",
                table: "InvoiceDoctorServices");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDoctorServices_Invoices_InvoiceId",
                table: "InvoiceDoctorServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceDoctorServices",
                table: "InvoiceDoctorServices");

            migrationBuilder.RenameTable(
                name: "InvoiceDoctorServices",
                newName: "InvoiceItems");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceDoctorServices_InvoiceId",
                table: "InvoiceItems",
                newName: "IX_InvoiceItems_InvoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceDoctorServices_DoctorServiceId",
                table: "InvoiceItems",
                newName: "IX_InvoiceItems_DoctorServiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceItems",
                table: "InvoiceItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItems_DoctorServices_DoctorServiceId",
                table: "InvoiceItems",
                column: "DoctorServiceId",
                principalTable: "DoctorServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItems_Invoices_InvoiceId",
                table: "InvoiceItems",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItems_DoctorServices_DoctorServiceId",
                table: "InvoiceItems");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItems_Invoices_InvoiceId",
                table: "InvoiceItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceItems",
                table: "InvoiceItems");

            migrationBuilder.RenameTable(
                name: "InvoiceItems",
                newName: "InvoiceDoctorServices");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceItems_InvoiceId",
                table: "InvoiceDoctorServices",
                newName: "IX_InvoiceDoctorServices_InvoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceItems_DoctorServiceId",
                table: "InvoiceDoctorServices",
                newName: "IX_InvoiceDoctorServices_DoctorServiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceDoctorServices",
                table: "InvoiceDoctorServices",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDoctorServices_DoctorServices_DoctorServiceId",
                table: "InvoiceDoctorServices",
                column: "DoctorServiceId",
                principalTable: "DoctorServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDoctorServices_Invoices_InvoiceId",
                table: "InvoiceDoctorServices",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
