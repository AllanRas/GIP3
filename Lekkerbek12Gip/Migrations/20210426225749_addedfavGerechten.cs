using Microsoft.EntityFrameworkCore.Migrations;

namespace Lekkerbek12Gip.Migrations
{
    public partial class addedfavGerechten : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gerechten_Klants_KlantId",
                table: "Gerechten");

            migrationBuilder.DropIndex(
                name: "IX_Gerechten_KlantId",
                table: "Gerechten");

            migrationBuilder.DropColumn(
                name: "KlantId",
                table: "Gerechten");

            migrationBuilder.CreateTable(
                name: "GerechtKlantFavorieten",
                columns: table => new
                {
                    KlantId = table.Column<int>(type: "int", nullable: false),
                    GerechtId = table.Column<int>(type: "int", nullable: false),
                    GerechtId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GerechtKlantFavorieten", x => new { x.GerechtId, x.KlantId });
                    table.ForeignKey(
                        name: "FK_GerechtKlantFavorieten_Gerechten_GerechtId",
                        column: x => x.GerechtId,
                        principalTable: "Gerechten",
                        principalColumn: "GerechtId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GerechtKlantFavorieten_Gerechten_GerechtId1",
                        column: x => x.GerechtId1,
                        principalTable: "Gerechten",
                        principalColumn: "GerechtId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GerechtKlantFavorieten_Klants_KlantId",
                        column: x => x.KlantId,
                        principalTable: "Klants",
                        principalColumn: "KlantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GerechtKlantFavorieten_GerechtId1",
                table: "GerechtKlantFavorieten",
                column: "GerechtId1");

            migrationBuilder.CreateIndex(
                name: "IX_GerechtKlantFavorieten_KlantId",
                table: "GerechtKlantFavorieten",
                column: "KlantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GerechtKlantFavorieten");

            migrationBuilder.AddColumn<int>(
                name: "KlantId",
                table: "Gerechten",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gerechten_KlantId",
                table: "Gerechten",
                column: "KlantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gerechten_Klants_KlantId",
                table: "Gerechten",
                column: "KlantId",
                principalTable: "Klants",
                principalColumn: "KlantId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
