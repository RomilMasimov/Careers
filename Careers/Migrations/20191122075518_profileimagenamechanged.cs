using Microsoft.EntityFrameworkCore.Migrations;

namespace Careers.Migrations
{
    public partial class profileimagenamechanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_MediaPaths_MediaPathId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_MediaPathId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "MediaPathId",
                table: "Persons");

            migrationBuilder.AddColumn<int>(
                name: "ProfileImageId",
                table: "Persons",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProfileImagePathId",
                table: "Persons",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_ProfileImagePathId",
                table: "Persons",
                column: "ProfileImagePathId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_MediaPaths_ProfileImagePathId",
                table: "Persons",
                column: "ProfileImagePathId",
                principalTable: "MediaPaths",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_MediaPaths_ProfileImagePathId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_ProfileImagePathId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "ProfileImageId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "ProfileImagePathId",
                table: "Persons");

            migrationBuilder.AddColumn<int>(
                name: "MediaPathId",
                table: "Persons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_MediaPathId",
                table: "Persons",
                column: "MediaPathId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_MediaPaths_MediaPathId",
                table: "Persons",
                column: "MediaPathId",
                principalTable: "MediaPaths",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
