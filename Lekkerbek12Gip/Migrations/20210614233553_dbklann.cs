using Microsoft.EntityFrameworkCore.Migrations;

namespace Lekkerbek12Gip.Migrations
{
    public partial class dbklann : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Klants_Firmas_FirmaId",
                table: "Klants");

            migrationBuilder.DropIndex(
                name: "IX_Klants_FirmaId",
                table: "Klants");

            migrationBuilder.DropColumn(
                name: "FirmaId",
                table: "Klants");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FirmaId",
                table: "Klants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Klants_FirmaId",
                table: "Klants",
                column: "FirmaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Klants_Firmas_FirmaId",
                table: "Klants",
                column: "FirmaId",
                principalTable: "Firmas",
                principalColumn: "FirmaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
