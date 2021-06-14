using Microsoft.EntityFrameworkCore.Migrations;

namespace Lekkerbek12Gip.Migrations
{
    public partial class BESTELDRANKiSET : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BestellingDrank_Bestellings_BestellingId",
                table: "BestellingDrank");

            migrationBuilder.DropForeignKey(
                name: "FK_BestellingDrank_Bestellings_BestellingId1",
                table: "BestellingDrank");

            migrationBuilder.DropForeignKey(
                name: "FK_BestellingDrank_Dranken_DrankId",
                table: "BestellingDrank");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BestellingDrank",
                table: "BestellingDrank");

            migrationBuilder.RenameTable(
                name: "BestellingDrank",
                newName: "BestellingDranks");

            migrationBuilder.RenameIndex(
                name: "IX_BestellingDrank_DrankId",
                table: "BestellingDranks",
                newName: "IX_BestellingDranks_DrankId");

            migrationBuilder.RenameIndex(
                name: "IX_BestellingDrank_BestellingId1",
                table: "BestellingDranks",
                newName: "IX_BestellingDranks_BestellingId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BestellingDranks",
                table: "BestellingDranks",
                columns: new[] { "BestellingId", "DrankId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BestellingDranks_Bestellings_BestellingId",
                table: "BestellingDranks",
                column: "BestellingId",
                principalTable: "Bestellings",
                principalColumn: "BestellingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BestellingDranks_Bestellings_BestellingId1",
                table: "BestellingDranks",
                column: "BestellingId1",
                principalTable: "Bestellings",
                principalColumn: "BestellingId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BestellingDranks_Dranken_DrankId",
                table: "BestellingDranks",
                column: "DrankId",
                principalTable: "Dranken",
                principalColumn: "DrankId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BestellingDranks_Bestellings_BestellingId",
                table: "BestellingDranks");

            migrationBuilder.DropForeignKey(
                name: "FK_BestellingDranks_Bestellings_BestellingId1",
                table: "BestellingDranks");

            migrationBuilder.DropForeignKey(
                name: "FK_BestellingDranks_Dranken_DrankId",
                table: "BestellingDranks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BestellingDranks",
                table: "BestellingDranks");

            migrationBuilder.RenameTable(
                name: "BestellingDranks",
                newName: "BestellingDrank");

            migrationBuilder.RenameIndex(
                name: "IX_BestellingDranks_DrankId",
                table: "BestellingDrank",
                newName: "IX_BestellingDrank_DrankId");

            migrationBuilder.RenameIndex(
                name: "IX_BestellingDranks_BestellingId1",
                table: "BestellingDrank",
                newName: "IX_BestellingDrank_BestellingId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BestellingDrank",
                table: "BestellingDrank",
                columns: new[] { "BestellingId", "DrankId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BestellingDrank_Bestellings_BestellingId",
                table: "BestellingDrank",
                column: "BestellingId",
                principalTable: "Bestellings",
                principalColumn: "BestellingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BestellingDrank_Bestellings_BestellingId1",
                table: "BestellingDrank",
                column: "BestellingId1",
                principalTable: "Bestellings",
                principalColumn: "BestellingId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BestellingDrank_Dranken_DrankId",
                table: "BestellingDrank",
                column: "DrankId",
                principalTable: "Dranken",
                principalColumn: "DrankId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
