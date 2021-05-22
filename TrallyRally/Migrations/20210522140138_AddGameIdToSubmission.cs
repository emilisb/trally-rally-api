using Microsoft.EntityFrameworkCore.Migrations;

namespace TrallyRally.Migrations
{
    public partial class AddGameIdToSubmission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameID",
                table: "QuestionSubmissions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameID",
                table: "QuestionSubmissions");
        }
    }
}
