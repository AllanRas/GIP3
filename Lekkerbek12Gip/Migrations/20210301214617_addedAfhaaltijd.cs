using Microsoft.EntityFrameworkCore.Migrations;

namespace Lekkerbek12Gip.Migrations
{
    public partial class addedAfhaaltijd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GetrouwheidsScore",
                table: "klants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Korting",
                table: "Bestellings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrijs",
                table: "Bestellings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GetrouwheidsScore",
                table: "klants");

            migrationBuilder.DropColumn(
                name: "Korting",
                table: "Bestellings");

            migrationBuilder.DropColumn(
                name: "TotalPrijs",
                table: "Bestellings");
        }
    }
}
