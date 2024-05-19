using AcademicFlow.Domain.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademicFlow.Infrastructure.Configuration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasOne(u => u.User)
                  .WithMany(u => u.UserRoles)
                  .HasForeignKey(u => u.UserId)
                  .HasPrincipalKey(u => u.Id);
            builder.Property(u => u.UserId).IsRequired();
            builder.Property(u => u.Role).IsRequired(); builder.HasData(GetSeeds());
        }
        private static UserRole[] GetSeeds()
        {
            return
            [
                new UserRole
                {
                    Id = 1,
                    UserId = 1,
                    Role = 0
                }
            ];
        }
    }
}
