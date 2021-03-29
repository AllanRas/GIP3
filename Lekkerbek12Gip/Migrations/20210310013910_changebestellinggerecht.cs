using Microsoft.EntityFrameworkCore.Migrations;

namespace Lekkerbek12Gip.Migrations
{
    public partial class changebestellinggerecht : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BestellingGerecht");

            migrationBuilder.CreateTable(
                name: "BestellingGerechten",
                columns: table => new
                {
                    BestellingId = table.Column<int>(type: "int", nullable: false),
                    GerechtId = table.Column<int>(type: "int", nullable: false),
                    Aantal = table.Column<int>(type: "int", nullable: false, defaultValueSql: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BestellingGerechten", x => new { x.BestellingId, x.GerechtId });
                    table.ForeignKey(
                        name: "FK_BestellingGerechten_Bestellings_BestellingId",
                        column: x => x.BestellingId,
                        principalTable: "Bestellings",
                        principalColumn: "BestellingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BestellingGerechten_Gerechten_GerechtId",
                        column: x => x.GerechtId,
                        principalTable: "Gerechten",
                        principalColumn: "GerechtId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BestellingGerechten_GerechtId",
                table: "BestellingGerechten",
                column: "GerechtId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BestellingGerechten");

            migrationBuilder.CreateTable(
                name: "BestellingGerecht",
                columns: table => new
                {
                    BestellingGerechtId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Aantal = table.Column<int>(type: "int", nullable: false, defaultValueSql: "0"),
                    BestellingId = table.Column<int>(type: "int", nullable: true),
                    GerechtId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BestellingGerecht", x => x.BestellingGerechtId);
                    table.ForeignKey(
                        name: "FK_BestellingGerecht_Bestellings_BestellingId",
                        column: x => x.BestellingId,
                        principalTable: "Bestellings",
                        principalColumn: "BestellingId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BestellingGerecht_Gerechten_GerechtId",
                        column: x => x.GerechtId,
                        principalTable: "Gerechten",
                        principalColumn: "GerechtId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BestellingGerecht_BestellingId",
                table: "BestellingGerecht",
                column: "BestellingId");

            migrationBuilder.CreateIndex(
                name: "IX_BestellingGerecht_GerechtId",
                table: "BestellingGerecht",
                column: "GerechtId");
        }
    }
}
