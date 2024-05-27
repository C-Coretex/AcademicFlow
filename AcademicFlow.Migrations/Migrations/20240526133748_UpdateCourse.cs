using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademicFlow.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Course",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "Course",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Course_PublicId",
                table: "Course",
                column: "PublicId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Course_PublicId",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "Course");
        }
    }
}
