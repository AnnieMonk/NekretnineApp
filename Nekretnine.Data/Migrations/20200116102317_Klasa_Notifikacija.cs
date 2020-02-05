using Microsoft.EntityFrameworkCore.Migrations;

namespace Nekretnine.Data.Migrations
{
    public partial class Klasa_Notifikacija : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NotifikacijaID",
                table: "Korisnici",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Notifikacije",
                columns: table => new
                {
                    NotifikacijaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    ExtraColumn = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifikacije", x => x.NotifikacijaID);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Korisnici_Notifikacije_NotifikacijaID",
                table: "Korisnici");

            migrationBuilder.DropTable(
                name: "Notifikacije");

            migrationBuilder.DropIndex(
                name: "IX_Korisnici_NotifikacijaID",
                table: "Korisnici");

            migrationBuilder.DropColumn(
                name: "NotifikacijaID",
                table: "Korisnici");
        }
    }
}
