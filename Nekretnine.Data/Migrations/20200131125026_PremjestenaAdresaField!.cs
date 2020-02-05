using Microsoft.EntityFrameworkCore.Migrations;

namespace Nekretnine.Data.Migrations
{
    public partial class PremjestenaAdresaField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adresa",
                table: "Lokacije");

            migrationBuilder.AddColumn<string>(
                name: "Adresa",
                table: "Obilasci",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adresa",
                table: "Obilasci");

            migrationBuilder.AddColumn<string>(
                name: "Adresa",
                table: "Lokacije",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
