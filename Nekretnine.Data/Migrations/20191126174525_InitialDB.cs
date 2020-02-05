using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Nekretnine.Data.Migrations
{
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drzave",
                columns: table => new
                {
                    DrzavaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drzave", x => x.DrzavaID);
                });

            migrationBuilder.CreateTable(
                name: "Kategorije",
                columns: table => new
                {
                    KategorijaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KategorijaNaziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorije", x => x.KategorijaID);
                });

            migrationBuilder.CreateTable(
                name: "VrsteGrijanja",
                columns: table => new
                {
                    VrstaGrijanjaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VrsteGrijanja", x => x.VrstaGrijanjaID);
                });

            migrationBuilder.CreateTable(
                name: "VrsteOglasa",
                columns: table => new
                {
                    VrstaOglasaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VrsteOglasa", x => x.VrstaOglasaID);
                });

            migrationBuilder.CreateTable(
                name: "Gradovi",
                columns: table => new
                {
                    GradID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    DrzavaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gradovi", x => x.GradID);
                    table.ForeignKey(
                        name: "FK_Gradovi_Drzave_DrzavaID",
                        column: x => x.DrzavaID,
                        principalTable: "Drzave",
                        principalColumn: "DrzavaID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Karakteristike",
                columns: table => new
                {
                    KarakteristikeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Voda = table.Column<bool>(nullable: false),
                    Struja = table.Column<bool>(nullable: false),
                    NedavnoAdaptiran = table.Column<bool>(nullable: false),
                    Klima = table.Column<bool>(nullable: false),
                    Internet = table.Column<bool>(nullable: false),
                    VideoNadzor = table.Column<bool>(nullable: false),
                    Kablovska = table.Column<bool>(nullable: false),
                    Kanalizacija = table.Column<bool>(nullable: false),
                    Lift = table.Column<bool>(nullable: false),
                    Parking = table.Column<bool>(nullable: false),
                    Namjesten = table.Column<bool>(nullable: false),
                    Plin = table.Column<bool>(nullable: false),
                    Ostava = table.Column<bool>(nullable: false),
                    Balkon = table.Column<bool>(nullable: false),
                    Garaza = table.Column<bool>(nullable: false),
                    vrstaGrijanjaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Karakteristike", x => x.KarakteristikeID);
                    table.ForeignKey(
                        name: "FK_Karakteristike_VrsteGrijanja_vrstaGrijanjaID",
                        column: x => x.vrstaGrijanjaID,
                        principalTable: "VrsteGrijanja",
                        principalColumn: "VrstaGrijanjaID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    KorisnikID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    KorisnickoIme = table.Column<string>(nullable: true),
                    Lozinka = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Telefon = table.Column<string>(nullable: true),
                    GradID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.KorisnikID);
                    table.ForeignKey(
                        name: "FK_Korisnici_Gradovi_GradID",
                        column: x => x.GradID,
                        principalTable: "Gradovi",
                        principalColumn: "GradID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Lokacije",
                columns: table => new
                {
                    LokacijaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    Adresa = table.Column<string>(nullable: true),
                    GradID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lokacije", x => x.LokacijaID);
                    table.ForeignKey(
                        name: "FK_Lokacije_Gradovi_GradID",
                        column: x => x.GradID,
                        principalTable: "Gradovi",
                        principalColumn: "GradID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Uposlenici",
                columns: table => new
                {
                    UposlenikID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumZaposlenja = table.Column<DateTime>(nullable: false),
                    Zvanje = table.Column<string>(nullable: true),
                    Opis = table.Column<string>(nullable: true),
                    RatingStars = table.Column<double>(nullable: false),
                    KorisnikID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uposlenici", x => x.UposlenikID);
                    table.ForeignKey(
                        name: "FK_Uposlenici_Korisnici_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Nekretnine",
                columns: table => new
                {
                    NekretninaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    Opis = table.Column<string>(nullable: true),
                    Kvadratura = table.Column<double>(nullable: false),
                    Cijena = table.Column<double>(nullable: false),
                    BrojSoba = table.Column<int>(nullable: false),
                    KategorijaID = table.Column<int>(nullable: false),
                    LokacijaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nekretnine", x => x.NekretninaID);
                    table.ForeignKey(
                        name: "FK_Nekretnine_Kategorije_KategorijaID",
                        column: x => x.KategorijaID,
                        principalTable: "Kategorije",
                        principalColumn: "KategorijaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Nekretnine_Lokacije_LokacijaID",
                        column: x => x.LokacijaID,
                        principalTable: "Lokacije",
                        principalColumn: "LokacijaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NekretninaKarks",
                columns: table => new
                {
                    NekretninaKarakteristikeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KarakteristikeID = table.Column<int>(nullable: false),
                    NekretninaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NekretninaKarks", x => x.NekretninaKarakteristikeID);
                    table.ForeignKey(
                        name: "FK_NekretninaKarks_Karakteristike_KarakteristikeID",
                        column: x => x.KarakteristikeID,
                        principalTable: "Karakteristike",
                        principalColumn: "KarakteristikeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NekretninaKarks_Nekretnine_NekretninaID",
                        column: x => x.NekretninaID,
                        principalTable: "Nekretnine",
                        principalColumn: "NekretninaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Oglas",
                columns: table => new
                {
                    OglasID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumVrijemeObjave = table.Column<DateTime>(nullable: false),
                    Aktivan = table.Column<bool>(nullable: false),
                    vrstaOglasaID = table.Column<int>(nullable: false),
                    UposlenikID = table.Column<int>(nullable: false),
                    NekretninaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oglas", x => x.OglasID);
                    table.ForeignKey(
                        name: "FK_Oglas_Nekretnine_NekretninaID",
                        column: x => x.NekretninaID,
                        principalTable: "Nekretnine",
                        principalColumn: "NekretninaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Oglas_Uposlenici_UposlenikID",
                        column: x => x.UposlenikID,
                        principalTable: "Uposlenici",
                        principalColumn: "UposlenikID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Oglas_VrsteOglasa_vrstaOglasaID",
                        column: x => x.vrstaOglasaID,
                        principalTable: "VrsteOglasa",
                        principalColumn: "VrstaOglasaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gradovi_DrzavaID",
                table: "Gradovi",
                column: "DrzavaID");

            migrationBuilder.CreateIndex(
                name: "IX_Karakteristike_vrstaGrijanjaID",
                table: "Karakteristike",
                column: "vrstaGrijanjaID");

            migrationBuilder.CreateIndex(
                name: "IX_Korisnici_GradID",
                table: "Korisnici",
                column: "GradID");

            migrationBuilder.CreateIndex(
                name: "IX_Lokacije_GradID",
                table: "Lokacije",
                column: "GradID");

            migrationBuilder.CreateIndex(
                name: "IX_NekretninaKarks_KarakteristikeID",
                table: "NekretninaKarks",
                column: "KarakteristikeID");

            migrationBuilder.CreateIndex(
                name: "IX_NekretninaKarks_NekretninaID",
                table: "NekretninaKarks",
                column: "NekretninaID");

            migrationBuilder.CreateIndex(
                name: "IX_Nekretnine_KategorijaID",
                table: "Nekretnine",
                column: "KategorijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Nekretnine_LokacijaID",
                table: "Nekretnine",
                column: "LokacijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Oglas_NekretninaID",
                table: "Oglas",
                column: "NekretninaID");

            migrationBuilder.CreateIndex(
                name: "IX_Oglas_UposlenikID",
                table: "Oglas",
                column: "UposlenikID");

            migrationBuilder.CreateIndex(
                name: "IX_Oglas_vrstaOglasaID",
                table: "Oglas",
                column: "vrstaOglasaID");

            migrationBuilder.CreateIndex(
                name: "IX_Uposlenici_KorisnikID",
                table: "Uposlenici",
                column: "KorisnikID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NekretninaKarks");

            migrationBuilder.DropTable(
                name: "Oglas");

            migrationBuilder.DropTable(
                name: "Karakteristike");

            migrationBuilder.DropTable(
                name: "Nekretnine");

            migrationBuilder.DropTable(
                name: "Uposlenici");

            migrationBuilder.DropTable(
                name: "VrsteOglasa");

            migrationBuilder.DropTable(
                name: "VrsteGrijanja");

            migrationBuilder.DropTable(
                name: "Kategorije");

            migrationBuilder.DropTable(
                name: "Lokacije");

            migrationBuilder.DropTable(
                name: "Korisnici");

            migrationBuilder.DropTable(
                name: "Gradovi");

            migrationBuilder.DropTable(
                name: "Drzave");
        }
    }
}
