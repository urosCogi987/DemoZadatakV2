﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZadatakV2.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class UserBlocking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "User",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "User");
        }
    }
}
