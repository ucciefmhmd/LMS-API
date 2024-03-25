using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateEntityQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "chooses",
                table: "Questions",
                newName: "chooseTwo");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "chooseTwo",
                table: "Questions",
                newName: "chooses");
        }
    }
}
