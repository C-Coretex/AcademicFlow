using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademicFlow.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTable_CourseForeignKeyCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentTask_Course_CourseId",
                table: "AssignmentTask",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentTask_Course_CourseId",
                table: "AssignmentTask");
        }
    }
}
