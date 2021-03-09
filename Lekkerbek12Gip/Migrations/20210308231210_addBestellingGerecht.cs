using Microsoft.EntityFrameworkCore.Migrations;

namespace Lekkerbek12Gip.Migrations
{
    public partial class addBestellingGerecht : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BestellingGerecht_Bestellings_BestellingenBestellingId",
                table: "BestellingGerecht");

            migrationBuilder.DropForeignKey(
                name: "FK_BestellingGerecht_Gerechten_GerechtenGerechtId",
                table: "BestellingGerecht");

            migrationBuilder.DropColumn(
                name: "BestellingId",
                table: "Gerechten");

            migrationBuilder.RenameColumn(
                name: "GerechtenGerechtId",
                table: "BestellingGerecht",
                newName: "GerechtId");

            migrationBuilder.RenameColumn(
                name: "BestellingenBestellingId",
                table: "BestellingGerecht",
                newName: "BestellingId");

            migrationBuilder.RenameIndex(
                name: "IX_BestellingGerecht_GerechtenGerechtId",
                table: "BestellingGerecht",
                newName: "IX_BestellingGerecht_GerechtId");

            migrationBuilder.AddForeignKey(
                name: "FK_BestellingGerecht_Bestellings_BestellingId",
                table: "BestellingGerecht",
                column: "BestellingId",
                principalTable: "Bestellings",
                principalColumn: "BestellingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BestellingGerecht_Gerechten_GerechtId",
                table: "BestellingGerecht",
                column: "GerechtId",
                principalTable: "Gerechten",
                principalColumn: "GerechtId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BestellingGerecht_Bestellings_BestellingId",
                table: "BestellingGerecht");

            migrationBuilder.DropForeignKey(
                name: "FK_BestellingGerecht_Gerechten_GerechtId",
                table: "BestellingGerecht");

            migrationBuilder.RenameColumn(
                name: "GerechtId",
                table: "BestellingGerecht",
                newName: "GerechtenGerechtId");

            migrationBuilder.RenameColumn(
                name: "BestellingId",
                table: "BestellingGerecht",
                newName: "BestellingenBestellingId");

            migrationBuilder.RenameIndex(
                name: "IX_BestellingGerecht_GerechtId",
                table: "BestellingGerecht",
                newName: "IX_BestellingGerecht_GerechtenGerechtId");

            migrationBuilder.AddColumn<int>(
                name: "BestellingId",
                table: "Gerechten",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BestellingGerecht_Bestellings_BestellingenBestellingId",
                table: "BestellingGerecht",
                column: "BestellingenBestellingId",
                principalTable: "Bestellings",
                principalColumn: "BestellingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BestellingGerecht_Gerechten_GerechtenGerechtId",
                table: "BestellingGerecht",
                column: "GerechtenGerechtId",
                principalTable: "Gerechten",
                principalColumn: "GerechtId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
