using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class translationspilling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_translations_parkingSpots_ParkingSpotId",
                table: "translations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_translations",
                table: "translations");

            migrationBuilder.RenameTable(
                name: "translations",
                newName: "Translations");

            migrationBuilder.RenameIndex(
                name: "IX_translations_ParkingSpotId",
                table: "Translations",
                newName: "IX_Translations_ParkingSpotId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Translations",
                table: "Translations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Translations_parkingSpots_ParkingSpotId",
                table: "Translations",
                column: "ParkingSpotId",
                principalTable: "parkingSpots",
                principalColumn: "ParkingSpotID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Translations_parkingSpots_ParkingSpotId",
                table: "Translations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Translations",
                table: "Translations");

            migrationBuilder.RenameTable(
                name: "Translations",
                newName: "translations");

            migrationBuilder.RenameIndex(
                name: "IX_Translations_ParkingSpotId",
                table: "translations",
                newName: "IX_translations_ParkingSpotId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_translations",
                table: "translations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_translations_parkingSpots_ParkingSpotId",
                table: "translations",
                column: "ParkingSpotId",
                principalTable: "parkingSpots",
                principalColumn: "ParkingSpotID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
