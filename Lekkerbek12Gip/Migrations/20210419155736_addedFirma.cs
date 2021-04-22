using Microsoft.EntityFrameworkCore.Migrations;

namespace Lekkerbek12Gip.Migrations
{
    public partial class addedFirma : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FirmaId",
                table: "Klants",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Firmas",
                columns: table => new
                {
                    FirmaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirmaNaam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BtwNummer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firmas", x => x.FirmaId);
                });

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
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Klants_Firmas_FirmaId",
                table: "Klants");

            migrationBuilder.DropTable(
                name: "Firmas");

            migrationBuilder.DropIndex(
                name: "IX_Klants_FirmaId",
                table: "Klants");

            migrationBuilder.DropColumn(
                name: "FirmaId",
                table: "Klants");
        }
    }
}
