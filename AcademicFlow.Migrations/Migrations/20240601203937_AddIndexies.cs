using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademicFlow.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /// Remove duplicates
            migrationBuilder.Sql(@"WITH DuplicateRecordsCTE AS
(
    SELECT 
        Id,
        CourseId,
        UserRoleId,
        ROW_NUMBER() OVER (PARTITION BY CourseId, UserRoleId ORDER BY Id) AS RowNum
    FROM 
        CourseUserRole
)
DELETE FROM DuplicateRecordsCTE
WHERE RowNum > 1;");

            migrationBuilder.Sql(@"WITH DuplicateRecordsCTEProgram AS
(
    SELECT 
        Id,
        ProgramId,
        UserRoleId,
        ROW_NUMBER() OVER (PARTITION BY ProgramId, UserRoleId ORDER BY Id) AS RowNumProgram
    FROM 
        ProgramUserRole
)
DELETE FROM DuplicateRecordsCTEProgram
WHERE RowNumProgram > 1;");

            migrationBuilder.DropIndex(
                name: "IX_ProgramUserRole_UserRoleId",
                table: "ProgramUserRole");

            migrationBuilder.DropIndex(
                name: "IX_CourseUserRole_UserRoleId",
                table: "CourseUserRole");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramUserRole_UserRoleId_ProgramId",
                table: "ProgramUserRole",
                columns: new[] { "UserRoleId", "ProgramId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseUserRole_UserRoleId_CourseId",
                table: "CourseUserRole",
                columns: new[] { "UserRoleId", "CourseId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProgramUserRole_UserRoleId_ProgramId",
                table: "ProgramUserRole");

            migrationBuilder.DropIndex(
                name: "IX_CourseUserRole_UserRoleId_CourseId",
                table: "CourseUserRole");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramUserRole_UserRoleId",
                table: "ProgramUserRole",
                column: "UserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseUserRole_UserRoleId",
                table: "CourseUserRole",
                column: "UserRoleId");
        }
    }
}
