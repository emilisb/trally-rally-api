using Microsoft.EntityFrameworkCore.Migrations;

namespace TrallyRally.Migrations
{
    public partial class AddCorrectColumnToSubmissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Correct",
                table: "QuestionSubmissions",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Correct",
                table: "QuestionSubmissions");
        }
    }
}
