using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Careers.Migrations
{
    public partial class reviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Question_AskedQuestionId",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Question_NextQuestionId",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_MediaPaths_ProfileImagePathId",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_Categories_CategoryId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_WhereCanGoSpecialist_MeetingPoints_SpecialistId",
                table: "WhereCanGoSpecialist");

            migrationBuilder.DropForeignKey(
                name: "FK_WhereCanGoSpecialist_Specialists_SpecialistId1",
                table: "WhereCanGoSpecialist");

            migrationBuilder.DropForeignKey(
                name: "FK_WhereCanMeetSpecialist_Specialists_SpecialistId",
                table: "WhereCanMeetSpecialist");

            migrationBuilder.DropForeignKey(
                name: "FK_WhereCanMeetSpecialist_MeetingPoints_WhereCanMeetId",
                table: "WhereCanMeetSpecialist");

            migrationBuilder.DropTable(
                name: "MediaPaths");

            migrationBuilder.DropIndex(
                name: "IX_Persons_ProfileImagePathId",
                table: "Persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WhereCanMeetSpecialist",
                table: "WhereCanMeetSpecialist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WhereCanGoSpecialist",
                table: "WhereCanGoSpecialist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Question",
                table: "Question");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Answer",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "ProfileImageId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "ProfileImagePathId",
                table: "Persons");

            migrationBuilder.RenameTable(
                name: "WhereCanMeetSpecialist",
                newName: "WhereCanMeetSpecialists");

            migrationBuilder.RenameTable(
                name: "WhereCanGoSpecialist",
                newName: "WhereCanGoSpecialists");

            migrationBuilder.RenameTable(
                name: "Question",
                newName: "Questions");

            migrationBuilder.RenameTable(
                name: "Answer",
                newName: "Answers");

            migrationBuilder.RenameIndex(
                name: "IX_WhereCanMeetSpecialist_WhereCanMeetId",
                table: "WhereCanMeetSpecialists",
                newName: "IX_WhereCanMeetSpecialists_WhereCanMeetId");

            migrationBuilder.RenameIndex(
                name: "IX_WhereCanMeetSpecialist_SpecialistId",
                table: "WhereCanMeetSpecialists",
                newName: "IX_WhereCanMeetSpecialists_SpecialistId");

            migrationBuilder.RenameIndex(
                name: "IX_WhereCanGoSpecialist_SpecialistId1",
                table: "WhereCanGoSpecialists",
                newName: "IX_WhereCanGoSpecialists_SpecialistId1");

            migrationBuilder.RenameIndex(
                name: "IX_WhereCanGoSpecialist_SpecialistId",
                table: "WhereCanGoSpecialists",
                newName: "IX_WhereCanGoSpecialists_SpecialistId");

            migrationBuilder.RenameIndex(
                name: "IX_Question_CategoryId",
                table: "Questions",
                newName: "IX_Questions_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Answer_NextQuestionId",
                table: "Answers",
                newName: "IX_Answers_NextQuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Answer_AskedQuestionId",
                table: "Answers",
                newName: "IX_Answers_AskedQuestionId");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Specialists",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProfileImagePath",
                table: "Persons",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Answers",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WhereCanMeetSpecialists",
                table: "WhereCanMeetSpecialists",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WhereCanGoSpecialists",
                table: "WhereCanGoSpecialists",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Questions",
                table: "Questions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Answers",
                table: "Answers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    SpecialistId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Specialists_SpecialistId",
                        column: x => x.SpecialistId,
                        principalTable: "Specialists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderMeetingPoints",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(nullable: false),
                    MeetingPointId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderMeetingPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderMeetingPoints_MeetingPoints_MeetingPointId",
                        column: x => x.MeetingPointId,
                        principalTable: "MeetingPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderMeetingPoints_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mark = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    DateTime = table.Column<DateTime>(nullable: false),
                    LikeCount = table.Column<int>(nullable: false),
                    DisLikeCount = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    ServiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Reviews_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReviewMedias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(nullable: true),
                    ReviewId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewMedias_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Specialists_CityId",
                table: "Specialists",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_OrderId",
                table: "Answers",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderMeetingPoints_MeetingPointId",
                table: "OrderMeetingPoints",
                column: "MeetingPointId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderMeetingPoints_OrderId",
                table: "OrderMeetingPoints",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ClientId",
                table: "Orders",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SpecialistId",
                table: "Orders",
                column: "SpecialistId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewMedias_ReviewId",
                table: "ReviewMedias",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ClientId",
                table: "Reviews",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_OrderId",
                table: "Reviews",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ServiceId",
                table: "Reviews",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_AskedQuestionId",
                table: "Answers",
                column: "AskedQuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_NextQuestionId",
                table: "Answers",
                column: "NextQuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Orders_OrderId",
                table: "Answers",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Categories_CategoryId",
                table: "Questions",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Specialists_Cities_CityId",
                table: "Specialists",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WhereCanGoSpecialists_MeetingPoints_SpecialistId",
                table: "WhereCanGoSpecialists",
                column: "SpecialistId",
                principalTable: "MeetingPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WhereCanGoSpecialists_Specialists_SpecialistId1",
                table: "WhereCanGoSpecialists",
                column: "SpecialistId1",
                principalTable: "Specialists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WhereCanMeetSpecialists_Specialists_SpecialistId",
                table: "WhereCanMeetSpecialists",
                column: "SpecialistId",
                principalTable: "Specialists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WhereCanMeetSpecialists_MeetingPoints_WhereCanMeetId",
                table: "WhereCanMeetSpecialists",
                column: "WhereCanMeetId",
                principalTable: "MeetingPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_AskedQuestionId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_NextQuestionId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Orders_OrderId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Categories_CategoryId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Specialists_Cities_CityId",
                table: "Specialists");

            migrationBuilder.DropForeignKey(
                name: "FK_WhereCanGoSpecialists_MeetingPoints_SpecialistId",
                table: "WhereCanGoSpecialists");

            migrationBuilder.DropForeignKey(
                name: "FK_WhereCanGoSpecialists_Specialists_SpecialistId1",
                table: "WhereCanGoSpecialists");

            migrationBuilder.DropForeignKey(
                name: "FK_WhereCanMeetSpecialists_Specialists_SpecialistId",
                table: "WhereCanMeetSpecialists");

            migrationBuilder.DropForeignKey(
                name: "FK_WhereCanMeetSpecialists_MeetingPoints_WhereCanMeetId",
                table: "WhereCanMeetSpecialists");

            migrationBuilder.DropTable(
                name: "OrderMeetingPoints");

            migrationBuilder.DropTable(
                name: "ReviewMedias");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Specialists_CityId",
                table: "Specialists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WhereCanMeetSpecialists",
                table: "WhereCanMeetSpecialists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WhereCanGoSpecialists",
                table: "WhereCanGoSpecialists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Questions",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Answers",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_OrderId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Specialists");

            migrationBuilder.DropColumn(
                name: "ProfileImagePath",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Answers");

            migrationBuilder.RenameTable(
                name: "WhereCanMeetSpecialists",
                newName: "WhereCanMeetSpecialist");

            migrationBuilder.RenameTable(
                name: "WhereCanGoSpecialists",
                newName: "WhereCanGoSpecialist");

            migrationBuilder.RenameTable(
                name: "Questions",
                newName: "Question");

            migrationBuilder.RenameTable(
                name: "Answers",
                newName: "Answer");

            migrationBuilder.RenameIndex(
                name: "IX_WhereCanMeetSpecialists_WhereCanMeetId",
                table: "WhereCanMeetSpecialist",
                newName: "IX_WhereCanMeetSpecialist_WhereCanMeetId");

            migrationBuilder.RenameIndex(
                name: "IX_WhereCanMeetSpecialists_SpecialistId",
                table: "WhereCanMeetSpecialist",
                newName: "IX_WhereCanMeetSpecialist_SpecialistId");

            migrationBuilder.RenameIndex(
                name: "IX_WhereCanGoSpecialists_SpecialistId1",
                table: "WhereCanGoSpecialist",
                newName: "IX_WhereCanGoSpecialist_SpecialistId1");

            migrationBuilder.RenameIndex(
                name: "IX_WhereCanGoSpecialists_SpecialistId",
                table: "WhereCanGoSpecialist",
                newName: "IX_WhereCanGoSpecialist_SpecialistId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_CategoryId",
                table: "Question",
                newName: "IX_Question_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Answers_NextQuestionId",
                table: "Answer",
                newName: "IX_Answer_NextQuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Answers_AskedQuestionId",
                table: "Answer",
                newName: "IX_Answer_AskedQuestionId");

            migrationBuilder.AddColumn<int>(
                name: "ProfileImageId",
                table: "Persons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProfileImagePathId",
                table: "Persons",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WhereCanMeetSpecialist",
                table: "WhereCanMeetSpecialist",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WhereCanGoSpecialist",
                table: "WhereCanGoSpecialist",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Question",
                table: "Question",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Answer",
                table: "Answer",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MediaPaths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaPaths", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persons_ProfileImagePathId",
                table: "Persons",
                column: "ProfileImagePathId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Question_AskedQuestionId",
                table: "Answer",
                column: "AskedQuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Question_NextQuestionId",
                table: "Answer",
                column: "NextQuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_MediaPaths_ProfileImagePathId",
                table: "Persons",
                column: "ProfileImagePathId",
                principalTable: "MediaPaths",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Categories_CategoryId",
                table: "Question",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WhereCanGoSpecialist_MeetingPoints_SpecialistId",
                table: "WhereCanGoSpecialist",
                column: "SpecialistId",
                principalTable: "MeetingPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WhereCanGoSpecialist_Specialists_SpecialistId1",
                table: "WhereCanGoSpecialist",
                column: "SpecialistId1",
                principalTable: "Specialists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WhereCanMeetSpecialist_Specialists_SpecialistId",
                table: "WhereCanMeetSpecialist",
                column: "SpecialistId",
                principalTable: "Specialists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WhereCanMeetSpecialist_MeetingPoints_WhereCanMeetId",
                table: "WhereCanMeetSpecialist",
                column: "WhereCanMeetId",
                principalTable: "MeetingPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
