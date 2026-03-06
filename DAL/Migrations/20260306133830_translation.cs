using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class translation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParkingArea",
                table: "parkingSpots");

            migrationBuilder.DropColumn(
                name: "ParkingFloor",
                table: "parkingSpots");

            migrationBuilder.DropColumn(
                name: "SpotNumber",
                table: "parkingSpots");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "parkingSpots",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "translations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpotNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParkingFloor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParkingArea = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParkingSpotId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_translations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_translations_parkingSpots_ParkingSpotId",
                        column: x => x.ParkingSpotId,
                        principalTable: "parkingSpots",
                        principalColumn: "ParkingSpotID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_translations_ParkingSpotId",
                table: "translations",
                column: "ParkingSpotId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "translations");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "parkingSpots");

            migrationBuilder.AddColumn<string>(
                name: "ParkingArea",
                table: "parkingSpots",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParkingFloor",
                table: "parkingSpots",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SpotNumber",
                table: "parkingSpots",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
