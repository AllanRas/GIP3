using Microsoft.EntityFrameworkCore.Migrations;

namespace Lekkerbek12Gip.Migrations
{
    public partial class updateBestellingGerecht : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BestellingGerecht",
                table: "BestellingGerecht");

            migrationBuilder.AddColumn<int>(
                name: "BestellingGerechtId",
                table: "BestellingGerecht",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BestellingGerecht",
                table: "BestellingGerecht",
                column: "BestellingGerechtId");

            migrationBuilder.CreateIndex(
                name: "IX_BestellingGerecht_GerechtId",
                table: "BestellingGerecht",
                column: "GerechtId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BestellingGerecht",
                table: "BestellingGerecht");

            migrationBuilder.DropIndex(
                name: "IX_BestellingGerecht_GerechtId",
                table: "BestellingGerecht");

            migrationBuilder.DropColumn(
                name: "BestellingGerechtId",
                table: "BestellingGerecht");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BestellingGerecht",
                table: "BestellingGerecht",
                columns: new[] { "GerechtId", "BestellingId" });
        }
    }
}
