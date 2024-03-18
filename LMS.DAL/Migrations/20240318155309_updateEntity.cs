using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoursesInstructors");

            migrationBuilder.DropTable(
                name: "EventsUsers");

            migrationBuilder.DropTable(
                name: "ExamStudents");

            migrationBuilder.DropTable(
                name: "InstructorCourseStudents");

            migrationBuilder.DropTable(
                name: "QuestionsStudents");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoursesInstructors",
                columns: table => new
                {
                    CoursesId = table.Column<int>(type: "int", nullable: false),
                    InstructorsuserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursesInstructors", x => new { x.CoursesId, x.InstructorsuserID });
                    table.ForeignKey(
                        name: "FK_CoursesInstructors_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoursesInstructors_Instructors_InstructorsuserID",
                        column: x => x.InstructorsuserID,
                        principalTable: "Instructors",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventsUsers",
                columns: table => new
                {
                    EventsId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventsUsers", x => new { x.EventsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_EventsUsers_Events_EventsId",
                        column: x => x.EventsId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventsUsers_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamStudents",
                columns: table => new
                {
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    StudentsuserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamStudents", x => new { x.ExamId, x.StudentsuserID });
                    table.ForeignKey(
                        name: "FK_ExamStudents_Exam_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamStudents_Students_StudentsuserID",
                        column: x => x.StudentsuserID,
                        principalTable: "Students",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstructorCourseStudents",
                columns: table => new
                {
                    InstructorCourseId = table.Column<int>(type: "int", nullable: false),
                    StudentsuserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructorCourseStudents", x => new { x.InstructorCourseId, x.StudentsuserID });
                    table.ForeignKey(
                        name: "FK_InstructorCourseStudents_InstructorCourse_InstructorCourseId",
                        column: x => x.InstructorCourseId,
                        principalTable: "InstructorCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstructorCourseStudents_Students_StudentsuserID",
                        column: x => x.StudentsuserID,
                        principalTable: "Students",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionsStudents",
                columns: table => new
                {
                    QuestionsId = table.Column<int>(type: "int", nullable: false),
                    StudentsuserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionsStudents", x => new { x.QuestionsId, x.StudentsuserID });
                    table.ForeignKey(
                        name: "FK_QuestionsStudents_Questions_QuestionsId",
                        column: x => x.QuestionsId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionsStudents_Students_StudentsuserID",
                        column: x => x.StudentsuserID,
                        principalTable: "Students",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoursesInstructors_InstructorsuserID",
                table: "CoursesInstructors",
                column: "InstructorsuserID");

            migrationBuilder.CreateIndex(
                name: "IX_EventsUsers_UsersId",
                table: "EventsUsers",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamStudents_StudentsuserID",
                table: "ExamStudents",
                column: "StudentsuserID");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorCourseStudents_StudentsuserID",
                table: "InstructorCourseStudents",
                column: "StudentsuserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsStudents_StudentsuserID",
                table: "QuestionsStudents",
                column: "StudentsuserID");
        }
    }
}
