using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addEntityChooseQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "chooseFour",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "chooseOne",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "chooseThree",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "chooseTwo",
                table: "Questions");

            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "Exam",
                type: "int",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "Time",
                table: "Exam",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.CreateTable(
                name: "ChooseQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Choose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ques_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChooseQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChooseQuestion_Questions_Ques_ID",
                        column: x => x.Ques_ID,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChooseQuestion_Ques_ID",
                table: "ChooseQuestion",
                column: "Ques_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChooseQuestion");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Exam");

            migrationBuilder.AddColumn<string>(
                name: "chooseFour",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "chooseOne",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "chooseThree",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "chooseTwo",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "Duration",
                table: "Exam",
                type: "time",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
