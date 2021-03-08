using Microsoft.EntityFrameworkCore.Migrations;

namespace Lekkerbek12Gip.Migrations
{
    public partial class gere : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BestellingId1",
                table: "Gerechten",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gerechten_BestellingId1",
                table: "Gerechten",
                column: "BestellingId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Gerechten_Bestellings_BestellingId1",
                table: "Gerechten",
                column: "BestellingId1",
                principalTable: "Bestellings",
                principalColumn: "BestellingId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gerechten_Bestellings_BestellingId1",
                table: "Gerechten");

            migrationBuilder.DropIndex(
                name: "IX_Gerechten_BestellingId1",
                table: "Gerechten");

            migrationBuilder.DropColumn(
                name: "BestellingId1",
                table: "Gerechten");
        }
    }
}
