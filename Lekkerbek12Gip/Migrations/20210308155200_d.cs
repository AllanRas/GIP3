using Microsoft.EntityFrameworkCore.Migrations;

namespace Lekkerbek12Gip.Migrations
{
    public partial class d : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "bestellingGerechts",
                columns: table => new
                {
                    BestellingGerechtId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BestellingId = table.Column<int>(type: "int", nullable: true),
                    GerechtId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bestellingGerechts", x => x.BestellingGerechtId);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bestellings_bestellingGerechts_BestellingGerechtId",
                table: "Bestellings");

            migrationBuilder.DropForeignKey(
                name: "FK_Gerechten_bestellingGerechts_BestellingGerechtId",
                table: "Gerechten");

            migrationBuilder.DropTable(
                name: "bestellingGerechts");

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
        }
    }
}
