using Microsoft.EntityFrameworkCore.Migrations;

namespace PFEmvc.Migrations
{
    public partial class checkdetailscriterias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataIdentity",
                table: "Checks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DataIdentity",
                table: "Checks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
