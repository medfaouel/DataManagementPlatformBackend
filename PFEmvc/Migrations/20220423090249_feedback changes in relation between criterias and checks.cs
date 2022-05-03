using Microsoft.EntityFrameworkCore.Migrations;

namespace PFEmvc.Migrations
{
    public partial class feedbackchangesinrelationbetweencriteriasandchecks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CDQM_comments",
                table: "Criterias");

            migrationBuilder.DropColumn(
                name: "CDQM_feedback",
                table: "Criterias");

            migrationBuilder.DropColumn(
                name: "DQMS_feedback",
                table: "Criterias");

            migrationBuilder.DropColumn(
                name: "TopicOwner_feedback",
                table: "Criterias");

            migrationBuilder.AddColumn<string>(
                name: "CDQM_comments",
                table: "Checks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CDQM_feedback",
                table: "Checks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DQMS_feedback",
                table: "Checks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TopicOwner_feedback",
                table: "Checks",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CDQM_comments",
                table: "Checks");

            migrationBuilder.DropColumn(
                name: "CDQM_feedback",
                table: "Checks");

            migrationBuilder.DropColumn(
                name: "DQMS_feedback",
                table: "Checks");

            migrationBuilder.DropColumn(
                name: "TopicOwner_feedback",
                table: "Checks");

            migrationBuilder.AddColumn<string>(
                name: "CDQM_comments",
                table: "Criterias",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CDQM_feedback",
                table: "Criterias",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DQMS_feedback",
                table: "Criterias",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TopicOwner_feedback",
                table: "Criterias",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
