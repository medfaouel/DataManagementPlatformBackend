using Microsoft.EntityFrameworkCore.Migrations;

namespace PFEmvc.Migrations
{
    public partial class relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checks_Data_DataId",
                table: "Checks");

            migrationBuilder.DropIndex(
                name: "IX_Checks_DataId",
                table: "Checks");

            migrationBuilder.DropColumn(
                name: "DataId",
                table: "Checks");

            migrationBuilder.AddColumn<int>(
                name: "CheckId",
                table: "Data",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Data_CheckId",
                table: "Data",
                column: "CheckId");

            migrationBuilder.AddForeignKey(
                name: "FK_Data_Checks_CheckId",
                table: "Data",
                column: "CheckId",
                principalTable: "Checks",
                principalColumn: "CheckId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Data_Checks_CheckId",
                table: "Data");

            migrationBuilder.DropIndex(
                name: "IX_Data_CheckId",
                table: "Data");

            migrationBuilder.DropColumn(
                name: "CheckId",
                table: "Data");

            migrationBuilder.AddColumn<int>(
                name: "DataId",
                table: "Checks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Checks_DataId",
                table: "Checks",
                column: "DataId",
                unique: true,
                filter: "[DataId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Checks_Data_DataId",
                table: "Checks",
                column: "DataId",
                principalTable: "Data",
                principalColumn: "DataId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
