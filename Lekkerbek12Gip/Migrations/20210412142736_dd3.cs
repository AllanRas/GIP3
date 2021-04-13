using Microsoft.EntityFrameworkCore.Migrations;

namespace Lekkerbek12Gip.Migrations
{
    public partial class dd3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BestellingGerechtenId",
                table: "Bestellings");

            migrationBuilder.DropColumn(
                name: "BestellingGerechtenId",
                table: "BestellingGerechten");
        }
    }
}
