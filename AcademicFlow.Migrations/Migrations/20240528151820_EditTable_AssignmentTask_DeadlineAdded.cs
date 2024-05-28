using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademicFlow.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class EditTable_AssignmentTask_DeadlineAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Deadline",
                table: "AssignmentTask",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentTask_Deadline",
                table: "AssignmentTask",
                column: "Deadline");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AssignmentTask_Deadline",
                table: "AssignmentTask");

            migrationBuilder.DropColumn(
                name: "Deadline",
                table: "AssignmentTask");
        }
    }
}
