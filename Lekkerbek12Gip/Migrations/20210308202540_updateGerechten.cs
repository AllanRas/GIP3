using Microsoft.EntityFrameworkCore.Migrations;

namespace Lekkerbek12Gip.Migrations
{
    public partial class updateGerechten : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BestellingGerecht");

            migrationBuilder.AddColumn<int>(
                name: "BestellingId",
                table: "Gerechten",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gerechten_BestellingId",
                table: "Gerechten",
                column: "BestellingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gerechten_Bestellings_BestellingId",
                table: "Gerechten",
                column: "BestellingId",
                principalTable: "Bestellings",
                principalColumn: "BestellingId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gerechten_Bestellings_BestellingId",
                table: "Gerechten");

            migrationBuilder.DropIndex(
                name: "IX_Gerechten_BestellingId",
                table: "Gerechten");

            migrationBuilder.DropColumn(
                name: "BestellingId",
                table: "Gerechten");

            migrationBuilder.CreateTable(
                name: "BestellingGerecht",
                columns: table => new
                {
                    BestellingenBestellingId = table.Column<int>(type: "int", nullable: false),
                    GerechtenGerechtId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BestellingGerecht", x => new { x.BestellingenBestellingId, x.GerechtenGerechtId });
                    table.ForeignKey(
                        name: "FK_BestellingGerecht_Bestellings_BestellingenBestellingId",
                        column: x => x.BestellingenBestellingId,
                        principalTable: "Bestellings",
                        principalColumn: "BestellingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BestellingGerecht_Gerechten_GerechtenGerechtId",
                        column: x => x.GerechtenGerechtId,
                        principalTable: "Gerechten",
                        principalColumn: "GerechtId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BestellingGerecht_GerechtenGerechtId",
                table: "BestellingGerecht",
                column: "GerechtenGerechtId");
        }
    }
}
