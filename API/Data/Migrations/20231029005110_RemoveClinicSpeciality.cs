using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveClinicSpeciality : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clinics_Specialities_SpecialityId",
                table: "Clinics");

            migrationBuilder.DropIndex(
                name: "IX_Clinics_SpecialityId",
                table: "Clinics");

            migrationBuilder.DropColumn(
                name: "SpecialityId",
                table: "Clinics");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpecialityId",
                table: "Clinics",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clinics_SpecialityId",
                table: "Clinics",
                column: "SpecialityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clinics_Specialities_SpecialityId",
                table: "Clinics",
                column: "SpecialityId",
                principalTable: "Specialities",
                principalColumn: "Id");
        }
    }
}
