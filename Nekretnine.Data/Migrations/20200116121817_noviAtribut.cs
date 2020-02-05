using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nekretnine.Data.Migrations
{
    public partial class noviAtribut : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtraColumn",
                table: "Notifikacije");

            migrationBuilder.AddColumn<DateTime>(
                name: "When",
                table: "Notifikacije",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "When",
                table: "Notifikacije");

            migrationBuilder.AddColumn<string>(
                name: "ExtraColumn",
                table: "Notifikacije",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
