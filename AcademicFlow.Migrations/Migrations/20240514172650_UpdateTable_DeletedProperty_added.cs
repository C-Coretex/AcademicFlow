using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademicFlow.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTable_DeletedProperty_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "UserRole");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "User",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_User_IsDeleted",
                table: "User",
                column: "IsDeleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_IsDeleted",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "Active",
                table: "UserRole",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "IsDeleted",
                table: "User",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
