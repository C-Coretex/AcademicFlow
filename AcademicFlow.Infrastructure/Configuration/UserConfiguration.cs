using AcademicFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademicFlow.Infrastructure.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Name).IsRequired();
            builder.Property(u => u.Surname).IsRequired();
            builder.Property(u => u.PersonalCode).IsRequired();
            builder.HasIndex(u => u.PersonalCode).IsUnique();
            builder.Property(u => u.IsDeleted).IsRequired();
            builder.HasIndex(u => u.IsDeleted);
            builder.HasData(GetSeeds());
        }
        private static User[] GetSeeds()
        {
            return
            [
                new User
                {
                    Id = 1,
                    Name = "Admin",
                    Surname = "Admin",
                    Email = "adm@adm.adm",
                    PersonalCode = "000000-00000",
                    PhoneNumber = "0000000",
                    Age = -1,
                    IsDeleted =  false
                }
            ];
        }
    }
}
