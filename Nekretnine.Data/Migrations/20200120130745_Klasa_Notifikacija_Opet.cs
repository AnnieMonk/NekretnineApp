using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nekretnine.Data.Migrations
{
    public partial class Klasa_Notifikacija_Opet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NotifikacijaID",
                table: "Korisnici",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Notifikacije",
                columns: table => new
                {
                    NotifikacijaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TextNotifikacije = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    DatumNotifikacije = table.Column<DateTime>(nullable: false),
                    UposlenikID = table.Column<int>(nullable: false),
                    KorisnikID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifikacije", x => x.NotifikacijaID);
                    table.ForeignKey(
                        name: "FK_Notifikacije_Korisnici_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifikacije_Uposlenici_UposlenikID",
                        column: x => x.UposlenikID,
                        principalTable: "Uposlenici",
                        principalColumn: "UposlenikID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifikacije_KorisnikID",
                table: "Notifikacije",
                column: "KorisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Notifikacije_UposlenikID",
                table: "Notifikacije",
                column: "UposlenikID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifikacije");

            migrationBuilder.DropColumn(
                name: "NotifikacijaID",
                table: "Korisnici");
        }
    }
}
