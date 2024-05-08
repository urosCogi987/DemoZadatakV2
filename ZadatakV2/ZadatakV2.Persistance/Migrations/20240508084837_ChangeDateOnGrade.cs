using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZadatakV2.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDateOnGrade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Grade");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedOn",
                table: "Grade",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedOn",
                table: "Grade");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "Grade",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }
    }
}
