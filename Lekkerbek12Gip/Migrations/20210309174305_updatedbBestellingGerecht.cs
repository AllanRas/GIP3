using Microsoft.EntityFrameworkCore.Migrations;

namespace Lekkerbek12Gip.Migrations
{
    public partial class updatedbBestellingGerecht : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BestellingGerecht_Bestellings_BestellingId",
                table: "BestellingGerecht");

            migrationBuilder.DropForeignKey(
                name: "FK_BestellingGerecht_Gerechten_GerechtId",
                table: "BestellingGerecht");

            migrationBuilder.AddColumn<int>(
                name: "GerechtId",
                table: "Bestellings",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GerechtId",
                table: "BestellingGerecht",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BestellingId",
                table: "BestellingGerecht",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Aantal",
                table: "BestellingGerecht",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValueSql: "0");

            migrationBuilder.CreateIndex(
                name: "IX_Bestellings_GerechtId",
                table: "Bestellings",
                column: "GerechtId");

            migrationBuilder.AddForeignKey(
                name: "FK_BestellingGerecht_Bestellings_BestellingId",
                table: "BestellingGerecht",
                column: "BestellingId",
                principalTable: "Bestellings",
                principalColumn: "BestellingId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BestellingGerecht_Gerechten_GerechtId",
                table: "BestellingGerecht",
                column: "GerechtId",
                principalTable: "Gerechten",
                principalColumn: "GerechtId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bestellings_Gerechten_GerechtId",
                table: "Bestellings",
                column: "GerechtId",
                principalTable: "Gerechten",
                principalColumn: "GerechtId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BestellingGerecht_Bestellings_BestellingId",
                table: "BestellingGerecht");

            migrationBuilder.DropForeignKey(
                name: "FK_BestellingGerecht_Gerechten_GerechtId",
                table: "BestellingGerecht");

            migrationBuilder.DropForeignKey(
                name: "FK_Bestellings_Gerechten_GerechtId",
                table: "Bestellings");

            migrationBuilder.DropIndex(
                name: "IX_Bestellings_GerechtId",
                table: "Bestellings");

            migrationBuilder.DropColumn(
                name: "GerechtId",
                table: "Bestellings");

            migrationBuilder.AlterColumn<int>(
                name: "GerechtId",
                table: "BestellingGerecht",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BestellingId",
                table: "BestellingGerecht",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Aantal",
                table: "BestellingGerecht",
                type: "int",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_BestellingGerecht_Bestellings_BestellingId",
                table: "BestellingGerecht",
                column: "BestellingId",
                principalTable: "Bestellings",
                principalColumn: "BestellingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BestellingGerecht_Gerechten_GerechtId",
                table: "BestellingGerecht",
                column: "GerechtId",
                principalTable: "Gerechten",
                principalColumn: "GerechtId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
