using Microsoft.EntityFrameworkCore.Migrations;

namespace Lekkerbek12Gip.Migrations
{
    public partial class s : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bestellings_bestellingGerechts_BestellingGerechtId",
                table: "Bestellings");

            migrationBuilder.DropForeignKey(
                name: "FK_Gerechten_bestellingGerechts_BestellingGerechtId",
                table: "Gerechten");

            migrationBuilder.DropIndex(
                name: "IX_Gerechten_BestellingGerechtId",
                table: "Gerechten");

            migrationBuilder.DropIndex(
                name: "IX_Bestellings_BestellingGerechtId",
                table: "Bestellings");

            migrationBuilder.DropColumn(
                name: "BestellingGerechtId",
                table: "Gerechten");

            migrationBuilder.DropColumn(
                name: "BestellingGerechtId",
                table: "Bestellings");

            migrationBuilder.CreateIndex(
                name: "IX_bestellingGerechts_BestellingId",
                table: "bestellingGerechts",
                column: "BestellingId");

            migrationBuilder.CreateIndex(
                name: "IX_bestellingGerechts_GerechtId",
                table: "bestellingGerechts",
                column: "GerechtId");

            migrationBuilder.AddForeignKey(
                name: "FK_bestellingGerechts_Bestellings_BestellingId",
                table: "bestellingGerechts",
                column: "BestellingId",
                principalTable: "Bestellings",
                principalColumn: "BestellingId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_bestellingGerechts_Gerechten_GerechtId",
                table: "bestellingGerechts",
                column: "GerechtId",
                principalTable: "Gerechten",
                principalColumn: "GerechtId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bestellingGerechts_Bestellings_BestellingId",
                table: "bestellingGerechts");

            migrationBuilder.DropForeignKey(
                name: "FK_bestellingGerechts_Gerechten_GerechtId",
                table: "bestellingGerechts");

            migrationBuilder.DropIndex(
                name: "IX_bestellingGerechts_BestellingId",
                table: "bestellingGerechts");

            migrationBuilder.DropIndex(
                name: "IX_bestellingGerechts_GerechtId",
                table: "bestellingGerechts");

            migrationBuilder.AddColumn<int>(
                name: "BestellingGerechtId",
                table: "Gerechten",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BestellingGerechtId",
                table: "Bestellings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gerechten_BestellingGerechtId",
                table: "Gerechten",
                column: "BestellingGerechtId");

            migrationBuilder.CreateIndex(
                name: "IX_Bestellings_BestellingGerechtId",
                table: "Bestellings",
                column: "BestellingGerechtId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bestellings_bestellingGerechts_BestellingGerechtId",
                table: "Bestellings",
                column: "BestellingGerechtId",
                principalTable: "bestellingGerechts",
                principalColumn: "BestellingGerechtId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Gerechten_bestellingGerechts_BestellingGerechtId",
                table: "Gerechten",
                column: "BestellingGerechtId",
                principalTable: "bestellingGerechts",
                principalColumn: "BestellingGerechtId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
