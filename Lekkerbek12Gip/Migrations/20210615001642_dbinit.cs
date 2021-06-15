using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lekkerbek12Gip.Migrations
{
    public partial class dbinit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Chefs",
                columns: table => new
                {
                    ChefId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChefName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chefs", x => x.ChefId);
                });

            migrationBuilder.CreateTable(
                name: "Klants",
                columns: table => new
                {
                    KlantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GetrouwheidsScore = table.Column<int>(type: "int", nullable: false),
                    Geboortedatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    emailadres = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klants", x => x.KlantId);
                });

            migrationBuilder.CreateTable(
                name: "PlanningsModules",
                columns: table => new
                {
                    PlanningsModuleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChefId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpeningsUren = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanningsModules", x => x.PlanningsModuleId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dranken",
                columns: table => new
                {
                    DrankId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    Prijs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Omschrijving = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dranken", x => x.DrankId);
                    table.ForeignKey(
                        name: "FK_Dranken_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Gerechten",
                columns: table => new
                {
                    GerechtId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Omschrijving = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prijs = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gerechten", x => x.GerechtId);
                    table.ForeignKey(
                        name: "FK_Gerechten_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Firmas",
                columns: table => new
                {
                    FirmaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirmaNaam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BtwNummer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KlantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firmas", x => x.FirmaId);
                    table.ForeignKey(
                        name: "FK_Firmas_Klants_KlantId",
                        column: x => x.KlantId,
                        principalTable: "Klants",
                        principalColumn: "KlantId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KlantId = table.Column<int>(type: "int", nullable: false),
                    ReviewMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Score = table.Column<int>(type: "int", nullable: false),
                    TimeOfReview = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_Klants_KlantId",
                        column: x => x.KlantId,
                        principalTable: "Klants",
                        principalColumn: "KlantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bestellings",
                columns: table => new
                {
                    BestellingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BestelingStatus = table.Column<int>(type: "int", nullable: false),
                    KlantId = table.Column<int>(type: "int", nullable: false),
                    ChefId = table.Column<int>(type: "int", nullable: true),
                    SpecialeWensen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Afgerekend = table.Column<bool>(type: "bit", nullable: false),
                    AfhaalTijd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Korting = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PlanningsModuleId = table.Column<int>(type: "int", nullable: true)
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bestellings_PlanningsModules_PlanningsModuleId",
                        column: x => x.PlanningsModuleId,
                        principalTable: "PlanningsModules",
                        principalColumn: "PlanningsModuleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChefPlanningsModules",
                columns: table => new
                {
                    ChefId = table.Column<int>(type: "int", nullable: false),
                    PlanningsModuleId = table.Column<int>(type: "int", nullable: false),
                    ChefStatu = table.Column<int>(type: "int", nullable: false),
                    PlanningsModuleId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChefPlanningsModules", x => new { x.PlanningsModuleId, x.ChefId });
                    table.ForeignKey(
                        name: "FK_ChefPlanningsModules_Chefs_ChefId",
                        column: x => x.ChefId,
                        principalTable: "Chefs",
                        principalColumn: "ChefId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChefPlanningsModules_PlanningsModules_PlanningsModuleId",
                        column: x => x.PlanningsModuleId,
                        principalTable: "PlanningsModules",
                        principalColumn: "PlanningsModuleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChefPlanningsModules_PlanningsModules_PlanningsModuleId1",
                        column: x => x.PlanningsModuleId1,
                        principalTable: "PlanningsModules",
                        principalColumn: "PlanningsModuleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlanningsModuleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_Events_PlanningsModules_PlanningsModuleId",
                        column: x => x.PlanningsModuleId,
                        principalTable: "PlanningsModules",
                        principalColumn: "PlanningsModuleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GerechtKlantFavorieten",
                columns: table => new
                {
                    KlantId = table.Column<int>(type: "int", nullable: false),
                    GerechtId = table.Column<int>(type: "int", nullable: false),
                    GerechtId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GerechtKlantFavorieten", x => new { x.GerechtId, x.KlantId });
                    table.ForeignKey(
                        name: "FK_GerechtKlantFavorieten_Gerechten_GerechtId",
                        column: x => x.GerechtId,
                        principalTable: "Gerechten",
                        principalColumn: "GerechtId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GerechtKlantFavorieten_Gerechten_GerechtId1",
                        column: x => x.GerechtId1,
                        principalTable: "Gerechten",
                        principalColumn: "GerechtId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GerechtKlantFavorieten_Klants_KlantId",
                        column: x => x.KlantId,
                        principalTable: "Klants",
                        principalColumn: "KlantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BestellingDranks",
                columns: table => new
                {
                    BestellingId = table.Column<int>(type: "int", nullable: false),
                    DrankId = table.Column<int>(type: "int", nullable: false),
                    Aantal = table.Column<int>(type: "int", nullable: false, defaultValueSql: "0"),
                    BestellingId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BestellingDranks", x => new { x.BestellingId, x.DrankId });
                    table.ForeignKey(
                        name: "FK_BestellingDranks_Bestellings_BestellingId",
                        column: x => x.BestellingId,
                        principalTable: "Bestellings",
                        principalColumn: "BestellingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BestellingDranks_Bestellings_BestellingId1",
                        column: x => x.BestellingId1,
                        principalTable: "Bestellings",
                        principalColumn: "BestellingId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BestellingDranks_Dranken_DrankId",
                        column: x => x.DrankId,
                        principalTable: "Dranken",
                        principalColumn: "DrankId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BestellingGerechten",
                columns: table => new
                {
                    BestellingId = table.Column<int>(type: "int", nullable: false),
                    GerechtId = table.Column<int>(type: "int", nullable: false),
                    Aantal = table.Column<int>(type: "int", nullable: false, defaultValueSql: "0"),
                    BestellingId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BestellingGerechten", x => new { x.BestellingId, x.GerechtId });
                    table.ForeignKey(
                        name: "FK_BestellingGerechten_Bestellings_BestellingId",
                        column: x => x.BestellingId,
                        principalTable: "Bestellings",
                        principalColumn: "BestellingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BestellingGerechten_Bestellings_BestellingId1",
                        column: x => x.BestellingId1,
                        principalTable: "Bestellings",
                        principalColumn: "BestellingId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BestellingGerechten_Gerechten_GerechtId",
                        column: x => x.GerechtId,
                        principalTable: "Gerechten",
                        principalColumn: "GerechtId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BestellingDranks_BestellingId1",
                table: "BestellingDranks",
                column: "BestellingId1");

            migrationBuilder.CreateIndex(
                name: "IX_BestellingDranks_DrankId",
                table: "BestellingDranks",
                column: "DrankId");

            migrationBuilder.CreateIndex(
                name: "IX_BestellingGerechten_BestellingId1",
                table: "BestellingGerechten",
                column: "BestellingId1");

            migrationBuilder.CreateIndex(
                name: "IX_BestellingGerechten_GerechtId",
                table: "BestellingGerechten",
                column: "GerechtId");

            migrationBuilder.CreateIndex(
                name: "IX_Bestellings_ChefId",
                table: "Bestellings",
                column: "ChefId");

            migrationBuilder.CreateIndex(
                name: "IX_Bestellings_KlantId",
                table: "Bestellings",
                column: "KlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Bestellings_PlanningsModuleId",
                table: "Bestellings",
                column: "PlanningsModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_ChefPlanningsModules_ChefId",
                table: "ChefPlanningsModules",
                column: "ChefId");

            migrationBuilder.CreateIndex(
                name: "IX_ChefPlanningsModules_PlanningsModuleId1",
                table: "ChefPlanningsModules",
                column: "PlanningsModuleId1");

            migrationBuilder.CreateIndex(
                name: "IX_Dranken_CategoryId",
                table: "Dranken",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_PlanningsModuleId",
                table: "Events",
                column: "PlanningsModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Firmas_KlantId",
                table: "Firmas",
                column: "KlantId",
                unique: true,
                filter: "[KlantId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Gerechten_CategoryId",
                table: "Gerechten",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GerechtKlantFavorieten_GerechtId1",
                table: "GerechtKlantFavorieten",
                column: "GerechtId1");

            migrationBuilder.CreateIndex(
                name: "IX_GerechtKlantFavorieten_KlantId",
                table: "GerechtKlantFavorieten",
                column: "KlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_KlantId",
                table: "Reviews",
                column: "KlantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BestellingDranks");

            migrationBuilder.DropTable(
                name: "BestellingGerechten");

            migrationBuilder.DropTable(
                name: "ChefPlanningsModules");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Firmas");

            migrationBuilder.DropTable(
                name: "GerechtKlantFavorieten");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Dranken");

            migrationBuilder.DropTable(
                name: "Bestellings");

            migrationBuilder.DropTable(
                name: "Gerechten");

            migrationBuilder.DropTable(
                name: "Chefs");

            migrationBuilder.DropTable(
                name: "Klants");

            migrationBuilder.DropTable(
                name: "PlanningsModules");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
