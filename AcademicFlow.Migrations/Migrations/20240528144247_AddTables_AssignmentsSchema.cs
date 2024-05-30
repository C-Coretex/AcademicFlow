using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademicFlow.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddTables_AssignmentsSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssignmentTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    AssignmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssignmentDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssignmentWeight = table.Column<float>(type: "real", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignmentTask_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AssignmentEntry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignmentTaskId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    AssignmentFilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignmentEntry_AssignmentTask_AssignmentTaskId",
                        column: x => x.AssignmentTaskId,
                        principalTable: "AssignmentTask",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssignmentEntry_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AssignmentGrade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignmentEntryId = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<int>(type: "int", nullable: false),
                    GradedById = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentGrade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignmentGrade_AssignmentEntry_AssignmentEntryId",
                        column: x => x.AssignmentEntryId,
                        principalTable: "AssignmentEntry",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssignmentGrade_User_GradedById",
                        column: x => x.GradedById,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentEntry_AssignmentTaskId",
                table: "AssignmentEntry",
                column: "AssignmentTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentEntry_CreatedById",
                table: "AssignmentEntry",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentGrade_AssignmentEntryId",
                table: "AssignmentGrade",
                column: "AssignmentEntryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentGrade_GradedById",
                table: "AssignmentGrade",
                column: "GradedById");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentTask_CourseId",
                table: "AssignmentTask",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentTask_CreatedById",
                table: "AssignmentTask",
                column: "CreatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignmentGrade");

            migrationBuilder.DropTable(
                name: "AssignmentEntry");

            migrationBuilder.DropTable(
                name: "AssignmentTask");
        }
    }
}
