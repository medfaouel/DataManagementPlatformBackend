using Microsoft.EntityFrameworkCore.Migrations;

namespace PFEmvc.Migrations
{
    public partial class reverse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Criterias_CheckDetails_CheckDetailsId",
                table: "Criterias");

            migrationBuilder.DropIndex(
                name: "IX_Criterias_CheckDetailsId",
                table: "Criterias");

            migrationBuilder.DropColumn(
                name: "CheckDetailsId",
                table: "Criterias");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CheckDetailsId",
                table: "Criterias",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Criterias_CheckDetailsId",
                table: "Criterias",
                column: "CheckDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Criterias_CheckDetails_CheckDetailsId",
                table: "Criterias",
                column: "CheckDetailsId",
                principalTable: "CheckDetails",
                principalColumn: "CheckDetailId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
