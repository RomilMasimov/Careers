using Microsoft.EntityFrameworkCore.Migrations;

namespace Careers.Migrations
{
    public partial class meetpoints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WhereCanGoSpecialist",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WhereCanGoId = table.Column<int>(nullable: false),
                    SpecialistId1 = table.Column<int>(nullable: false),
                    SpecialistId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WhereCanGoSpecialist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WhereCanGoSpecialist_MeetingPoints_SpecialistId",
                        column: x => x.SpecialistId,
                        principalTable: "MeetingPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WhereCanGoSpecialist_Specialists_SpecialistId1",
                        column: x => x.SpecialistId1,
                        principalTable: "Specialists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WhereCanMeetSpecialist",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WhereCanMeetId = table.Column<int>(nullable: false),
                    SpecialistId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WhereCanMeetSpecialist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WhereCanMeetSpecialist_Specialists_SpecialistId",
                        column: x => x.SpecialistId,
                        principalTable: "Specialists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WhereCanMeetSpecialist_MeetingPoints_WhereCanMeetId",
                        column: x => x.WhereCanMeetId,
                        principalTable: "MeetingPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WhereCanGoSpecialist_SpecialistId",
                table: "WhereCanGoSpecialist",
                column: "SpecialistId");

            migrationBuilder.CreateIndex(
                name: "IX_WhereCanGoSpecialist_SpecialistId1",
                table: "WhereCanGoSpecialist",
                column: "SpecialistId1");

            migrationBuilder.CreateIndex(
                name: "IX_WhereCanMeetSpecialist_SpecialistId",
                table: "WhereCanMeetSpecialist",
                column: "SpecialistId");

            migrationBuilder.CreateIndex(
                name: "IX_WhereCanMeetSpecialist_WhereCanMeetId",
                table: "WhereCanMeetSpecialist",
                column: "WhereCanMeetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WhereCanGoSpecialist");

            migrationBuilder.DropTable(
                name: "WhereCanMeetSpecialist");
        }
    }
}
