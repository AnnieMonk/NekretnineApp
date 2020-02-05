using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nekretnine.Data.Migrations
{
    public partial class Klasa_Ugovor_VrstaUgovora : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VrsteUgovora",
                columns: table => new
                {
                    VrstaUgovoraID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VrsteUgovora", x => x.VrstaUgovoraID);
                });

            migrationBuilder.CreateTable(
                name: "Ugovori",
                columns: table => new
                {
                    UgovorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Oznaka = table.Column<string>(nullable: true),
                    DatumKreiranja = table.Column<DateTime>(nullable: false),
                    VrstaUgovoraID = table.Column<int>(nullable: false),
                    NekretninaID = table.Column<int>(nullable: false),
                    KorisnikID = table.Column<int>(nullable: false),
                    UposlenikID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ugovori", x => x.UgovorID);
                    table.ForeignKey(
                        name: "FK_Ugovori_Korisnici_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ugovori_Nekretnine_NekretninaID",
                        column: x => x.NekretninaID,
                        principalTable: "Nekretnine",
                        principalColumn: "NekretninaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ugovori_Uposlenici_UposlenikID",
                        column: x => x.UposlenikID,
                        principalTable: "Uposlenici",
                        principalColumn: "UposlenikID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ugovori_VrsteUgovora_VrstaUgovoraID",
                        column: x => x.VrstaUgovoraID,
                        principalTable: "VrsteUgovora",
                        principalColumn: "VrstaUgovoraID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ugovori_KorisnikID",
                table: "Ugovori",
                column: "KorisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Ugovori_NekretninaID",
                table: "Ugovori",
                column: "NekretninaID");

            migrationBuilder.CreateIndex(
                name: "IX_Ugovori_UposlenikID",
                table: "Ugovori",
                column: "UposlenikID");

            migrationBuilder.CreateIndex(
                name: "IX_Ugovori_VrstaUgovoraID",
                table: "Ugovori",
                column: "VrstaUgovoraID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ugovori");

            migrationBuilder.DropTable(
                name: "VrsteUgovora");
        }
    }
}
