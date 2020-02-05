using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nekretnine.Data.Migrations
{
    public partial class recreatedPDF : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PDFID",
                table: "Ugovori",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "PDFID1",
                table: "Ugovori",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PDFs",
                columns: table => new
                {
                    PDFID = table.Column<Guid>(nullable: false),
                    MyPDF = table.Column<string>(nullable: true),
                    Extension = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PDFs", x => x.PDFID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ugovori_PDFID1",
                table: "Ugovori",
                column: "PDFID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Ugovori_PDFs_PDFID1",
                table: "Ugovori",
                column: "PDFID1",
                principalTable: "PDFs",
                principalColumn: "PDFID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ugovori_PDFs_PDFID1",
                table: "Ugovori");

            migrationBuilder.DropTable(
                name: "PDFs");

            migrationBuilder.DropIndex(
                name: "IX_Ugovori_PDFID1",
                table: "Ugovori");

            migrationBuilder.DropColumn(
                name: "PDFID",
                table: "Ugovori");

            migrationBuilder.DropColumn(
                name: "PDFID1",
                table: "Ugovori");
        }
    }
}
