using AcademicFlow.Domain.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademicFlow.Infrastructure.Configuration
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasAlternateKey(x => x.PublicId);
        }
    }
}
