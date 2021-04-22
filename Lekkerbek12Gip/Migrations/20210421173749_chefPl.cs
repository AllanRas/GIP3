using Microsoft.EntityFrameworkCore.Migrations;

namespace Lekkerbek12Gip.Migrations
{
    public partial class chefPl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlanningsModuleId1",
                table: "ChefPlanningsModules",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChefPlanningsModules_PlanningsModuleId1",
                table: "ChefPlanningsModules",
                column: "PlanningsModuleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ChefPlanningsModules_PlanningsModules_PlanningsModuleId1",
                table: "ChefPlanningsModules",
                column: "PlanningsModuleId1",
                principalTable: "PlanningsModules",
                principalColumn: "PlanningsModuleId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChefPlanningsModules_PlanningsModules_PlanningsModuleId1",
                table: "ChefPlanningsModules");

            migrationBuilder.DropIndex(
                name: "IX_ChefPlanningsModules_PlanningsModuleId1",
                table: "ChefPlanningsModules");

            migrationBuilder.DropColumn(
                name: "PlanningsModuleId1",
                table: "ChefPlanningsModules");
        }
    }
}
