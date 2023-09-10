using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedSpecialityCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Specialities_SpecialityId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Specialities_DoctorSpecialityId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Specialities_SpecialityId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SpecialityId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_SpecialityId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "SpecialityId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SpecialityId",
                table: "Appointments");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorSpecialityId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Specialities_DoctorSpecialityId",
                table: "AspNetUsers",
                column: "DoctorSpecialityId",
                principalTable: "Specialities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Specialities_DoctorSpecialityId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorSpecialityId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "SpecialityId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpecialityId",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SpecialityId",
                table: "AspNetUsers",
                column: "SpecialityId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_SpecialityId",
                table: "Appointments",
                column: "SpecialityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Specialities_SpecialityId",
                table: "Appointments",
                column: "SpecialityId",
                principalTable: "Specialities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Specialities_DoctorSpecialityId",
                table: "AspNetUsers",
                column: "DoctorSpecialityId",
                principalTable: "Specialities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Specialities_SpecialityId",
                table: "AspNetUsers",
                column: "SpecialityId",
                principalTable: "Specialities",
                principalColumn: "Id");
        }
    }
}
