using Microsoft.EntityFrameworkCore.Migrations;

namespace Lekkerbek12Gip.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    GerechtenId = table.Column<int>(type: "int", nullable: false),
                    bestellingenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BestellingGerecht", x => new { x.GerechtenId, x.bestellingenId });
                    table.ForeignKey(
                        name: "FK_BestellingGerecht_Bestellings_bestellingenId",
                        column: x => x.bestellingenId,
                        principalTable: "Bestellings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BestellingGerecht_Gerechten_GerechtenId",
                        column: x => x.GerechtenId,
                        principalTable: "Gerechten",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BestellingGerecht_bestellingenId",
                table: "BestellingGerecht",
                column: "bestellingenId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
