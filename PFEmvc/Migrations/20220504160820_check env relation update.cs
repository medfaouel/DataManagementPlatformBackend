using Microsoft.EntityFrameworkCore.Migrations;

namespace PFEmvc.Migrations
{
    public partial class checkenvrelationupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checks_Environments_environmentEnvId",
                table: "Checks");

            migrationBuilder.DropIndex(
                name: "IX_Checks_environmentEnvId",
                table: "Checks");

            migrationBuilder.DropColumn(
                name: "environmentEnvId",
                table: "Checks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "environmentEnvId",
                table: "Checks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Checks_environmentEnvId",
                table: "Checks",
                column: "environmentEnvId");

            migrationBuilder.AddForeignKey(
                name: "FK_Checks_Environments_environmentEnvId",
                table: "Checks",
                column: "environmentEnvId",
                principalTable: "Environments",
                principalColumn: "EnvId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
