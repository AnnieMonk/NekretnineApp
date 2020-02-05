using Microsoft.EntityFrameworkCore.Migrations;

namespace Nekretnine.Data.Migrations
{
    public partial class DroppedPDF : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PDFID",
                table: "Ugovori",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PDFs",
                columns: table => new
                {
                    PDFID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MyPDF = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
    }
}
