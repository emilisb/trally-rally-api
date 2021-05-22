using Microsoft.EntityFrameworkCore.Migrations;

namespace TrallyRally.Migrations
{
    public partial class MakeSubmissionCorrectNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Correct",
                table: "QuestionSubmissions",
                type: "tinyint(1)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Correct",
                table: "QuestionSubmissions",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldNullable: true);
        }
    }
}
