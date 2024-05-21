using AcademicFlow.Domain.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademicFlow.Infrastructure.Configuration
{
    public class CourseUserRoleConfiguration : IEntityTypeConfiguration<CourseUserRole>
    {
        public void Configure(EntityTypeBuilder<CourseUserRole> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasOne(x => x.UserRole).WithMany(x => x.Courses);
            builder.HasOne(x => x.Course).WithMany(x => x.Users);
        }
    }
}
