using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedServiceSpecialityRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceSpecialityId",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Services_ServiceSpecialityId",
                table: "Services",
                column: "ServiceSpecialityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Specialities_ServiceSpecialityId",
                table: "Services",
                column: "ServiceSpecialityId",
                principalTable: "Specialities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Specialities_ServiceSpecialityId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_ServiceSpecialityId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ServiceSpecialityId",
                table: "Services");
        }
    }
}
