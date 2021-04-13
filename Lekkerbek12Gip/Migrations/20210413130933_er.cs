using Microsoft.EntityFrameworkCore.Migrations;

namespace Lekkerbek12Gip.Migrations
{
    public partial class er : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BestellingGerechten_BestellingId1",
                table: "BestellingGerechten");

            migrationBuilder.DropColumn(
                name: "BestellingGerechtenId",
                table: "Bestellings");

            migrationBuilder.DropColumn(
                name: "BestellingGerechtenId",
                table: "BestellingGerechten");

            migrationBuilder.CreateIndex(
                name: "IX_BestellingGerechten_BestellingId1",
                table: "BestellingGerechten",
                column: "BestellingId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BestellingGerechten_BestellingId1",
                table: "BestellingGerechten");

            migrationBuilder.AddColumn<int>(
                name: "BestellingGerechtenId",
                table: "Bestellings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BestellingGerechtenId",
                table: "BestellingGerechten",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BestellingGerechten_BestellingId1",
                table: "BestellingGerechten",
                column: "BestellingId1",
                unique: true,
                filter: "[BestellingId1] IS NOT NULL");
        }
    }
}
