using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedClinics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clinics_AspNetUsers_DoctorId",
                table: "Clinics");

            migrationBuilder.DropForeignKey(
                name: "FK_Clinics_Specialities_ClinicSpecialityId",
                table: "Clinics");

            migrationBuilder.DropIndex(
                name: "IX_Clinics_ClinicSpecialityId",
                table: "Clinics");

            migrationBuilder.DropIndex(
                name: "IX_Clinics_DoctorId",
                table: "Clinics");

            migrationBuilder.DropColumn(
                name: "ClinicSpecialityId",
                table: "Clinics");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "Clinics",
                newName: "SpecialityId");

            migrationBuilder.CreateTable(
                name: "ClinicDoctor",
                columns: table => new
                {
                    ClinicId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicDoctor", x => new { x.ClinicId, x.DoctorId });
                    table.ForeignKey(
                        name: "FK_ClinicDoctor_AspNetUsers_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClinicDoctor_Clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clinics_SpecialityId",
                table: "Clinics",
                column: "SpecialityId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicDoctor_DoctorId",
                table: "ClinicDoctor",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clinics_Specialities_SpecialityId",
                table: "Clinics",
                column: "SpecialityId",
                principalTable: "Specialities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clinics_Specialities_SpecialityId",
                table: "Clinics");

            migrationBuilder.DropTable(
                name: "ClinicDoctor");

            migrationBuilder.DropIndex(
                name: "IX_Clinics_SpecialityId",
                table: "Clinics");

            migrationBuilder.RenameColumn(
                name: "SpecialityId",
                table: "Clinics",
                newName: "DoctorId");

            migrationBuilder.AddColumn<int>(
                name: "ClinicSpecialityId",
                table: "Clinics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Clinics_ClinicSpecialityId",
                table: "Clinics",
                column: "ClinicSpecialityId");

            migrationBuilder.CreateIndex(
                name: "IX_Clinics_DoctorId",
                table: "Clinics",
                column: "DoctorId",
                unique: true,
                filter: "[DoctorId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Clinics_AspNetUsers_DoctorId",
                table: "Clinics",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clinics_Specialities_ClinicSpecialityId",
                table: "Clinics",
                column: "ClinicSpecialityId",
                principalTable: "Specialities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
