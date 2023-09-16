using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedInvoiceServiceCustomItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalDue = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    TotalAfterDiscount = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    TotalPaid = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    TotalRemaining = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    FinalizationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppointmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoctorPercentage = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    HospitalPercentage = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    DisposablesPercentage = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    Units = table.Column<int>(type: "int", nullable: true),
                    InvoiceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomItems_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InvoiceServiceJoin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceServiceJoin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceServiceJoin_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceServiceJoin_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_InvoiceId",
                table: "Appointments",
                column: "InvoiceId",
                unique: true,
                filter: "[InvoiceId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CustomItems_InvoiceId",
                table: "CustomItems",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceServiceJoin_InvoiceId",
                table: "InvoiceServiceJoin",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceServiceJoin_ServiceId",
                table: "InvoiceServiceJoin",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Invoices_InvoiceId",
                table: "Appointments",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Invoices_InvoiceId",
                table: "Appointments");

            migrationBuilder.DropTable(
                name: "CustomItems");

            migrationBuilder.DropTable(
                name: "InvoiceServiceJoin");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_InvoiceId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "Appointments");
        }
    }
}
