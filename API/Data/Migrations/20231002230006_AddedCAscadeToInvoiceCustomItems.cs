using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedCAscadeToInvoiceCustomItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomItems_Invoices_InvoiceId",
                table: "CustomItems");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomItems_Invoices_InvoiceId",
                table: "CustomItems",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomItems_Invoices_InvoiceId",
                table: "CustomItems");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomItems_Invoices_InvoiceId",
                table: "CustomItems",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id");
        }
    }
}
