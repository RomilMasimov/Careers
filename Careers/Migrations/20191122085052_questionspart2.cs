using Microsoft.EntityFrameworkCore.Migrations;

namespace Careers.Migrations
{
    public partial class questionspart2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NextQuestionId",
                table: "Answer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answer_NextQuestionId",
                table: "Answer",
                column: "NextQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Question_NextQuestionId",
                table: "Answer",
                column: "NextQuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Question_NextQuestionId",
                table: "Answer");

            migrationBuilder.DropIndex(
                name: "IX_Answer_NextQuestionId",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "NextQuestionId",
                table: "Answer");
        }
    }
}
