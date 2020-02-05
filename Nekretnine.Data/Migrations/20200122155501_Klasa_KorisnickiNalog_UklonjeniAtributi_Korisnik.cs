using Microsoft.EntityFrameworkCore.Migrations;

namespace Nekretnine.Data.Migrations
{
    public partial class Klasa_KorisnickiNalog_UklonjeniAtributi_Korisnik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KorisnickoIme",
                table: "Korisnici");

            migrationBuilder.DropColumn(
                name: "Lozinka",
                table: "Korisnici");

            migrationBuilder.AddColumn<int>(
                name: "KorisnickiNalogID",
                table: "Korisnici",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "KorisnickiNalozi",
                columns: table => new
                {
                    KorisnickiNalogID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnickoIme = table.Column<string>(nullable: true),
                    Lozinka = table.Column<string>(nullable: true),
                    ZapamtiMe = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisnickiNalozi", x => x.KorisnickiNalogID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Korisnici_KorisnickiNalogID",
                table: "Korisnici",
                column: "KorisnickiNalogID");

            migrationBuilder.AddForeignKey(
                name: "FK_Korisnici_KorisnickiNalozi_KorisnickiNalogID",
                table: "Korisnici",
                column: "KorisnickiNalogID",
                principalTable: "KorisnickiNalozi",
                principalColumn: "KorisnickiNalogID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Korisnici_KorisnickiNalozi_KorisnickiNalogID",
                table: "Korisnici");

            migrationBuilder.DropTable(
                name: "KorisnickiNalozi");

            migrationBuilder.DropIndex(
                name: "IX_Korisnici_KorisnickiNalogID",
                table: "Korisnici");

            migrationBuilder.DropColumn(
                name: "KorisnickiNalogID",
                table: "Korisnici");

            migrationBuilder.AddColumn<string>(
                name: "KorisnickoIme",
                table: "Korisnici",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lozinka",
                table: "Korisnici",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
