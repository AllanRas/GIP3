using Microsoft.EntityFrameworkCore.Migrations;

namespace Lekkerbek12Gip.Migrations
{
    public partial class fixing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dranken_Bestellings_BestellingId",
                table: "Dranken");

            migrationBuilder.DropIndex(
                name: "IX_Dranken_BestellingId",
                table: "Dranken");

            migrationBuilder.DropColumn(
                name: "BestellingId",
                table: "Dranken");

            migrationBuilder.CreateTable(
                name: "BestellingDrank",
                columns: table => new
                {
                    BestellingenBestellingId = table.Column<int>(type: "int", nullable: false),
                    DrankenDrankId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BestellingDrank", x => new { x.BestellingenBestellingId, x.DrankenDrankId });
                    table.ForeignKey(
                        name: "FK_BestellingDrank_Bestellings_BestellingenBestellingId",
                        column: x => x.BestellingenBestellingId,
                        principalTable: "Bestellings",
                        principalColumn: "BestellingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BestellingDrank_Dranken_DrankenDrankId",
                        column: x => x.DrankenDrankId,
                        principalTable: "Dranken",
                        principalColumn: "DrankId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BestellingDrank_DrankenDrankId",
                table: "BestellingDrank",
                column: "DrankenDrankId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BestellingDrank");

            migrationBuilder.AddColumn<int>(
                name: "BestellingId",
                table: "Dranken",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dranken_BestellingId",
                table: "Dranken",
                column: "BestellingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dranken_Bestellings_BestellingId",
                table: "Dranken",
                column: "BestellingId",
                principalTable: "Bestellings",
                principalColumn: "BestellingId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
