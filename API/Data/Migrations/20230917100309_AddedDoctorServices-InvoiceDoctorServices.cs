using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedDoctorServicesInvoiceDoctorServices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceServiceJoin");

            migrationBuilder.DropColumn(
                name: "DoctorPercentage",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "HospitalPercentage",
                table: "Services");

            migrationBuilder.CreateTable(
                name: "DoctorServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    DoctorPercentage = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    HospitalPercentage = table.Column<decimal>(type: "decimal(18,3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorServices_AspNetUsers_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceDoctorServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    DoctorServiceId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceDoctorServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceDoctorServices_DoctorServices_DoctorServiceId",
                        column: x => x.DoctorServiceId,
                        principalTable: "DoctorServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceDoctorServices_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorServices_DoctorId",
                table: "DoctorServices",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorServices_ServiceId",
                table: "DoctorServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDoctorServices_DoctorServiceId",
                table: "InvoiceDoctorServices",
                column: "DoctorServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDoctorServices_InvoiceId",
                table: "InvoiceDoctorServices",
                column: "InvoiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceDoctorServices");

            migrationBuilder.DropTable(
                name: "DoctorServices");

            migrationBuilder.AddColumn<decimal>(
                name: "DoctorPercentage",
                table: "Services",
                type: "decimal(18,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "HospitalPercentage",
                table: "Services",
                type: "decimal(18,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "InvoiceServiceJoin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
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
                name: "IX_InvoiceServiceJoin_InvoiceId",
                table: "InvoiceServiceJoin",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceServiceJoin_ServiceId",
                table: "InvoiceServiceJoin",
                column: "ServiceId");
        }
    }
}
