using Microsoft.EntityFrameworkCore.Migrations;

namespace Nekretnine.Data.Migrations
{
    public partial class Klasa_Notifikacije : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ObilazakID",
                table: "Notifikacije",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Notifikacije_ObilazakID",
                table: "Notifikacije",
                column: "ObilazakID");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifikacije_Obilasci_ObilazakID",
                table: "Notifikacije",
                column: "ObilazakID",
                principalTable: "Obilasci",
                principalColumn: "ObilazakID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifikacije_Obilasci_ObilazakID",
                table: "Notifikacije");

            migrationBuilder.DropIndex(
                name: "IX_Notifikacije_ObilazakID",
                table: "Notifikacije");

            migrationBuilder.DropColumn(
                name: "ObilazakID",
                table: "Notifikacije");
        }
    }
}
