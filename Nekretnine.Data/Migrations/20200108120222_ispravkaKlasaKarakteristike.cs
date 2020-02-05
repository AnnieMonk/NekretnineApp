using Microsoft.EntityFrameworkCore.Migrations;

namespace Nekretnine.Data.Migrations
{
    public partial class ispravkaKlasaKarakteristike : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Karakteristike_PonudjeniOdgovori_PonudjeniOdgovoriID",
                table: "Karakteristike");

            migrationBuilder.DropTable(
                name: "PonudjeniOdgovori");

            migrationBuilder.DropIndex(
                name: "IX_Karakteristike_PonudjeniOdgovoriID",
                table: "Karakteristike");

            migrationBuilder.DropColumn(
                name: "PonudjeniOdgovoriID",
                table: "Karakteristike");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PonudjeniOdgovoriID",
                table: "Karakteristike",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PonudjeniOdgovori",
                columns: table => new
                {
                    PonudjeniOdgovoriID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PonudjeniOdgovori", x => x.PonudjeniOdgovoriID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Karakteristike_PonudjeniOdgovoriID",
                table: "Karakteristike",
                column: "PonudjeniOdgovoriID");

            migrationBuilder.AddForeignKey(
                name: "FK_Karakteristike_PonudjeniOdgovori_PonudjeniOdgovoriID",
                table: "Karakteristike",
                column: "PonudjeniOdgovoriID",
                principalTable: "PonudjeniOdgovori",
                principalColumn: "PonudjeniOdgovoriID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
