using Microsoft.EntityFrameworkCore.Migrations;

namespace Nekretnine.Data.Migrations
{
    public partial class Klasa_Karakteristike : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Karakteristike_VrsteGrijanja_vrstaGrijanjaID",
                table: "Karakteristike");

            migrationBuilder.DropForeignKey(
                name: "FK_NekretninaKarks_Karakteristike_KarakteristikeID",
                table: "NekretninaKarks");

            migrationBuilder.DropForeignKey(
                name: "FK_NekretninaKarks_Nekretnine_NekretninaID",
                table: "NekretninaKarks");

            migrationBuilder.DropTable(
                name: "VrsteGrijanja");

            migrationBuilder.DropIndex(
                name: "IX_Karakteristike_vrstaGrijanjaID",
                table: "Karakteristike");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NekretninaKarks",
                table: "NekretninaKarks");

            migrationBuilder.DropColumn(
                name: "Balkon",
                table: "Karakteristike");

            migrationBuilder.DropColumn(
                name: "Garaza",
                table: "Karakteristike");

            migrationBuilder.DropColumn(
                name: "Internet",
                table: "Karakteristike");

            migrationBuilder.DropColumn(
                name: "Kablovska",
                table: "Karakteristike");

            migrationBuilder.DropColumn(
                name: "Kanalizacija",
                table: "Karakteristike");

            migrationBuilder.DropColumn(
                name: "Klima",
                table: "Karakteristike");

            migrationBuilder.DropColumn(
                name: "Lift",
                table: "Karakteristike");

            migrationBuilder.DropColumn(
                name: "Namjesten",
                table: "Karakteristike");

            migrationBuilder.DropColumn(
                name: "NedavnoAdaptiran",
                table: "Karakteristike");

            migrationBuilder.DropColumn(
                name: "Ostava",
                table: "Karakteristike");

            migrationBuilder.DropColumn(
                name: "Parking",
                table: "Karakteristike");

            migrationBuilder.DropColumn(
                name: "Plin",
                table: "Karakteristike");

            migrationBuilder.DropColumn(
                name: "Struja",
                table: "Karakteristike");

            migrationBuilder.DropColumn(
                name: "VideoNadzor",
                table: "Karakteristike");

            migrationBuilder.DropColumn(
                name: "Voda",
                table: "Karakteristike");

            migrationBuilder.DropColumn(
                name: "vrstaGrijanjaID",
                table: "Karakteristike");

            migrationBuilder.RenameTable(
                name: "NekretninaKarks",
                newName: "NekretninaKarakteristike");

            migrationBuilder.RenameIndex(
                name: "IX_NekretninaKarks_NekretninaID",
                table: "NekretninaKarakteristike",
                newName: "IX_NekretninaKarakteristike_NekretninaID");

            migrationBuilder.RenameIndex(
                name: "IX_NekretninaKarks_KarakteristikeID",
                table: "NekretninaKarakteristike",
                newName: "IX_NekretninaKarakteristike_KarakteristikeID");

            migrationBuilder.AddColumn<string>(
                name: "Naziv",
                table: "Karakteristike",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PonudjeniOdgovoriID",
                table: "Karakteristike",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NekretninaKarakteristike",
                table: "NekretninaKarakteristike",
                column: "NekretninaKarakteristikeID");

            migrationBuilder.CreateTable(
                name: "PonudjeniOdgovori",
                columns: table => new
                {
                    PonudjeniOdgovoriID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
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

            migrationBuilder.AddForeignKey(
                name: "FK_NekretninaKarakteristike_Karakteristike_KarakteristikeID",
                table: "NekretninaKarakteristike",
                column: "KarakteristikeID",
                principalTable: "Karakteristike",
                principalColumn: "KarakteristikeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NekretninaKarakteristike_Nekretnine_NekretninaID",
                table: "NekretninaKarakteristike",
                column: "NekretninaID",
                principalTable: "Nekretnine",
                principalColumn: "NekretninaID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Karakteristike_PonudjeniOdgovori_PonudjeniOdgovoriID",
                table: "Karakteristike");

            migrationBuilder.DropForeignKey(
                name: "FK_NekretninaKarakteristike_Karakteristike_KarakteristikeID",
                table: "NekretninaKarakteristike");

            migrationBuilder.DropForeignKey(
                name: "FK_NekretninaKarakteristike_Nekretnine_NekretninaID",
                table: "NekretninaKarakteristike");

            migrationBuilder.DropTable(
                name: "PonudjeniOdgovori");

            migrationBuilder.DropIndex(
                name: "IX_Karakteristike_PonudjeniOdgovoriID",
                table: "Karakteristike");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NekretninaKarakteristike",
                table: "NekretninaKarakteristike");

            migrationBuilder.DropColumn(
                name: "Naziv",
                table: "Karakteristike");

            migrationBuilder.DropColumn(
                name: "PonudjeniOdgovoriID",
                table: "Karakteristike");

            migrationBuilder.RenameTable(
                name: "NekretninaKarakteristike",
                newName: "NekretninaKarks");

            migrationBuilder.RenameIndex(
                name: "IX_NekretninaKarakteristike_NekretninaID",
                table: "NekretninaKarks",
                newName: "IX_NekretninaKarks_NekretninaID");

            migrationBuilder.RenameIndex(
                name: "IX_NekretninaKarakteristike_KarakteristikeID",
                table: "NekretninaKarks",
                newName: "IX_NekretninaKarks_KarakteristikeID");

            migrationBuilder.AddColumn<bool>(
                name: "Balkon",
                table: "Karakteristike",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Garaza",
                table: "Karakteristike",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Internet",
                table: "Karakteristike",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Kablovska",
                table: "Karakteristike",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Kanalizacija",
                table: "Karakteristike",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Klima",
                table: "Karakteristike",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Lift",
                table: "Karakteristike",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Namjesten",
                table: "Karakteristike",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NedavnoAdaptiran",
                table: "Karakteristike",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Ostava",
                table: "Karakteristike",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Parking",
                table: "Karakteristike",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Plin",
                table: "Karakteristike",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Struja",
                table: "Karakteristike",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "VideoNadzor",
                table: "Karakteristike",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Voda",
                table: "Karakteristike",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "vrstaGrijanjaID",
                table: "Karakteristike",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NekretninaKarks",
                table: "NekretninaKarks",
                column: "NekretninaKarakteristikeID");

            migrationBuilder.CreateTable(
                name: "VrsteGrijanja",
                columns: table => new
                {
                    VrstaGrijanjaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VrsteGrijanja", x => x.VrstaGrijanjaID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Karakteristike_vrstaGrijanjaID",
                table: "Karakteristike",
                column: "vrstaGrijanjaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Karakteristike_VrsteGrijanja_vrstaGrijanjaID",
                table: "Karakteristike",
                column: "vrstaGrijanjaID",
                principalTable: "VrsteGrijanja",
                principalColumn: "VrstaGrijanjaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NekretninaKarks_Karakteristike_KarakteristikeID",
                table: "NekretninaKarks",
                column: "KarakteristikeID",
                principalTable: "Karakteristike",
                principalColumn: "KarakteristikeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NekretninaKarks_Nekretnine_NekretninaID",
                table: "NekretninaKarks",
                column: "NekretninaID",
                principalTable: "Nekretnine",
                principalColumn: "NekretninaID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
