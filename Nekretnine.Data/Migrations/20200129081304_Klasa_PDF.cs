using Microsoft.EntityFrameworkCore.Migrations;

namespace Nekretnine.Data.Migrations
{
    public partial class Klasa_PDF : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Ugovori");

            migrationBuilder.AddColumn<int>(
                name: "PDFID",
                table: "Ugovori",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PDFs",
                columns: table => new
                {
                    PDFID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MyPDF = table.Column<string>(nullable: true),
                    Extension = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PDFs", x => x.PDFID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ugovori_PDFID",
                table: "Ugovori",
                column: "PDFID");

            migrationBuilder.AddForeignKey(
                name: "FK_Ugovori_PDFs_PDFID",
                table: "Ugovori",
                column: "PDFID",
                principalTable: "PDFs",
                principalColumn: "PDFID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ugovori_PDFs_PDFID",
                table: "Ugovori");

            migrationBuilder.DropTable(
                name: "PDFs");

            migrationBuilder.DropIndex(
                name: "IX_Ugovori_PDFID",
                table: "Ugovori");

            migrationBuilder.DropColumn(
                name: "PDFID",
                table: "Ugovori");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Ugovori",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
