using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademicFlow.Infrastructure.Configuration
{
    public class UserCredentialsConfiguration : IEntityTypeConfiguration<UserCredentials>
    {
        public void Configure(EntityTypeBuilder<UserCredentials> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasOne(x => x.User)
                   .WithOne(x => x.UserCredentials)
                   .HasForeignKey<UserCredentials>(x => x.Id)
                   .HasPrincipalKey<User>(x => x.Id);
            builder.HasData(GetSeeds());
        }
        private static UserCredentials[] GetSeeds()
        {
            return
            [
                new UserCredentials
                {
                    Id = 1,
                    Username = "admin",
                    PasswordHash = "9D5224C863CDFF320DF4CA0A4FC4450EF3CAAE32C7683FB7D91EAA1E0ECDF5A7", // BadPassword01
                    Salt = "jqh08bf8",
                    SecurityKey = null
                }
            ];
        }

    }
}
