using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedSellOrderConsumesSupplyOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SupplyOrderId",
                table: "InvoiceDoctorServiceSupplyOrders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "SellOrderConsumesSupplyOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellOrderId = table.Column<int>(type: "int", nullable: true),
                    SupplyOrderId = table.Column<int>(type: "int", nullable: true),
                    QuantityUsed = table.Column<int>(type: "int", nullable: false),
                    ItemPrice = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellOrderConsumesSupplyOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SellOrderConsumesSupplyOrders_SellOrders_SellOrderId",
                        column: x => x.SellOrderId,
                        principalTable: "SellOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_SellOrderConsumesSupplyOrders_SupplyOrders_SupplyOrderId",
                        column: x => x.SupplyOrderId,
                        principalTable: "SupplyOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SellOrderConsumesSupplyOrders_SellOrderId",
                table: "SellOrderConsumesSupplyOrders",
                column: "SellOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SellOrderConsumesSupplyOrders_SupplyOrderId",
                table: "SellOrderConsumesSupplyOrders",
                column: "SupplyOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SellOrderConsumesSupplyOrders");

            migrationBuilder.AlterColumn<int>(
                name: "SupplyOrderId",
                table: "InvoiceDoctorServiceSupplyOrders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
