using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrallyRally.Migrations
{
    public partial class UpdateDateFieldsInQuestionSubmissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubmissionTime",
                table: "QuestionSubmissions",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "LastUpdated",
                table: "Games",
                newName: "ModifiedDate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "QuestionSubmissions",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "QuestionSubmissions",
                type: "datetime(6)",
                nullable: false)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "QuestionSubmissions");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "QuestionSubmissions",
                newName: "SubmissionTime");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "Games",
                newName: "LastUpdated");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubmissionTime",
                table: "QuestionSubmissions",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);
        }
    }
}
