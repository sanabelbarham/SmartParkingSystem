using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class removeQR : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QR");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QR",
                columns: table => new
                {
                    QRID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParkingSpotID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QR", x => x.QRID);
                    table.ForeignKey(
                        name: "FK_QR_parkingSpots_ParkingSpotID",
                        column: x => x.ParkingSpotID,
                        principalTable: "parkingSpots",
                        principalColumn: "ParkingSpotID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_QR_ParkingSpotID",
                table: "QR",
                column: "ParkingSpotID",
                unique: true);
        }
    }
}
