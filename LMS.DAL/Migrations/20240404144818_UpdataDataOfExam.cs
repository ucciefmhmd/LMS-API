using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdataDataOfExam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exam_Courses_Course_ID",
                table: "Exam");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Users_userID",
                table: "Instructors");

            migrationBuilder.AddForeignKey(
                name: "FK_Exam_Courses_Course_ID",
                table: "Exam",
                column: "Course_ID",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_Users_userID",
                table: "Instructors",
                column: "userID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exam_Courses_Course_ID",
                table: "Exam");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Users_userID",
                table: "Instructors");

            migrationBuilder.AddForeignKey(
                name: "FK_Exam_Courses_Course_ID",
                table: "Exam",
                column: "Course_ID",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_Users_userID",
                table: "Instructors",
                column: "userID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
