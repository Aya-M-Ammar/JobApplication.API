using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobApplication.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class modifay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosedAt",
                table: "Applications");

            migrationBuilder.AddColumn<DateTime>(
                name: "ClosedAt",
                table: "Jobs",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosedAt",
                table: "Jobs");

            migrationBuilder.AddColumn<DateTime>(
                name: "ClosedAt",
                table: "Applications",
                type: "datetime2",
                nullable: true);
        }
    }
}
