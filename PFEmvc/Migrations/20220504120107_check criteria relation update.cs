using Microsoft.EntityFrameworkCore.Migrations;

namespace PFEmvc.Migrations
{
    public partial class checkcriteriarelationupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Criterias_Checks_CheckId",
                table: "Criterias");

            migrationBuilder.DropIndex(
                name: "IX_Criterias_CheckId",
                table: "Criterias");

            migrationBuilder.DropColumn(
                name: "CheckId",
                table: "Criterias");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CheckId",
                table: "Criterias",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Criterias_CheckId",
                table: "Criterias",
                column: "CheckId");

            migrationBuilder.AddForeignKey(
                name: "FK_Criterias_Checks_CheckId",
                table: "Criterias",
                column: "CheckId",
                principalTable: "Checks",
                principalColumn: "CheckId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
