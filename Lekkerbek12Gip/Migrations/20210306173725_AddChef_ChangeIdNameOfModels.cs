using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lekkerbek12Gip.Migrations
{
    public partial class AddChef_ChangeIdNameOfModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chefs",
                columns: table => new
                {
                    ChefId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChefName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BestellingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chefs", x => x.ChefId);
                });

            migrationBuilder.CreateTable(
                name: "Gerechten",
                columns: table => new
                {
                    GerechtId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Omschrijving = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prijs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Categorie = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gerechten", x => x.GerechtId);
                });

            migrationBuilder.CreateTable(
                name: "Klants",
                columns: table => new
                {
                    KlantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GetrouwheidsScore = table.Column<int>(type: "int", nullable: false),
                    Geboortedatum = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klants", x => x.KlantId);
                });

            migrationBuilder.CreateTable(
                name: "Bestellings",
                columns: table => new
                {
                    BestellingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KlantId = table.Column<int>(type: "int", nullable: true),
                    ChefId = table.Column<int>(type: "int", nullable: true),
                    SpecialeWensen = table.Column<int>(type: "int", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Afgerekend = table.Column<bool>(type: "bit", nullable: false),
                    AfhaalTijd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Korting = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bestellings", x => x.BestellingId);
                    table.ForeignKey(
                        name: "FK_Bestellings_Chefs_ChefId",
                        column: x => x.ChefId,
                        principalTable: "Chefs",
                        principalColumn: "ChefId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bestellings_Klants_KlantId",
                        column: x => x.KlantId,
                        principalTable: "Klants",
                        principalColumn: "KlantId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BestellingGerecht",
                columns: table => new
                {
                    BestellingenBestellingId = table.Column<int>(type: "int", nullable: false),
                    GerechtenGerechtId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BestellingGerecht", x => new { x.BestellingenBestellingId, x.GerechtenGerechtId });
                    table.ForeignKey(
                        name: "FK_BestellingGerecht_Bestellings_BestellingenBestellingId",
                        column: x => x.BestellingenBestellingId,
                        principalTable: "Bestellings",
                        principalColumn: "BestellingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BestellingGerecht_Gerechten_GerechtenGerechtId",
                        column: x => x.GerechtenGerechtId,
                        principalTable: "Gerechten",
                        principalColumn: "GerechtId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BestellingGerecht_GerechtenGerechtId",
                table: "BestellingGerecht",
                column: "GerechtenGerechtId");

            migrationBuilder.CreateIndex(
                name: "IX_Bestellings_ChefId",
                table: "Bestellings",
                column: "ChefId");

            migrationBuilder.CreateIndex(
                name: "IX_Bestellings_KlantId",
                table: "Bestellings",
                column: "KlantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BestellingGerecht");

            migrationBuilder.DropTable(
                name: "Bestellings");

            migrationBuilder.DropTable(
                name: "Gerechten");

            migrationBuilder.DropTable(
                name: "Chefs");

            migrationBuilder.DropTable(
                name: "Klants");
        }
    }
}
