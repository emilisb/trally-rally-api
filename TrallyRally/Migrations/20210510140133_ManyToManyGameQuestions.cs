using Microsoft.EntityFrameworkCore.Migrations;

namespace TrallyRally.Migrations
{
    public partial class ManyToManyGameQuestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Games_GameID",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_GameID",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "GameID",
                table: "Questions");

            migrationBuilder.CreateTable(
                name: "GameQuestion",
                columns: table => new
                {
                    GamesID = table.Column<int>(type: "int", nullable: false),
                    QuestionsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameQuestion", x => new { x.GamesID, x.QuestionsID });
                    table.ForeignKey(
                        name: "FK_GameQuestion_Games_GamesID",
                        column: x => x.GamesID,
                        principalTable: "Games",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameQuestion_Questions_QuestionsID",
                        column: x => x.QuestionsID,
                        principalTable: "Questions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameQuestion_QuestionsID",
                table: "GameQuestion",
                column: "QuestionsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameQuestion");

            migrationBuilder.AddColumn<int>(
                name: "GameID",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_GameID",
                table: "Questions",
                column: "GameID");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Games_GameID",
                table: "Questions",
                column: "GameID",
                principalTable: "Games",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
