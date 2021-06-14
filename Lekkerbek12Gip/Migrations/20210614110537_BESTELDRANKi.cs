using Microsoft.EntityFrameworkCore.Migrations;

namespace Lekkerbek12Gip.Migrations
{
    public partial class BESTELDRANKi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BestellingId1",
                table: "BestellingDrank",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BestellingDrank_BestellingId1",
                table: "BestellingDrank",
                column: "BestellingId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BestellingDrank_Bestellings_BestellingId1",
                table: "BestellingDrank",
                column: "BestellingId1",
                principalTable: "Bestellings",
                principalColumn: "BestellingId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BestellingDrank_Bestellings_BestellingId1",
                table: "BestellingDrank");

            migrationBuilder.DropIndex(
                name: "IX_BestellingDrank_BestellingId1",
                table: "BestellingDrank");

            migrationBuilder.DropColumn(
                name: "BestellingId1",
                table: "BestellingDrank");
        }
    }
}
