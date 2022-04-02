using Microsoft.EntityFrameworkCore.Migrations;

namespace PFEmvc.Migrations
{
    public partial class workerandteams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Team",
                table: "Workers");

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Workers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workers_TeamId",
                table: "Workers",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Teams_TeamId",
                table: "Workers",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Teams_TeamId",
                table: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_Workers_TeamId",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Workers");

            migrationBuilder.AddColumn<string>(
                name: "Team",
                table: "Workers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
