using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class MadeDoctorNullableInRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_AspNetUsers_DoctorId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_DoctorId",
                table: "Rooms");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "Rooms",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_DoctorId",
                table: "Rooms",
                column: "DoctorId",
                unique: true,
                filter: "[DoctorId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_AspNetUsers_DoctorId",
                table: "Rooms",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_AspNetUsers_DoctorId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_DoctorId",
                table: "Rooms");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_DoctorId",
                table: "Rooms",
                column: "DoctorId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_AspNetUsers_DoctorId",
                table: "Rooms",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
