using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedInvoiceDoctorServiceOrdersSupplyOrderInventoryItemServiceInventoryItemInvoiceDoctorService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceItems");

            migrationBuilder.CreateTable(
                name: "InventoryItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceSpecialityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryItems_Specialities_ServiceSpecialityId",
                        column: x => x.ServiceSpecialityId,
                        principalTable: "Specialities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceDoctorService",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    DoctorServiceId = table.Column<int>(type: "int", nullable: true),
                    ServiceQuantity = table.Column<int>(type: "int", nullable: false),
                    TotalDisposablesPrice = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    ServiceSoldPrice = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceDoctorService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceDoctorService_DoctorServices_DoctorServiceId",
                        column: x => x.DoctorServiceId,
                        principalTable: "DoctorServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_InvoiceDoctorService_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceInventoryItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    InventoryItemId = table.Column<int>(type: "int", nullable: false),
                    QuantityNeeded = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceInventoryItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceInventoryItems_InventoryItems_InventoryItemId",
                        column: x => x.InventoryItemId,
                        principalTable: "InventoryItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceInventoryItems_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplyOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ItemPrice = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InventoryItemId = table.Column<int>(type: "int", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplyOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplyOrders_InventoryItems_InventoryItemId",
                        column: x => x.InventoryItemId,
                        principalTable: "InventoryItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceDoctorServiceSupplyOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceDoctorServiceId = table.Column<int>(type: "int", nullable: true),
                    SupplyOrderId = table.Column<int>(type: "int", nullable: true),
                    QuantityUsed = table.Column<int>(type: "int", nullable: false),
                    ItemPrice = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceDoctorServiceSupplyOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceDoctorServiceSupplyOrders_InvoiceDoctorService_InvoiceDoctorServiceId",
                        column: x => x.InvoiceDoctorServiceId,
                        principalTable: "InvoiceDoctorService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_InvoiceDoctorServiceSupplyOrders_SupplyOrders_SupplyOrderId",
                        column: x => x.SupplyOrderId,
                        principalTable: "SupplyOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_ServiceSpecialityId",
                table: "InventoryItems",
                column: "ServiceSpecialityId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDoctorService_DoctorServiceId",
                table: "InvoiceDoctorService",
                column: "DoctorServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDoctorService_InvoiceId",
                table: "InvoiceDoctorService",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDoctorServiceSupplyOrders_InvoiceDoctorServiceId",
                table: "InvoiceDoctorServiceSupplyOrders",
                column: "InvoiceDoctorServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDoctorServiceSupplyOrders_SupplyOrderId",
                table: "InvoiceDoctorServiceSupplyOrders",
                column: "SupplyOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceInventoryItems_InventoryItemId",
                table: "ServiceInventoryItems",
                column: "InventoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceInventoryItems_ServiceId",
                table: "ServiceInventoryItems",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplyOrders_InventoryItemId",
                table: "SupplyOrders",
                column: "InventoryItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceDoctorServiceSupplyOrders");

            migrationBuilder.DropTable(
                name: "ServiceInventoryItems");

            migrationBuilder.DropTable(
                name: "InvoiceDoctorService");

            migrationBuilder.DropTable(
                name: "SupplyOrders");

            migrationBuilder.DropTable(
                name: "InventoryItems");

            migrationBuilder.CreateTable(
                name: "InvoiceItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorServiceId = table.Column<int>(type: "int", nullable: true),
                    InvoiceId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServicePrice = table.Column<decimal>(type: "decimal(18,3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_DoctorServices_DoctorServiceId",
                        column: x => x.DoctorServiceId,
                        principalTable: "DoctorServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_DoctorServiceId",
                table: "InvoiceItems",
                column: "DoctorServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_InvoiceId",
                table: "InvoiceItems",
                column: "InvoiceId");
        }
    }
}
