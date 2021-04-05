using Microsoft.EntityFrameworkCore.Migrations;

namespace Lekkerbek12Gip.Migrations
{
    public partial class addmigratonpl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlanningsModuleId",
                table: "PlanningsModules",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "PlanningsModuleId",
                table: "Chefs",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanningsModules",
                table: "PlanningsModules",
                column: "PlanningsModuleId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chefs_PlanningsModules_PlanningsModuleId",
                table: "Chefs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlanningsModules",
                table: "PlanningsModules");

            migrationBuilder.DropIndex(
                name: "IX_Chefs_PlanningsModuleId",
                table: "Chefs");

            migrationBuilder.DropColumn(
                name: "PlanningsModuleId",
                table: "PlanningsModules");

            migrationBuilder.DropColumn(
                name: "PlanningsModuleId",
                table: "Chefs");
        }
    }
}
