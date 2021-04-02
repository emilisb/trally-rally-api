using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrallyRally.Migrations
{
    public partial class CreatePlayersAndSubmissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Phone = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Avatar = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    StartPosition = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "QuestionSubmission",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    QuestionID = table.Column<int>(type: "int", nullable: false),
                    PlayerID = table.Column<int>(type: "int", nullable: false),
                    Answer = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    SubmissionTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionSubmission", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QuestionSubmission_Players_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Players",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionSubmission_Questions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionSubmission_PlayerID",
                table: "QuestionSubmission",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionSubmission_QuestionID",
                table: "QuestionSubmission",
                column: "QuestionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionSubmission");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
