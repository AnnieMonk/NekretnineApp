using Microsoft.EntityFrameworkCore.Migrations;

namespace Nekretnine.Data.Migrations
{
    public partial class ispravkaKlaseNotifikacija : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Korisnici_Notifikacije_NotifikacijaID",
                table: "Korisnici");

            migrationBuilder.DropIndex(
                name: "IX_Korisnici_NotifikacijaID",
                table: "Korisnici");

            migrationBuilder.DropColumn(
                name: "NotifikacijaID",
                table: "Korisnici");

            migrationBuilder.AddColumn<int>(
                name: "KorisnikID",
                table: "Notifikacije",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Notifikacije_KorisnikID",
                table: "Notifikacije",
                column: "KorisnikID");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifikacije_Korisnici_KorisnikID",
                table: "Notifikacije",
                column: "KorisnikID",
                principalTable: "Korisnici",
                principalColumn: "KorisnikID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifikacije_Korisnici_KorisnikID",
                table: "Notifikacije");

            migrationBuilder.DropIndex(
                name: "IX_Notifikacije_KorisnikID",
                table: "Notifikacije");

            migrationBuilder.DropColumn(
                name: "KorisnikID",
                table: "Notifikacije");

            migrationBuilder.AddColumn<int>(
                name: "NotifikacijaID",
                table: "Korisnici",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Korisnici_NotifikacijaID",
                table: "Korisnici",
                column: "NotifikacijaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Korisnici_Notifikacije_NotifikacijaID",
                table: "Korisnici",
                column: "NotifikacijaID",
                principalTable: "Notifikacije",
                principalColumn: "NotifikacijaID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
