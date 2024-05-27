using AcademicFlow.Domain.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademicFlow.Infrastructure.Configuration
{
    public class CourseProgramConfiguration : IEntityTypeConfiguration<CourseProgram>
    {
        public void Configure(EntityTypeBuilder<CourseProgram> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasOne(x => x.Program).WithMany(x => x.Courses);
            builder.HasOne(x => x.Course).WithMany(x => x.Programs);
        }
    }
}
