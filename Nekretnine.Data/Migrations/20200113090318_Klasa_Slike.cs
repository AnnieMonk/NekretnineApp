using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Nekretnine.Data.Migrations
{
    public partial class Klasa_Slike : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Slike",
                columns: table => new
                {
                    SlikeID = table.Column<Guid>(nullable: false),
                    MyImage = table.Column<string>(nullable: true),
                    Ekstenzija = table.Column<string>(nullable: true),
                    NekretninaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slike", x => x.SlikeID);
                    table.ForeignKey(
                        name: "FK_Slike_Nekretnine_NekretninaID",
                        column: x => x.NekretninaID,
                        principalTable: "Nekretnine",
                        principalColumn: "NekretninaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Slike_NekretninaID",
                table: "Slike",
                column: "NekretninaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Slike");
        }
    }
}
