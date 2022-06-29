using Microsoft.EntityFrameworkCore.Migrations;

namespace PFEmvc.Migrations
{
    public partial class teamandchecksrelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Checks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Checks_TeamId",
                table: "Checks",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Checks_Teams_TeamId",
                table: "Checks",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checks_Teams_TeamId",
                table: "Checks");

            migrationBuilder.DropIndex(
                name: "IX_Checks_TeamId",
                table: "Checks");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Checks");
        }
    }
}
