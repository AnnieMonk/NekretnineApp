using Microsoft.EntityFrameworkCore.Migrations;

namespace Nekretnine.Data.Migrations
{
    public partial class Uposlenik_DodanatributZaNalog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KorisnickiNalogID",
                table: "Uposlenici",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Uposlenici_KorisnickiNalogID",
                table: "Uposlenici",
                column: "KorisnickiNalogID");

            migrationBuilder.AddForeignKey(
                name: "FK_Uposlenici_KorisnickiNalozi_KorisnickiNalogID",
                table: "Uposlenici",
                column: "KorisnickiNalogID",
                principalTable: "KorisnickiNalozi",
                principalColumn: "KorisnickiNalogID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Uposlenici_KorisnickiNalozi_KorisnickiNalogID",
                table: "Uposlenici");

            migrationBuilder.DropIndex(
                name: "IX_Uposlenici_KorisnickiNalogID",
                table: "Uposlenici");

            migrationBuilder.DropColumn(
                name: "KorisnickiNalogID",
                table: "Uposlenici");
        }
    }
}
