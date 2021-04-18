using Microsoft.EntityFrameworkCore.Migrations;

namespace Lekkerbek12Gip.Migrations
{
    public partial class kl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bestellings_Klants_KlantId",
                table: "Bestellings");

            migrationBuilder.AlterColumn<int>(
                name: "KlantId",
                table: "Bestellings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bestellings_Klants_KlantId",
                table: "Bestellings",
                column: "KlantId",
                principalTable: "Klants",
                principalColumn: "KlantId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bestellings_Klants_KlantId",
                table: "Bestellings");

            migrationBuilder.AlterColumn<int>(
                name: "KlantId",
                table: "Bestellings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Bestellings_Klants_KlantId",
                table: "Bestellings",
                column: "KlantId",
                principalTable: "Klants",
                principalColumn: "KlantId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
