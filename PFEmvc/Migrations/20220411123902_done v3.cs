using Microsoft.EntityFrameworkCore.Migrations;

namespace PFEmvc.Migrations
{
    public partial class donev3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checks_Data_DataId",
                table: "Checks");

            migrationBuilder.RenameColumn(
                name: "DataId",
                table: "Checks",
                newName: "DataIdentity");

            migrationBuilder.RenameIndex(
                name: "IX_Checks_DataId",
                table: "Checks",
                newName: "IX_Checks_DataIdentity");

            migrationBuilder.AddForeignKey(
                name: "FK_Checks_Data_DataIdentity",
                table: "Checks",
                column: "DataIdentity",
                principalTable: "Data",
                principalColumn: "DataId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checks_Data_DataIdentity",
                table: "Checks");

            migrationBuilder.RenameColumn(
                name: "DataIdentity",
                table: "Checks",
                newName: "DataId");

            migrationBuilder.RenameIndex(
                name: "IX_Checks_DataIdentity",
                table: "Checks",
                newName: "IX_Checks_DataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Checks_Data_DataId",
                table: "Checks",
                column: "DataId",
                principalTable: "Data",
                principalColumn: "DataId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
