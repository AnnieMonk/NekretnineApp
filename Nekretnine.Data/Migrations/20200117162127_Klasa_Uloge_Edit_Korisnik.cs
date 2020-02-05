using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nekretnine.Data.Migrations
{
    public partial class Klasa_Uloge_Edit_Korisnik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UlogaID",
                table: "Korisnici",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Obilasci",
                columns: table => new
                {
                    ObilazakID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumVrijemeStart = table.Column<DateTime>(nullable: false),
                    DatumVrijemeEnd = table.Column<DateTime>(nullable: false),
                    Napomena = table.Column<string>(nullable: true),
                    Otkazano = table.Column<bool>(nullable: false),
                    Zavrseno = table.Column<bool>(nullable: false),
                    KorisnikID = table.Column<int>(nullable: false),
                    UposlenikID = table.Column<int>(nullable: false),
                    LokacijaID = table.Column<int>(nullable: false),
                    NekretninaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obilasci", x => x.ObilazakID);
                    table.ForeignKey(
                        name: "FK_Obilasci_Korisnici_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Obilasci_Lokacije_LokacijaID",
                        column: x => x.LokacijaID,
                        principalTable: "Lokacije",
                        principalColumn: "LokacijaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Obilasci_Nekretnine_NekretninaID",
                        column: x => x.NekretninaID,
                        principalTable: "Nekretnine",
                        principalColumn: "NekretninaID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Obilasci_Uposlenici_UposlenikID",
                        column: x => x.UposlenikID,
                        principalTable: "Uposlenici",
                        principalColumn: "UposlenikID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Uloge",
                columns: table => new
                {
                    UlogaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    Opis = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uloge", x => x.UlogaID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Korisnici_UlogaID",
                table: "Korisnici",
                column: "UlogaID");

            migrationBuilder.CreateIndex(
                name: "IX_Obilasci_KorisnikID",
                table: "Obilasci",
                column: "KorisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Obilasci_LokacijaID",
                table: "Obilasci",
                column: "LokacijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Obilasci_NekretninaID",
                table: "Obilasci",
                column: "NekretninaID");

            migrationBuilder.CreateIndex(
                name: "IX_Obilasci_UposlenikID",
                table: "Obilasci",
                column: "UposlenikID");

            migrationBuilder.AddForeignKey(
                name: "FK_Korisnici_Uloge_UlogaID",
                table: "Korisnici",
                column: "UlogaID",
                principalTable: "Uloge",
                principalColumn: "UlogaID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Korisnici_Uloge_UlogaID",
                table: "Korisnici");

            migrationBuilder.DropTable(
                name: "Obilasci");

            migrationBuilder.DropTable(
                name: "Uloge");

            migrationBuilder.DropIndex(
                name: "IX_Korisnici_UlogaID",
                table: "Korisnici");

            migrationBuilder.DropColumn(
                name: "UlogaID",
                table: "Korisnici");
        }
    }
}
