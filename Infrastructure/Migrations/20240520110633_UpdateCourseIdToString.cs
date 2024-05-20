using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class UpdateCourseIdToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "SavedCourses");

            migrationBuilder.AddColumn<string>(
                name: "CourseId",
                table: "SavedCourses",
                type: "nvarchar(max)",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "SavedCourses");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "SavedCourses",
                type: "int",
                nullable: false);
        }
    }
}
