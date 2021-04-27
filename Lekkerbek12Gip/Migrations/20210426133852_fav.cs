using Microsoft.EntityFrameworkCore.Migrations;

namespace Lekkerbek12Gip.Migrations
{
    public partial class fav : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
