using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademicFlow.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class UserSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF NOT EXISTS (SELECT 1 FROM [User] WHERE [Id] = 1)
                BEGIN 
                    INSERT INTO [User] ([Age], [Email], [IsDeleted], [Name], [PersonalCode], [PhoneNumber], [Surname]) 
                    VALUES (-1, 'adm@adm.adm', 0, 'Admin', '000000-00000', '0000000', 'Admin');
                    INSERT INTO [UserCredentials] ([Id], [PasswordHash], [Salt], [SecurityKey], [Username])
                    VALUES (1, '9D5224C863CDFF320DF4CA0A4FC4450EF3CAAE32C7683FB7D91EAA1E0ECDF5A7', 'jqh08bf8', NULL, 'admin');
                    INSERT INTO [UserRole] ([Role], [UserId])
                    VALUES (0, 1), (1, 1), (2, 1);
                END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
