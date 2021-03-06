using Microsoft.EntityFrameworkCore.Migrations;

namespace Lekkerbek12Gip.Migrations
{
    public partial class updatetables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "Bestellings");

            migrationBuilder.AddColumn<bool>(
                name: "Afgerekend",
                table: "Bestellings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SpecialeWensen",
                table: "Bestellings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Gerechten",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Omschrijving = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prijs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Categorie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BestellingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gerechten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gerechten_Bestellings_BestellingId",
                        column: x => x.BestellingId,
                        principalTable: "Bestellings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gerechten_BestellingId",
                table: "Gerechten",
                column: "BestellingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gerechten");

            migrationBuilder.DropColumn(
                name: "Afgerekend",
                table: "Bestellings");

            migrationBuilder.DropColumn(
                name: "SpecialeWensen",
                table: "Bestellings");

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Bestellings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
