using Microsoft.EntityFrameworkCore.Migrations;

namespace PFEmvc.Migrations
{
    public partial class donev4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checks_Data_DataIdentity",
                table: "Checks");

            migrationBuilder.DropIndex(
                name: "IX_Checks_DataIdentity",
                table: "Checks");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_Checks_DataIdentity",
                table: "Checks",
                column: "DataIdentity",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Checks_Data_DataIdentity",
                table: "Checks",
                column: "DataIdentity",
                principalTable: "Data",
                principalColumn: "DataId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
