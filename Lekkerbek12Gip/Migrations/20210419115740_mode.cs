using Microsoft.EntityFrameworkCore.Migrations;

namespace Lekkerbek12Gip.Migrations
{
    public partial class mode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChefPlanningsModule");

            migrationBuilder.CreateTable(
                name: "ChefPlanningsModules",
                columns: table => new
                {
                    ChefId = table.Column<int>(type: "int", nullable: false),
                    PlanningsModuleId = table.Column<int>(type: "int", nullable: false),
                    ChefStatu = table.Column<int>(type: "int", nullable: false)
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
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChefPlanningsModules_ChefId",
                table: "ChefPlanningsModules",
                column: "ChefId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChefPlanningsModules");

            migrationBuilder.CreateTable(
                name: "ChefPlanningsModule",
                columns: table => new
                {
                    PlanningsModuleId = table.Column<int>(type: "int", nullable: false),
                    chefsChefId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChefPlanningsModule", x => new { x.PlanningsModuleId, x.chefsChefId });
                    table.ForeignKey(
                        name: "FK_ChefPlanningsModule_Chefs_chefsChefId",
                        column: x => x.chefsChefId,
                        principalTable: "Chefs",
                        principalColumn: "ChefId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChefPlanningsModule_PlanningsModules_PlanningsModuleId",
                        column: x => x.PlanningsModuleId,
                        principalTable: "PlanningsModules",
                        principalColumn: "PlanningsModuleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChefPlanningsModule_chefsChefId",
                table: "ChefPlanningsModule",
                column: "chefsChefId");
        }
    }
}
