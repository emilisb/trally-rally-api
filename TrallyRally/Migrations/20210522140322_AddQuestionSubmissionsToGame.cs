using Microsoft.EntityFrameworkCore.Migrations;

namespace TrallyRally.Migrations
{
    public partial class AddQuestionSubmissionsToGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_QuestionSubmissions_GameID",
                table: "QuestionSubmissions",
                column: "GameID");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionSubmissions_Games_GameID",
                table: "QuestionSubmissions",
                column: "GameID",
                principalTable: "Games",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionSubmissions_Games_GameID",
                table: "QuestionSubmissions");

            migrationBuilder.DropIndex(
                name: "IX_QuestionSubmissions_GameID",
                table: "QuestionSubmissions");
        }
    }
}
