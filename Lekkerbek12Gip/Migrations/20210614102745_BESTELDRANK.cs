using Microsoft.EntityFrameworkCore.Migrations;

namespace Lekkerbek12Gip.Migrations
{
    public partial class BESTELDRANK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BestellingDrank_Bestellings_BestellingenBestellingId",
                table: "BestellingDrank");

            migrationBuilder.DropForeignKey(
                name: "FK_BestellingDrank_Dranken_DrankenDrankId",
                table: "BestellingDrank");

            migrationBuilder.RenameColumn(
                name: "DrankenDrankId",
                table: "BestellingDrank",
                newName: "DrankId");

            migrationBuilder.RenameColumn(
                name: "BestellingenBestellingId",
                table: "BestellingDrank",
                newName: "BestellingId");

            migrationBuilder.RenameIndex(
                name: "IX_BestellingDrank_DrankenDrankId",
                table: "BestellingDrank",
                newName: "IX_BestellingDrank_DrankId");

            migrationBuilder.AddColumn<int>(
                name: "Aantal",
                table: "BestellingDrank",
                type: "int",
                nullable: false,
                defaultValueSql: "0");

            migrationBuilder.AddForeignKey(
                name: "FK_BestellingDrank_Bestellings_BestellingId",
                table: "BestellingDrank",
                column: "BestellingId",
                principalTable: "Bestellings",
                principalColumn: "BestellingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BestellingDrank_Dranken_DrankId",
                table: "BestellingDrank",
                column: "DrankId",
                principalTable: "Dranken",
                principalColumn: "DrankId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BestellingDrank_Bestellings_BestellingId",
                table: "BestellingDrank");

            migrationBuilder.DropForeignKey(
                name: "FK_BestellingDrank_Dranken_DrankId",
                table: "BestellingDrank");

            migrationBuilder.DropColumn(
                name: "Aantal",
                table: "BestellingDrank");

            migrationBuilder.RenameColumn(
                name: "DrankId",
                table: "BestellingDrank",
                newName: "DrankenDrankId");

            migrationBuilder.RenameColumn(
                name: "BestellingId",
                table: "BestellingDrank",
                newName: "BestellingenBestellingId");

            migrationBuilder.RenameIndex(
                name: "IX_BestellingDrank_DrankId",
                table: "BestellingDrank",
                newName: "IX_BestellingDrank_DrankenDrankId");

            migrationBuilder.AddForeignKey(
                name: "FK_BestellingDrank_Bestellings_BestellingenBestellingId",
                table: "BestellingDrank",
                column: "BestellingenBestellingId",
                principalTable: "Bestellings",
                principalColumn: "BestellingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BestellingDrank_Dranken_DrankenDrankId",
                table: "BestellingDrank",
                column: "DrankenDrankId",
                principalTable: "Dranken",
                principalColumn: "DrankId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
