using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class code : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ResetPasswordExpiryDate",
                table: "AspNetUsers",
                newName: "PaswordResetCodeExpiary");

            migrationBuilder.RenameColumn(
                name: "RequestPasswordReset",
                table: "AspNetUsers",
                newName: "PaswordResetCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaswordResetCodeExpiary",
                table: "AspNetUsers",
                newName: "ResetPasswordExpiryDate");

            migrationBuilder.RenameColumn(
                name: "PaswordResetCode",
                table: "AspNetUsers",
                newName: "RequestPasswordReset");
        }
    }
}
