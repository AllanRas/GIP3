using Microsoft.EntityFrameworkCore.Migrations;

namespace Lekkerbek12Gip.Migrations
{
    public partial class mod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chefs_PlanningsModules_PlanningsModuleId",
                table: "Chefs");

            migrationBuilder.DropIndex(
                name: "IX_Chefs_PlanningsModuleId",
                table: "Chefs");

            migrationBuilder.DropColumn(
                name: "PlanningsModuleId",
                table: "Chefs");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChefPlanningsModule");

            migrationBuilder.AddColumn<int>(
                name: "PlanningsModuleId",
                table: "Chefs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chefs_PlanningsModuleId",
                table: "Chefs",
                column: "PlanningsModuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chefs_PlanningsModules_PlanningsModuleId",
                table: "Chefs",
                column: "PlanningsModuleId",
                principalTable: "PlanningsModules",
                principalColumn: "PlanningsModuleId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
