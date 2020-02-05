using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nekretnine.Data.Migrations
{
    public partial class KorigovaneUplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DogDatumUplate",
                table: "Uplate");

            migrationBuilder.DropColumn(
                name: "RealDatumUplate",
                table: "Uplate");

            migrationBuilder.AddColumn<DateTime>(
                name: "DatumUplate",
                table: "Uplate",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DatumUplate",
                table: "Uplate");

            migrationBuilder.AddColumn<DateTime>(
                name: "DogDatumUplate",
                table: "Uplate",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RealDatumUplate",
                table: "Uplate",
                type: "datetime2",
                nullable: true);
        }
    }
}
