using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZadatakV2.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class UserVerification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEmailVerified",
                table: "User",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "VerificationToken",
                table: "User",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEmailVerified",
                table: "User");

            migrationBuilder.DropColumn(
                name: "VerificationToken",
                table: "User");
        }
    }
}
