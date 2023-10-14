using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedAppointmentMedicine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Diagnoses",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentMedicine",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(type: "int", nullable: false),
                    MedicineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentMedicine", x => new { x.AppointmentId, x.MedicineId });
                    table.ForeignKey(
                        name: "FK_AppointmentMedicine_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointmentMedicine_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentMedicine_MedicineId",
                table: "AppointmentMedicine",
                column: "MedicineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentMedicine");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropColumn(
                name: "Diagnoses",
                table: "Appointments");
        }
    }
}
