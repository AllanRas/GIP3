using Microsoft.EntityFrameworkCore.Migrations;

namespace Lekkerbek12Gip.Migrations
{
    public partial class AddBestellingStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BestelingStatus",
                table: "Bestellings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BestelingStatus",
                table: "Bestellings");
        }
    }
}
