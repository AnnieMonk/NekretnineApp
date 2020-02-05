using Microsoft.EntityFrameworkCore.Migrations;

namespace Nekretnine.Data.Migrations
{
    public partial class doradaNotifikacija : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotifikacijaID",
                table: "Korisnici");

            migrationBuilder.AddColumn<bool>(
                name: "Vidjeno",
                table: "Notifikacije",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Vidjeno",
                table: "Notifikacije");

            migrationBuilder.AddColumn<int>(
                name: "NotifikacijaID",
                table: "Korisnici",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
