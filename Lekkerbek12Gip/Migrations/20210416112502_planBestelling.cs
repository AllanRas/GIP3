using Microsoft.EntityFrameworkCore.Migrations;

namespace Lekkerbek12Gip.Migrations
{
    public partial class planBestelling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlanningsModuleId",
                table: "Bestellings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bestellings_PlanningsModuleId",
                table: "Bestellings",
                column: "PlanningsModuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bestellings_PlanningsModules_PlanningsModuleId",
                table: "Bestellings",
                column: "PlanningsModuleId",
                principalTable: "PlanningsModules",
                principalColumn: "PlanningsModuleId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bestellings_PlanningsModules_PlanningsModuleId",
                table: "Bestellings");

            migrationBuilder.DropIndex(
                name: "IX_Bestellings_PlanningsModuleId",
                table: "Bestellings");

            migrationBuilder.DropColumn(
                name: "PlanningsModuleId",
                table: "Bestellings");
        }
    }
}
