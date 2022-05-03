using Microsoft.EntityFrameworkCore.Migrations;

namespace PFEmvc.Migrations
{
    public partial class relationdatacriterias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DataId",
                table: "Criterias",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Criterias_DataId",
                table: "Criterias",
                column: "DataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Criterias_Data_DataId",
                table: "Criterias",
                column: "DataId",
                principalTable: "Data",
                principalColumn: "DataId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Criterias_Data_DataId",
                table: "Criterias");

            migrationBuilder.DropIndex(
                name: "IX_Criterias_DataId",
                table: "Criterias");

            migrationBuilder.DropColumn(
                name: "DataId",
                table: "Criterias");
        }
    }
}
