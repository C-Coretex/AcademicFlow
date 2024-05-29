using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademicFlow.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class FixCourseIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Course_PublicId",
                table: "Course");

            migrationBuilder.CreateIndex(
                name: "IX_Course_PublicId",
                table: "Course",
                column: "PublicId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Course_PublicId",
                table: "Course");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Course_PublicId",
                table: "Course",
                column: "PublicId");
        }
    }
}
