using Microsoft.EntityFrameworkCore.Migrations;

namespace Lekkerbek12Gip.Migrations
{
    public partial class be : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BestellingId1",
                table: "BestellingGerechten",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BestellingGerechten_BestellingId1",
                table: "BestellingGerechten",
                column: "BestellingId1",
                unique: true,
                filter: "[BestellingId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_BestellingGerechten_Bestellings_BestellingId1",
                table: "BestellingGerechten",
                column: "BestellingId1",
                principalTable: "Bestellings",
                principalColumn: "BestellingId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BestellingGerechten_Bestellings_BestellingId1",
                table: "BestellingGerechten");

            migrationBuilder.DropIndex(
                name: "IX_BestellingGerechten_BestellingId1",
                table: "BestellingGerechten");

            migrationBuilder.DropColumn(
                name: "BestellingId1",
                table: "BestellingGerechten");
        }
    }
}
