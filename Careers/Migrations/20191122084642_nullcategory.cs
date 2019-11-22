using Microsoft.EntityFrameworkCore.Migrations;

namespace Careers.Migrations
{
    public partial class nullcategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_Categories_CategoryId",
                table: "Question");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Question",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Categories_CategoryId",
                table: "Question",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_Categories_CategoryId",
                table: "Question");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Question",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Categories_CategoryId",
                table: "Question",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
