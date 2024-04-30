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

            builder.Property(x => x.Username).IsRequired();
            builder.HasIndex(x => x.Username).IsUnique();
            builder.Property(x => x.PasswordHash).IsRequired();
            builder.Property(x => x.Salt).IsRequired();
        }
    }
}
