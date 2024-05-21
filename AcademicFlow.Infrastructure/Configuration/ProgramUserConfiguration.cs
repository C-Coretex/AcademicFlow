using AcademicFlow.Domain.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademicFlow.Infrastructure.Configuration
{
    public class ProgramUserConfiguration : IEntityTypeConfiguration<ProgramUserRole>
    {
        public void Configure(EntityTypeBuilder<ProgramUserRole> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasOne(x => x.User).WithMany(x => x.Programs);
            builder.HasOne(x => x.Program).WithMany(x => x.Users);
        }
    }
}
