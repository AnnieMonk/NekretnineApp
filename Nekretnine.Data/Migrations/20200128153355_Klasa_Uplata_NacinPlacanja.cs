using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nekretnine.Data.Migrations
{
    public partial class Klasa_Uplata_NacinPlacanja : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NaciniPlacanja",
                columns: table => new
                {
                    NacinPlacanjaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NaciniPlacanja", x => x.NacinPlacanjaID);
                });

            migrationBuilder.CreateTable(
                name: "Uplate",
                columns: table => new
                {
                    UplataID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DogDatumUplate = table.Column<DateTime>(nullable: false),
                    RealDatumUplate = table.Column<DateTime>(nullable: false),
                    Kasnjenje = table.Column<DateTime>(nullable: false),
                    MjesecnaRata = table.Column<double>(nullable: false),
                    UkupanIznosPDV = table.Column<double>(nullable: false),
                    UkupanIznosBezPDV = table.Column<double>(nullable: false),
                    NacinPlacanjaID = table.Column<int>(nullable: false),
                    KorisnikID = table.Column<int>(nullable: false),
                    UposlenikID = table.Column<int>(nullable: false),
                    UgovorID = table.Column<int>(nullable: false),
                    NekretninaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uplate", x => x.UplataID);
                    table.ForeignKey(
                        name: "FK_Uplate_Korisnici_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Uplate_NaciniPlacanja_NacinPlacanjaID",
                        column: x => x.NacinPlacanjaID,
                        principalTable: "NaciniPlacanja",
                        principalColumn: "NacinPlacanjaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Uplate_Nekretnine_NekretninaID",
                        column: x => x.NekretninaID,
                        principalTable: "Nekretnine",
                        principalColumn: "NekretninaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Uplate_Ugovori_UgovorID",
                        column: x => x.UgovorID,
                        principalTable: "Ugovori",
                        principalColumn: "UgovorID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Uplate_Uposlenici_UposlenikID",
                        column: x => x.UposlenikID,
                        principalTable: "Uposlenici",
                        principalColumn: "UposlenikID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Uplate_KorisnikID",
                table: "Uplate",
                column: "KorisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Uplate_NacinPlacanjaID",
                table: "Uplate",
                column: "NacinPlacanjaID");

            migrationBuilder.CreateIndex(
                name: "IX_Uplate_NekretninaID",
                table: "Uplate",
                column: "NekretninaID");

            migrationBuilder.CreateIndex(
                name: "IX_Uplate_UgovorID",
                table: "Uplate",
                column: "UgovorID");

            migrationBuilder.CreateIndex(
                name: "IX_Uplate_UposlenikID",
                table: "Uplate",
                column: "UposlenikID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Uplate");

            migrationBuilder.DropTable(
                name: "NaciniPlacanja");
        }
    }
}
