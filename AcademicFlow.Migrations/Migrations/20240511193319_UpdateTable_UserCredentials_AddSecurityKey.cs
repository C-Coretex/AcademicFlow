using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademicFlow.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTable_UserCredentials_AddSecurityKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserCredentials_Username",
                table: "UserCredentials");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "UserCredentials",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Salt",
                table: "UserCredentials",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "UserCredentials",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "SecurityKey",
                table: "UserCredentials",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecurityKey",
                table: "UserCredentials");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "UserCredentials",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Salt",
                table: "UserCredentials",
                type: "nvarchar(max)",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "UserCredentials",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_UserCredentials_Username",
                table: "UserCredentials",
                column: "Username",
                unique: true);
        }
    }
}
