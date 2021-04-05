using Microsoft.EntityFrameworkCore.Migrations;

namespace TrallyRally.Migrations
{
    public partial class AddProperQuestionSubmissionsRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionSubmission_Players_PlayerID",
                table: "QuestionSubmission");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionSubmission_Questions_QuestionID",
                table: "QuestionSubmission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionSubmission",
                table: "QuestionSubmission");

            migrationBuilder.RenameTable(
                name: "QuestionSubmission",
                newName: "QuestionSubmissions");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionSubmission_QuestionID",
                table: "QuestionSubmissions",
                newName: "IX_QuestionSubmissions_QuestionID");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionSubmission_PlayerID",
                table: "QuestionSubmissions",
                newName: "IX_QuestionSubmissions_PlayerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionSubmissions",
                table: "QuestionSubmissions",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionSubmissions_Players_PlayerID",
                table: "QuestionSubmissions",
                column: "PlayerID",
                principalTable: "Players",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionSubmissions_Questions_QuestionID",
                table: "QuestionSubmissions",
                column: "QuestionID",
                principalTable: "Questions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionSubmissions_Players_PlayerID",
                table: "QuestionSubmissions");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionSubmissions_Questions_QuestionID",
                table: "QuestionSubmissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionSubmissions",
                table: "QuestionSubmissions");

            migrationBuilder.RenameTable(
                name: "QuestionSubmissions",
                newName: "QuestionSubmission");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionSubmissions_QuestionID",
                table: "QuestionSubmission",
                newName: "IX_QuestionSubmission_QuestionID");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionSubmissions_PlayerID",
                table: "QuestionSubmission",
                newName: "IX_QuestionSubmission_PlayerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionSubmission",
                table: "QuestionSubmission",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionSubmission_Players_PlayerID",
                table: "QuestionSubmission",
                column: "PlayerID",
                principalTable: "Players",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionSubmission_Questions_QuestionID",
                table: "QuestionSubmission",
                column: "QuestionID",
                principalTable: "Questions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
